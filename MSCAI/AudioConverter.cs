using NAudio;
using NAudio.Wave;
using NLayer.NAudioSupport;
using OggVorbisEncoder;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MSCD
{
    internal class AudioConverter
    {
        private readonly static Random _random = new Random();
        
        private TrackList _trackList;

        public Action<object, int, int> ProgressUpdate;

        public AudioConverter(TrackList list)
        {
            _trackList = list;
        }

        public async void Convert(string path, int countOfTracks)
        {
            var tracks = _trackList.GetTracks();
            
            if (tracks.Length == 0)
                return;

            PrepareDirectory(path);
            
            int length = Math.Min(tracks.Length, countOfTracks);

            for (int i = 0; i < length; i++)
            {
                ProgressUpdate.Invoke(this, i, length);

                var filename = $"track{i+1}.ogg";
                var destination = Path.Combine(path, filename);

                if (File.Exists(destination))
                    File.Delete(destination);

                await Task.Run(() => ConvertTrack(tracks[i], destination));
            }

            ProgressUpdate(this, length, length);
        }

        private static void PrepareDirectory(string directory)
        {
            var coverArt = Path.Combine($"{directory}", "coverart.png");
            
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            if (!File.Exists(coverArt))
                File.Copy(@".\Standart CD\coverart.png", coverArt);
        }

        private static void ConvertTrack(Track track, string destination)
        {
            using (var reader = new Mp3FileReaderBase(track.path, waveFormat => new Mp3FrameDecompressor(waveFormat)))
            {
                var format = reader.WaveFormat;
                byte[] samples = new byte[reader.Length];
                
                var readed = reader.Read(samples, 0, (int)reader.Length);

                if (readed < reader.Length)
                    throw new FileLoadException("");

                var output = ConvertWavToOgg(samples, format.BitsPerSample, format.SampleRate, format.Channels);
                
                
                File.WriteAllBytes(destination, output);
            }
        }


        private static byte[] ConvertWavToOgg(byte[] samples, int sampleSize, int sampleRate, int channels)
        {
            sampleSize /= 8;

            int samplesCount = (samples.Length / sampleSize / channels);
           
            float[][] outSamples = new float[channels][];

            for (int ch = 0; ch < channels; ch++)
            {
                outSamples[ch] = new float[samplesCount];
            }

            for (int sampleNumber = 0; sampleNumber < samplesCount; sampleNumber++)
            {
                float rawSample = 0.0f;

                for (int channel = 0; channel < channels; channel++)
                {
                    int i = (sampleNumber * channels) * sampleSize;

                    if (channel < channels)
                        i += channel * sampleSize;

                    switch (sampleSize)
                    {
                        case 1:
                            rawSample = samples[i] / (float)(1<<8);
                            break;
                        case 2:
                            rawSample = (samples[i + 1] << 8 | samples[i]) / (float)(1 << 16);
                            break;
                        case 3:
                            rawSample = (samples[i + 2] << 16 | samples[i + 1] << 8 | samples[i]) / (float)(1 << 24);
                            break;
                        case 4:
                            rawSample = BitConverter.ToSingle(new byte[] { samples[i + 0], samples[i + 1], samples[i + 2], samples[i + 3] },0);
                            break;
                    }

                    outSamples[channel][sampleNumber] = rawSample;
                }
            }

            return GenerateFile(outSamples, sampleRate, channels);
        }

        private static byte[] GenerateFile(float[][] samples, int sampleRate, int channels)
        {
            using MemoryStream data = new MemoryStream();
            
            var info = VorbisInfo.InitVariableBitRate(channels, sampleRate, 0.5f);
            var comments = new Comments();
            comments.AddTag("ARTIST", "mscai");

            var oggStream = new OggStream(_random.Next());
            oggStream.PacketIn(HeaderPacketBuilder.BuildInfoPacket(info));
            oggStream.PacketIn(HeaderPacketBuilder.BuildCommentsPacket(comments));
            oggStream.PacketIn(HeaderPacketBuilder.BuildBooksPacket(info));

            FlushPages(oggStream, data, true);

            var state = ProcessingState.Create(info);

            for (int i = 0; i <= samples[0].Length; i += 512)
            {
                if (i == samples[0].Length)
                    state.WriteEndOfStream();
                else
                    state.WriteData(samples, Math.Min(512, samples[0].Length - i), i);

                while (!oggStream.Finished && state.PacketOut(out OggPacket packet))
                {
                    oggStream.PacketIn(packet);

                    FlushPages(oggStream, data, false);
                }
            }

            FlushPages(oggStream, data, true);
            
            return data.ToArray();
        }

        private static void FlushPages(OggStream oggStream, Stream output, bool force)
        {
            while (oggStream.PageOut(out OggPage page, force))
            {
                output.Write(page.Header, 0, page.Header.Length);
                output.Write(page.Body, 0, page.Body.Length);
            }
        }
    }
}

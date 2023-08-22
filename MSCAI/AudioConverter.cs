using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using OggVorbisEncoder;
using NAudio.Wave;
using NLayer.NAudioSupport;

namespace MSCD
{
    internal class AudioConverter
    {
        private readonly static Random _random = new Random();
        
        private TrackList _trackList;

        public bool IsWorking { get; private set; }

        public Action<object> ProgressIncrement;
        public Action<object> ConvertEnd;

        public AudioConverter(TrackList list)
        {
            _trackList = list;
        }

        public async void Convert(string path)
        {
            var tracks = _trackList.GetTracks();
            
            if (tracks.Length == 0)
                return;

            IsWorking = true;

            PrepareDirectory(path);
            
            List<Task> tasks = new List<Task>(4);

            for (int j = 0; j <= (tracks.Length / 4); j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    int index = i + 4 * j;

                    if (index >= tracks.Length) break;

                    var track = tracks[index];
                    var destination = Path.Combine(path,  $"track{index + 1}.ogg");

                    if (File.Exists(destination))
                        File.Delete(destination);

                    tasks.Add(Task.Run(() => 
                    { 
                        ConvertTrack(track, destination);
                        ProgressIncrement.Invoke(this);
                    }));
                    
                }
                await Task.WhenAll(tasks);
                tasks.Clear();
            }

            ConvertEnd.Invoke(this);
            IsWorking = false;
        }

        private void PrepareDirectory(string directory)
        {
            var coverArt = Path.Combine($"{directory}", "coverart.png");
            
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            if (!File.Exists(coverArt))
                File.Copy(@".\Standart CD\coverart.png", coverArt);
        }

        private static void ConvertTrack(Track track, string destination)
        {
            using var reader = new Mp3FileReaderBase(track.path, waveFormat => new Mp3FrameDecompressor(waveFormat));
            
            var format = reader.WaveFormat;
            byte[] samples = new byte[reader.Length];

            var readed = reader.Read(samples, 0, (int)reader.Length);

            var output = ConvertWavToOgg(samples, format.BitsPerSample, format.SampleRate, format.Channels);

            File.WriteAllBytes(destination, output);
        }

        private static byte[] ConvertWavToOgg(byte[] samples, int sampleSize, int sampleRate, int channels)
        {
            sampleSize /= 8;

            int samplesCount = samples.Length / sampleSize / channels;

            float[][] outSamples = InitBuffer(channels, samplesCount);

            for (int sampleNumber = 0; sampleNumber < samplesCount; sampleNumber++)
            {
                float sample = 0.0f;

                for (int channel = 0; channel < channels; channel++)
                {
                    int i = sampleNumber * channels * sampleSize;

                    if (channel < channels)
                        i += channel * sampleSize;

                    switch (sampleSize)
                    {
                        case 1:
                            sample = samples[i] / (float)(1 << 8);
                            break;
                        case 2:
                            sample = (samples[i + 1] << 8 | samples[i]) / (float)(1 << 16);
                            break;
                        case 3:
                            sample = (samples[i + 2] << 16 | samples[i + 1] << 8 | samples[i]) / (float)(1 << 24);
                            break;
                        case 4:
                            sample = BitConverter.ToSingle(new byte[] { samples[i + 0], samples[i + 1], samples[i + 2], samples[i + 3] }, 0);
                            break;
                    }

                    outSamples[channel][sampleNumber] = sample;
                }
            }

            return GenerateFile(outSamples, sampleRate, channels);
        }

        private static float[][] InitBuffer(int channels, int samplesCount)
        {
            float[][] outSamples = new float[channels][];

            for (int ch = 0; ch < channels; ch++)
                outSamples[ch] = new float[samplesCount];
            return outSamples;
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace MSCD
{
    internal class Track
    {
        private const string ffmpegLocation = "./ffmpeg/bin/ffmpeg.exe";
        
        public string TrackName { get; private set; }

        private string _path;

        public Track(string trackName, string path)
        {
            TrackName = trackName;
            _path = path;
        }

        public static Track Load(string path)
        {
            if (!File.Exists(path)) throw new ArgumentException("File doesn't exist");

            var name = ParseNameFromPath(path);

            return new Track(name, path);
        }

        public void ConvertTrackToFormattedOgg(string directory, int index)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = ffmpegLocation,
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = $"-i \"{_path}\" \"{directory}\\track{index}.ogg\""
            };

            Process process = new Process
            {
                StartInfo = startInfo
            };

            process.Start();
            process.WaitForExit();
        }

        private static string ParseNameFromPath(string path)
        {
            string name = path.Substring(path.LastIndexOf('\\') + 1);
            name = name.Remove(name.LastIndexOf('.'));
            name = name.Trim();
            return name;
        }
    }

    internal class TrackImporter
    {
        public event Action OnListUpdated;
        
        private const int basicTracksCount = 15;
        private Dictionary<string, Track> _tracks;

        public TrackImporter()
        {
            _tracks = new Dictionary<string, Track>(basicTracksCount);
        }

        public void AddTrack(string path)
        {
            Track newMusic = Track.Load(path);

            if (_tracks.ContainsKey(newMusic.TrackName) == false)
            {
                _tracks.Add(newMusic.TrackName, newMusic);
                OnListUpdated.Invoke();
            }
        }
        
        public void RemoveTrack(string name)
        {
            if (_tracks.Remove(name) == false) 
                throw new ArgumentException("Track doesnt exist");

            OnListUpdated.Invoke();
        }

        public List<string> GetTrackNames()
        {
            List<string> names = new List<string>(basicTracksCount);

            foreach (var track in _tracks)
                names.Add(track.Value.TrackName);

            return names;
        }

        public bool CreateCD(string folderName, int countOfTracks = basicTracksCount)
        {
            if (_tracks.Count <= 0) return false;

            var directory = Directory.GetCurrentDirectory() + "\\" + folderName;

            PrepareDirectory(directory);

            int index = 1;

            foreach (var pair in _tracks)
            {
                if (index > countOfTracks)
                    break;

                var track = pair.Value;
                track.ConvertTrackToFormattedOgg(directory, index++);
            }

            return true;
        }

        private void PrepareDirectory(string directory)
        {
            if (Directory.Exists(directory))
                Directory.Delete(directory, true);

            Directory.CreateDirectory(directory);
            
            File.Copy($".\\Standart CD\\coverart.png", $"{directory}\\coverart.png");
        }
    }
}

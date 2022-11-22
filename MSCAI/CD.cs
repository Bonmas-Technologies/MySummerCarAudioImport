using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MSCD
{
    internal class TrackImporter
    {
        private const string baseDirectory = ".\\Standart CD";
        private const string coverartName = "coverart.png";

        private Dictionary<string, string> _tracks;

        public static string ParseName(string path)
        {
            string name = path.Substring(path.LastIndexOf('\\') + 1);
            name = name.Remove(name.LastIndexOf('.'));
            name = name.Trim();
            return name;
        }

        public TrackImporter()
        {
            _tracks = new Dictionary<string, string>(15);
        }

        public void AddTrack(string path)
        {
            if (!File.Exists(path)) throw new ArgumentException("File dont exist");
            var name = ParseName(path);

            if (_tracks.ContainsKey(name)) return;
            _tracks.Add(name, path);
        }
        
        public void RemoveTrack(string name)
        {
            if (!_tracks.Remove(name)) throw new ArgumentException("Track doesnt exist");
        }

        public List<string> GetTrackNames()
        {
            List<string> names = new List<string>(15);

            foreach (var track in _tracks)
                names.Add(track.Key);

            return names;
        }

        public void Render(string path)
        {
            if (_tracks.Count <= 0) return;

            var directory = Directory.GetCurrentDirectory() + "\\" + path;

            if (Directory.Exists(directory))
                Directory.Delete(directory, true);

            Directory.CreateDirectory(directory);

            File.Copy($"{baseDirectory}\\{coverartName}", $"{directory}\\{coverartName}");

            Process process = new Process();
            var info = new ProcessStartInfo();
            process.StartInfo = info;

            info.FileName = "./ffmpeg/bin/ffmpeg.exe";
            info.WindowStyle = ProcessWindowStyle.Hidden;

            int i = 1;

            foreach (var track in _tracks)
            {
                var trackPath = track.Value;

                info.Arguments = $"-i \"{trackPath}\" \"{directory}\\track{i++}.ogg\"";
                process.Start();
            }
            
            process.WaitForExit();

            OpenExplorer(directory);
        }

        private void OpenExplorer(string directory)
        {
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();

            var info = process.StartInfo;
            info.FileName = "explorer.exe";
            info.Arguments = $"{directory}";
            process.Start();
        }
    }
}

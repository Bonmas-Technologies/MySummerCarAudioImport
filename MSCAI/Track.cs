using System;
using System.IO;

namespace MSCD
{
    internal struct Track
    {
        public readonly string name;
        public readonly string path;

        public Track(string name, string path)
        {
            this.name = name;
            this.path = path;
        }

        public static Track Load(string path)
        {
            if (!File.Exists(path)) throw new ArgumentException("File doesn't exist");

            var name = ParseNameFromPath(path);

            return new Track(name, path);
        }

        private static string ParseNameFromPath(string path)
        {
            string name = path.Substring(path.LastIndexOf('\\') + 1);
            name = name.Remove(name.LastIndexOf('.'));
            name = name.Trim();
            return name;
        }
    }
}

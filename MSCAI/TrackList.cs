using System;
using System.Collections.Generic;

namespace MSCD
{
    internal class TrackList
    {
        private const int baseCount = 15;

        public event Action<string[]> OnTrackListUpdated;

        private Dictionary<string, Track> _tracks;
        
        public TrackList() => _tracks = new Dictionary<string, Track>(baseCount);

        public void AddTrack(string path)
        {
            Track newMusic = Track.Load(path);

            if (_tracks.ContainsKey(newMusic.name) == false)
            {
                _tracks.Add(newMusic.name, newMusic);
                OnTrackListUpdated.Invoke(GetTrackNames());
            }
        }

        public void RemoveTrack(string name)
        {
            if (_tracks.Remove(name) == false) 
                throw new ArgumentException("Track doesn't exist");

            OnTrackListUpdated.Invoke(GetTrackNames());
        }

        public Track[] GetTracks()
        {
            var values = _tracks.Values;

            var names = new Track[values.Count];

            values.CopyTo(names, 0);

            return names;
        }
        
        private string[] GetTrackNames()
        {
            var keys = _tracks.Keys;

            var names = new string[keys.Count];

            keys.CopyTo(names, 0);

            return names;
        }
    }
}

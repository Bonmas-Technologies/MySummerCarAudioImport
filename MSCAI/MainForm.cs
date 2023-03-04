using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace MSCD
{
    public partial class MainView : Form
    {
        private const int maxCountOfTracks = 200;

        private TrackImporter _importer;

        public MainView()
        {
            InitializeComponent();
            _importer = new TrackImporter();
            _importer.OnListUpdated += UpdateList;
        }

        private void OnMusicAddButtonClick(object sender, EventArgs e)
        {
            var result = musicLoader.ShowDialog();

            if (result != DialogResult.OK)
                return;

            foreach (var track in musicLoader.FileNames)
            {
                _importer.AddTrack(track);
            }
        }

        private void OnMusicListDoubleClick(object sender, EventArgs e)
        {
            if (MusicList.SelectedIndex == -1)
                return;

            _importer.RemoveTrack(MusicList.Items[MusicList.SelectedIndex] as string);
        }

        private void UpdateList()
        {
            var container = MusicList.Items;

            container.Clear();
            var tracks = _importer.GetTrackNames();
            container.AddRange(tracks.ToArray());
        }


        private void OnRenderButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (radioCheck.Checked)
                    _importer.CreateCD("Radio", maxCountOfTracks);
                else
                    _importer.CreateCD("CD");
            }
            catch (IOException)
            {
                MessageBox.Show("Close other applications using current directory!", "Error occured");
            }

            OpenExplorer(Directory.GetCurrentDirectory());
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

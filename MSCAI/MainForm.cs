using System;
using System.Windows.Forms;

namespace MSCD
{
    public partial class MainView : Form
    {
        private TrackList _list;
        private AudioConverter _converter;
        public MainView()
        {
            InitializeComponent();

            _list = new TrackList();
            _list.OnTrackListUpdated += UpdateList;

            _converter = new AudioConverter(_list);
            _converter.ConvertEnd += OnConvertEnd;
            _converter.ProgressIncrement += OnProgressBarUpdate;
        }

        private void OnMusicAddButtonClick(object sender, EventArgs e)
        {
            var result = musicLoader.ShowDialog();

            if (result != DialogResult.OK)
                return;

            foreach (var track in musicLoader.FileNames)
            {
                _list.AddTrack(track);
            }
        }

        private void OnMusicListDoubleClick(object sender, EventArgs e)
        {
            if (MusicList.SelectedIndex == -1)
                return;

            _list.RemoveTrack(MusicList.Items[MusicList.SelectedIndex] as string);
        }

        private void UpdateList(string[] list)
        {
            MusicList.Items.Clear();
            MusicList.Items.AddRange(list);
        }


        private void OnRenderButtonClick(object sender, EventArgs e)
        {
            if (_converter.IsWorking) return;

            DialogResult result = folderFinder.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderFinder.SelectedPath))
            {
                var tracks = _list.GetTracks();
                progress.Value = 1;
                progress.Maximum = tracks.Length + 1;

                if (tracks.Length > 199)
                    MessageBox.Show("You reached MSC radio tracks limit", "Warning!");
                else if (tracks.Length > 15)
                    MessageBox.Show("You reached MSC CD tracks limit", "Warning!");

                _converter.Convert(folderFinder.SelectedPath);
            }
        }

        private void OnConvertEnd(object obj)
        {
            progress.Value = 0;
            MessageBox.Show("Task completed");
        }

        private void OnProgressBarUpdate(object converter)
        {
            Invoke(new MethodInvoker(() => ++progress.Value));
        }
    }
}

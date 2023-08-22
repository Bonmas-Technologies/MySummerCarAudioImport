using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace MSCD
{
    public partial class MainView : Form
    {
        private TrackList _list;
        private bool isWorking = false;
        public MainView()
        {
            InitializeComponent();
            _list = new TrackList();
            _list.OnTrackListUpdated += UpdateList;
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
            AudioConverter converter = new AudioConverter(_list);
            converter.ProgressUpdate += OnProgressBarUpdate;

            DialogResult result = folderFinder.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderFinder.SelectedPath))
            {
                isWorking = true;
                converter.Convert(folderFinder.SelectedPath, 15);
            }
        }

        private void OnProgressBarUpdate(object converter, int index, int length)
        {
            progress.Value = index + 1;
            progress.Maximum = length + 1;

            if (length == index)
            {
                isWorking = false;
                MessageBox.Show("Task completed");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSCD
{
    public partial class MainView : Form
    {
        private TrackImporter music;

        public MainView()
        {
            InitializeComponent();
        }
        private void MainView_Load(object sender, EventArgs e)
        {
            music = new TrackImporter();
        }

        private void OnMusicAddButtonClick(object sender, EventArgs e)
        {
            var result = musicLoader.ShowDialog();

            if (result != DialogResult.OK)
                return;

            var tracks = musicLoader.FileNames;

            int i = 0;
            bool toManyTracks = false;

            foreach (var track in tracks)
            {
                if (i < 15)
                    music.AddTrack(track);
                else if (!radioCheck.Checked)
                    toManyTracks = true;

                if (radioCheck.Checked)
                    music.AddTrack(track);

                i++;
            }

            if (toManyTracks)
                MessageBox.Show("To many tracks in cd", "To many tracks");

            UpdateList();
        }

        private void OnMusicListDoubleClick(object sender, EventArgs e)
        {
            if (MusicList.SelectedIndex == -1)
                return;

            music.RemoveTrack(MusicList.Items[MusicList.SelectedIndex] as string);
            UpdateList();
        }

        private void UpdateList()
        {
            MusicList.Items.Clear();

            var tracks = music.GetTrackNames();

            MusicList.Items.AddRange(tracks.ToArray());
        }


        private void RederButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!radioCheck.Checked)
                    music.Render("CD");
                else
                    music.Render("Radio");

            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("Close other applications using current directory!", "Error occured");
            }
}
    }
}

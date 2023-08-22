namespace MSCD
{
    partial class MainView
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            musicLoader = new System.Windows.Forms.OpenFileDialog();
            MusicList = new System.Windows.Forms.ListBox();
            MusicAddButton = new System.Windows.Forms.Button();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            splitContainer2 = new System.Windows.Forms.SplitContainer();
            progress = new System.Windows.Forms.ProgressBar();
            RederButton = new System.Windows.Forms.Button();
            folderFinder = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            SuspendLayout();
            // 
            // musicLoader
            // 
            musicLoader.Filter = "audio|*.mp3;";
            musicLoader.Multiselect = true;
            // 
            // MusicList
            // 
            MusicList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            MusicList.Dock = System.Windows.Forms.DockStyle.Fill;
            MusicList.FormattingEnabled = true;
            MusicList.ItemHeight = 15;
            MusicList.Location = new System.Drawing.Point(0, 0);
            MusicList.Name = "MusicList";
            MusicList.Size = new System.Drawing.Size(307, 361);
            MusicList.TabIndex = 0;
            MusicList.DoubleClick += OnMusicListDoubleClick;
            // 
            // MusicAddButton
            // 
            MusicAddButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            MusicAddButton.Location = new System.Drawing.Point(3, 5);
            MusicAddButton.Name = "MusicAddButton";
            MusicAddButton.Size = new System.Drawing.Size(207, 39);
            MusicAddButton.TabIndex = 0;
            MusicAddButton.Text = "Add tracks";
            MusicAddButton.UseVisualStyleBackColor = true;
            MusicAddButton.Click += OnMusicAddButtonClick;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(RederButton);
            splitContainer1.Panel2.Controls.Add(MusicAddButton);
            splitContainer1.Size = new System.Drawing.Size(307, 441);
            splitContainer1.SplitterDistance = 390;
            splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            splitContainer2.IsSplitterFixed = true;
            splitContainer2.Location = new System.Drawing.Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(progress);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(MusicList);
            splitContainer2.Size = new System.Drawing.Size(307, 390);
            splitContainer2.SplitterDistance = 25;
            splitContainer2.TabIndex = 1;
            // 
            // progress
            // 
            progress.Dock = System.Windows.Forms.DockStyle.Fill;
            progress.Location = new System.Drawing.Point(0, 0);
            progress.Name = "progress";
            progress.Size = new System.Drawing.Size(307, 25);
            progress.TabIndex = 0;
            // 
            // RederButton
            // 
            RederButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            RederButton.Location = new System.Drawing.Point(216, 5);
            RederButton.Name = "RederButton";
            RederButton.Size = new System.Drawing.Size(88, 39);
            RederButton.TabIndex = 1;
            RederButton.Text = "Convert";
            RederButton.UseVisualStyleBackColor = true;
            RederButton.Click += OnRenderButtonClick;
            // 
            // MainView
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(307, 441);
            Controls.Add(splitContainer1);
            MinimumSize = new System.Drawing.Size(323, 480);
            Name = "MainView";
            Text = "MSC CD Creator";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.OpenFileDialog musicLoader;
        private System.Windows.Forms.ListBox MusicList;
        private System.Windows.Forms.Button MusicAddButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button RederButton;
        private System.Windows.Forms.FolderBrowserDialog folderFinder;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ProgressBar progress;
    }
}

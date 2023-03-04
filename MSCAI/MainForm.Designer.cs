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
            this.musicLoader = new System.Windows.Forms.OpenFileDialog();
            this.MusicList = new System.Windows.Forms.ListBox();
            this.MusicAddButton = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.radioCheck = new System.Windows.Forms.CheckBox();
            this.RederButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // musicLoader
            // 
            this.musicLoader.Filter = "audio|*.mp3;*.ogg;*.wav";
            this.musicLoader.Multiselect = true;
            // 
            // MusicList
            // 
            this.MusicList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MusicList.FormattingEnabled = true;
            this.MusicList.ItemHeight = 15;
            this.MusicList.Location = new System.Drawing.Point(0, 0);
            this.MusicList.Name = "MusicList";
            this.MusicList.Size = new System.Drawing.Size(321, 410);
            this.MusicList.TabIndex = 0;
            this.MusicList.DoubleClick += new System.EventHandler(this.OnMusicListDoubleClick);
            // 
            // MusicAddButton
            // 
            this.MusicAddButton.Location = new System.Drawing.Point(107, 10);
            this.MusicAddButton.Name = "MusicAddButton";
            this.MusicAddButton.Size = new System.Drawing.Size(84, 27);
            this.MusicAddButton.TabIndex = 0;
            this.MusicAddButton.Text = "Add music";
            this.MusicAddButton.UseVisualStyleBackColor = true;
            this.MusicAddButton.Click += new System.EventHandler(this.OnMusicAddButtonClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.MusicList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.radioCheck);
            this.splitContainer1.Panel2.Controls.Add(this.RederButton);
            this.splitContainer1.Panel2.Controls.Add(this.MusicAddButton);
            this.splitContainer1.Size = new System.Drawing.Size(323, 463);
            this.splitContainer1.SplitterDistance = 412;
            this.splitContainer1.TabIndex = 0;
            // 
            // radioCheck
            // 
            this.radioCheck.Location = new System.Drawing.Point(11, 10);
            this.radioCheck.Name = "radioCheck";
            this.radioCheck.Size = new System.Drawing.Size(66, 27);
            this.radioCheck.TabIndex = 2;
            this.radioCheck.Text = "Radio";
            this.radioCheck.UseVisualStyleBackColor = true;
            // 
            // RederButton
            // 
            this.RederButton.Location = new System.Drawing.Point(222, 10);
            this.RederButton.Name = "RederButton";
            this.RederButton.Size = new System.Drawing.Size(88, 27);
            this.RederButton.TabIndex = 1;
            this.RederButton.Text = "Render";
            this.RederButton.UseVisualStyleBackColor = true;
            this.RederButton.Click += new System.EventHandler(this.OnRenderButtonClick);
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 463);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainView";
            this.Text = "MSC CD Creator";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog musicLoader;
        private System.Windows.Forms.ListBox MusicList;
        private System.Windows.Forms.Button MusicAddButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button RederButton;
        private System.Windows.Forms.CheckBox radioCheck;
    }
}

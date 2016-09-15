using LibIPlug;

namespace Zony_Lrc_Download_2._0.Window
{
    partial class Window_Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Window_Main));
            this.toolStrip_Main = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_Search = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_DownLoad = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Set = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_Discuz = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Donate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_Plugins = new System.Windows.Forms.ToolStripButton();
            this.statusStrip_Main = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_Information = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar_DownLoad = new System.Windows.Forms.ToolStripProgressBar();
            this.listView_Music = new LibIPlug.ListViewNF();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip_Main.SuspendLayout();
            this.statusStrip_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip_Main
            // 
            this.toolStrip_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_Search,
            this.toolStripButton_DownLoad,
            this.toolStripButton_Set,
            this.toolStripSeparator1,
            this.toolStripButton_Discuz,
            this.toolStripButton_Donate,
            this.toolStripSeparator2,
            this.toolStripButton_Plugins});
            this.toolStrip_Main.Location = new System.Drawing.Point(0, 0);
            this.toolStrip_Main.Name = "toolStrip_Main";
            this.toolStrip_Main.Size = new System.Drawing.Size(401, 25);
            this.toolStrip_Main.TabIndex = 1;
            this.toolStrip_Main.Text = "扫描目录";
            // 
            // toolStripButton_Search
            // 
            this.toolStripButton_Search.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Search.Image")));
            this.toolStripButton_Search.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Search.Name = "toolStripButton_Search";
            this.toolStripButton_Search.Size = new System.Drawing.Size(76, 22);
            this.toolStripButton_Search.Text = "扫描目录";
            this.toolStripButton_Search.Click += new System.EventHandler(this.toolStripButton_Search_Click);
            // 
            // toolStripButton_DownLoad
            // 
            this.toolStripButton_DownLoad.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_DownLoad.Image")));
            this.toolStripButton_DownLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_DownLoad.Name = "toolStripButton_DownLoad";
            this.toolStripButton_DownLoad.Size = new System.Drawing.Size(76, 22);
            this.toolStripButton_DownLoad.Text = "下载歌词";
            this.toolStripButton_DownLoad.Click += new System.EventHandler(this.toolStripButton_DownLoad_Click);
            // 
            // toolStripButton_Set
            // 
            this.toolStripButton_Set.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Set.Image")));
            this.toolStripButton_Set.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Set.Name = "toolStripButton_Set";
            this.toolStripButton_Set.Size = new System.Drawing.Size(52, 22);
            this.toolStripButton_Set.Text = "设置";
            this.toolStripButton_Set.Click += new System.EventHandler(this.toolStripButton_Set_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton_Discuz
            // 
            this.toolStripButton_Discuz.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Discuz.Image")));
            this.toolStripButton_Discuz.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Discuz.Name = "toolStripButton_Discuz";
            this.toolStripButton_Discuz.Size = new System.Drawing.Size(52, 22);
            this.toolStripButton_Discuz.Text = "反馈";
            this.toolStripButton_Discuz.Click += new System.EventHandler(this.toolStripButton_Discuz_Click);
            // 
            // toolStripButton_Donate
            // 
            this.toolStripButton_Donate.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Donate.Image")));
            this.toolStripButton_Donate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Donate.Name = "toolStripButton_Donate";
            this.toolStripButton_Donate.Size = new System.Drawing.Size(52, 22);
            this.toolStripButton_Donate.Text = "捐赠";
            this.toolStripButton_Donate.Click += new System.EventHandler(this.toolStripButton_Donate_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton_Plugins
            // 
            this.toolStripButton_Plugins.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Plugins.Image")));
            this.toolStripButton_Plugins.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Plugins.Name = "toolStripButton_Plugins";
            this.toolStripButton_Plugins.Size = new System.Drawing.Size(52, 22);
            this.toolStripButton_Plugins.Text = "插件";
            this.toolStripButton_Plugins.Click += new System.EventHandler(this.toolStripButton_Plugins_Click);
            // 
            // statusStrip_Main
            // 
            this.statusStrip_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_Information,
            this.toolStripProgressBar_DownLoad});
            this.statusStrip_Main.Location = new System.Drawing.Point(0, 373);
            this.statusStrip_Main.Name = "statusStrip_Main";
            this.statusStrip_Main.Size = new System.Drawing.Size(401, 22);
            this.statusStrip_Main.TabIndex = 2;
            this.statusStrip_Main.Text = "statusStrip1";
            // 
            // toolStripStatusLabel_Information
            // 
            this.toolStripStatusLabel_Information.AutoSize = false;
            this.toolStripStatusLabel_Information.Name = "toolStripStatusLabel_Information";
            this.toolStripStatusLabel_Information.Size = new System.Drawing.Size(240, 17);
            this.toolStripStatusLabel_Information.Text = "状态:程序启动成功...";
            this.toolStripStatusLabel_Information.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripProgressBar_DownLoad
            // 
            this.toolStripProgressBar_DownLoad.Name = "toolStripProgressBar_DownLoad";
            this.toolStripProgressBar_DownLoad.Size = new System.Drawing.Size(145, 16);
            // 
            // listView_Music
            // 
            this.listView_Music.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_Music.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView_Music.Location = new System.Drawing.Point(12, 28);
            this.listView_Music.Name = "listView_Music";
            this.listView_Music.Size = new System.Drawing.Size(375, 333);
            this.listView_Music.TabIndex = 0;
            this.listView_Music.UseCompatibleStateImageBehavior = false;
            this.listView_Music.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "歌曲名";
            this.columnHeader1.Width = 300;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "下载";
            // 
            // Window_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 395);
            this.Controls.Add(this.statusStrip_Main);
            this.Controls.Add(this.toolStrip_Main);
            this.Controls.Add(this.listView_Music);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Window_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ZonyLrcDownLoad 3.2.3 Beta2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Window_Main_FormClosing);
            this.Load += new System.EventHandler(this.Window_Main_Load);
            this.toolStrip_Main.ResumeLayout(false);
            this.toolStrip_Main.PerformLayout();
            this.statusStrip_Main.ResumeLayout(false);
            this.statusStrip_Main.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListViewNF listView_Music;
        private System.Windows.Forms.ToolStrip toolStrip_Main;
        private System.Windows.Forms.ToolStripButton toolStripButton_Search;
        private System.Windows.Forms.ToolStripButton toolStripButton_DownLoad;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ToolStripButton toolStripButton_Set;
        private System.Windows.Forms.ToolStripButton toolStripButton_Discuz;
        private System.Windows.Forms.ToolStripButton toolStripButton_Donate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.StatusStrip statusStrip_Main;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_Information;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar_DownLoad;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton_Plugins;
    }
}
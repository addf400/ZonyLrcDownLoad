namespace Zony_Lrc_Download_2._0
{
    partial class Lrc_Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Lrc_Main));
            this.StatusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.button_DownLrc = new System.Windows.Forms.Button();
            this.button_SelectDirectory = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.显示主窗口ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.button_SetButton = new System.Windows.Forms.Button();
            this.button_Help = new System.Windows.Forms.Button();
            this.LrcListItem = new Zony_Lrc_Download_2._0.ListViewNF();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StatusStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // StatusStrip1
            // 
            this.StatusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1});
            this.StatusStrip1.Location = new System.Drawing.Point(0, 408);
            this.StatusStrip1.Name = "StatusStrip1";
            this.StatusStrip1.Size = new System.Drawing.Size(361, 22);
            this.StatusStrip1.SizingGrip = false;
            this.StatusStrip1.TabIndex = 4;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.AutoSize = false;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(250, 17);
            this.toolStripStatusLabel1.Text = "状态:";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.AutoSize = false;
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // button_DownLrc
            // 
            this.button_DownLrc.Enabled = false;
            this.button_DownLrc.Location = new System.Drawing.Point(194, 336);
            this.button_DownLrc.Name = "button_DownLrc";
            this.button_DownLrc.Size = new System.Drawing.Size(160, 30);
            this.button_DownLrc.TabIndex = 6;
            this.button_DownLrc.Text = "下载";
            this.button_DownLrc.UseVisualStyleBackColor = true;
            this.button_DownLrc.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_SelectDirectory
            // 
            this.button_SelectDirectory.Location = new System.Drawing.Point(12, 336);
            this.button_SelectDirectory.Name = "button_SelectDirectory";
            this.button_SelectDirectory.Size = new System.Drawing.Size(160, 30);
            this.button_SelectDirectory.TabIndex = 5;
            this.button_SelectDirectory.Text = "选择并扫描目录";
            this.button_SelectDirectory.UseVisualStyleBackColor = true;
            this.button_SelectDirectory.Click += new System.EventHandler(this.Button_SelectDirectory_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.显示主窗口ToolStripMenuItem,
            this.关于ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.contextMenuStrip1.Size = new System.Drawing.Size(137, 70);
            // 
            // 显示主窗口ToolStripMenuItem
            // 
            this.显示主窗口ToolStripMenuItem.Name = "显示主窗口ToolStripMenuItem";
            this.显示主窗口ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.显示主窗口ToolStripMenuItem.Text = "显示主窗口";
            this.显示主窗口ToolStripMenuItem.Click += new System.EventHandler(this.显示主窗口ToolStripMenuItem_Click);
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.关于ToolStripMenuItem.Text = "关于";
            this.关于ToolStripMenuItem.Click += new System.EventHandler(this.关于ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "ZonyLrcDownLoad";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // button_SetButton
            // 
            this.button_SetButton.Location = new System.Drawing.Point(12, 371);
            this.button_SetButton.Name = "button_SetButton";
            this.button_SetButton.Size = new System.Drawing.Size(160, 30);
            this.button_SetButton.TabIndex = 6;
            this.button_SetButton.Text = "设置";
            this.button_SetButton.UseVisualStyleBackColor = true;
            this.button_SetButton.Click += new System.EventHandler(this.button_SetButton_Click);
            // 
            // button_Help
            // 
            this.button_Help.Location = new System.Drawing.Point(194, 371);
            this.button_Help.Name = "button_Help";
            this.button_Help.Size = new System.Drawing.Size(160, 30);
            this.button_Help.TabIndex = 6;
            this.button_Help.Text = "帮助/关于";
            this.button_Help.UseVisualStyleBackColor = true;
            this.button_Help.Click += new System.EventHandler(this.button_Help_Click);
            // 
            // LrcListItem
            // 
            this.LrcListItem.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader3});
            this.LrcListItem.Location = new System.Drawing.Point(12, 12);
            this.LrcListItem.Name = "LrcListItem";
            this.LrcListItem.Size = new System.Drawing.Size(342, 318);
            this.LrcListItem.TabIndex = 0;
            this.LrcListItem.UseCompatibleStateImageBehavior = false;
            this.LrcListItem.View = System.Windows.Forms.View.Details;
            this.LrcListItem.Click += new System.EventHandler(this.LrcListItem_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "歌曲名";
            this.columnHeader1.Width = 250;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "状态";
            this.columnHeader3.Width = 75;
            // 
            // Lrc_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 430);
            this.Controls.Add(this.button_Help);
            this.Controls.Add(this.button_SetButton);
            this.Controls.Add(this.button_DownLrc);
            this.Controls.Add(this.button_SelectDirectory);
            this.Controls.Add(this.StatusStrip1);
            this.Controls.Add(this.LrcListItem);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Lrc_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "  Zony歌词下载器 2.8";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Lrc_Main_FormClosed);
            this.Load += new System.EventHandler(this.Lrc_Main_Load);
            this.SizeChanged += new System.EventHandler(this.Lrc_Main_SizeChanged);
            this.StatusStrip1.ResumeLayout(false);
            this.StatusStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        
        private ListViewNF LrcListItem;
        private System.Windows.Forms.StatusStrip StatusStrip1;
        private System.Windows.Forms.Button button_DownLrc;
        private System.Windows.Forms.Button button_SelectDirectory;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 显示主窗口ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Button button_SetButton;
        private System.Windows.Forms.Button button_Help;

    }
}


namespace Zony_Lrc_Download_2._0.Window
{
    partial class Window_Config
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.comboBox_UserDirectoryOption = new System.Windows.Forms.ComboBox();
            this.comboBox_EncodingOption = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBox_ThreadNumberOption = new System.Windows.Forms.TextBox();
            this.comboBox_LrcSourceOption = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.checkBox_IgnoreFileOption = new System.Windows.Forms.CheckBox();
            this.textBox_FileSuffix = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.checkBox_Update = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(209, 100);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.comboBox_UserDirectoryOption);
            this.tabPage1.Controls.Add(this.comboBox_EncodingOption);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(201, 74);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "输出";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // comboBox_UserDirectoryOption
            // 
            this.comboBox_UserDirectoryOption.FormattingEnabled = true;
            this.comboBox_UserDirectoryOption.Items.AddRange(new object[] {
            "写到同目录",
            "自定义目录"});
            this.comboBox_UserDirectoryOption.Location = new System.Drawing.Point(71, 41);
            this.comboBox_UserDirectoryOption.Name = "comboBox_UserDirectoryOption";
            this.comboBox_UserDirectoryOption.Size = new System.Drawing.Size(120, 20);
            this.comboBox_UserDirectoryOption.TabIndex = 1;
            this.comboBox_UserDirectoryOption.Text = "写到同目录";
            // 
            // comboBox_EncodingOption
            // 
            this.comboBox_EncodingOption.FormattingEnabled = true;
            this.comboBox_EncodingOption.Items.AddRange(new object[] {
            "UTF-8",
            "GB2312",
            "GBK",
            "BIG5(繁体)",
            "日语 (Shift-JIS)",
            "ANSI"});
            this.comboBox_EncodingOption.Location = new System.Drawing.Point(71, 9);
            this.comboBox_EncodingOption.Name = "comboBox_EncodingOption";
            this.comboBox_EncodingOption.Size = new System.Drawing.Size(120, 20);
            this.comboBox_EncodingOption.TabIndex = 1;
            this.comboBox_EncodingOption.Text = "UTF-8";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "输出目录:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "编码格式:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBox_ThreadNumberOption);
            this.tabPage2.Controls.Add(this.comboBox_LrcSourceOption);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(201, 74);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "下载";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBox_ThreadNumberOption
            // 
            this.textBox_ThreadNumberOption.Location = new System.Drawing.Point(71, 41);
            this.textBox_ThreadNumberOption.Name = "textBox_ThreadNumberOption";
            this.textBox_ThreadNumberOption.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_ThreadNumberOption.Size = new System.Drawing.Size(30, 21);
            this.textBox_ThreadNumberOption.TabIndex = 4;
            this.textBox_ThreadNumberOption.Text = "4";
            this.textBox_ThreadNumberOption.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // comboBox_LrcSourceOption
            // 
            this.comboBox_LrcSourceOption.FormattingEnabled = true;
            this.comboBox_LrcSourceOption.Items.AddRange(new object[] {
            "全引擎",
            "仅百度乐库",
            "仅网易云音乐"});
            this.comboBox_LrcSourceOption.Location = new System.Drawing.Point(59, 9);
            this.comboBox_LrcSourceOption.Name = "comboBox_LrcSourceOption";
            this.comboBox_LrcSourceOption.Size = new System.Drawing.Size(136, 20);
            this.comboBox_LrcSourceOption.TabIndex = 3;
            this.comboBox_LrcSourceOption.Text = "全引擎";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "下载线程:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "歌词源:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.checkBox_IgnoreFileOption);
            this.tabPage3.Controls.Add(this.textBox_FileSuffix);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(201, 74);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "搜索";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // checkBox_IgnoreFileOption
            // 
            this.checkBox_IgnoreFileOption.AutoSize = true;
            this.checkBox_IgnoreFileOption.Location = new System.Drawing.Point(6, 44);
            this.checkBox_IgnoreFileOption.Name = "checkBox_IgnoreFileOption";
            this.checkBox_IgnoreFileOption.Size = new System.Drawing.Size(132, 16);
            this.checkBox_IgnoreFileOption.TabIndex = 7;
            this.checkBox_IgnoreFileOption.Text = "略过已经下载的文件";
            this.checkBox_IgnoreFileOption.UseVisualStyleBackColor = true;
            // 
            // textBox_FileSuffix
            // 
            this.textBox_FileSuffix.Location = new System.Drawing.Point(71, 9);
            this.textBox_FileSuffix.Name = "textBox_FileSuffix";
            this.textBox_FileSuffix.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_FileSuffix.Size = new System.Drawing.Size(118, 21);
            this.textBox_FileSuffix.TabIndex = 6;
            this.textBox_FileSuffix.Text = "*.acc;*.mp3;*.ape;*.flac";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "扫描后缀:";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.checkBox_Update);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(201, 74);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "网络";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // checkBox_Update
            // 
            this.checkBox_Update.AutoSize = true;
            this.checkBox_Update.Location = new System.Drawing.Point(50, 26);
            this.checkBox_Update.Name = "checkBox_Update";
            this.checkBox_Update.Size = new System.Drawing.Size(96, 16);
            this.checkBox_Update.TabIndex = 0;
            this.checkBox_Update.Text = "自动检测更新";
            this.checkBox_Update.UseVisualStyleBackColor = true;
            // 
            // Window_Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(229, 121);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Window_Config";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Window_Config_FormClosing);
            this.Load += new System.EventHandler(this.Window_Config_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_EncodingOption;
        private System.Windows.Forms.ComboBox comboBox_UserDirectoryOption;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_LrcSourceOption;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_ThreadNumberOption;
        private System.Windows.Forms.TextBox textBox_FileSuffix;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBox_IgnoreFileOption;
        private System.Windows.Forms.CheckBox checkBox_Update;
    }
}
namespace Zony_Lrc_Download_2._0
{
    partial class Seting
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.comboBox_DownLoadEngine = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_DL_ThreadNum = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBox_DownLoadPath = new System.Windows.Forms.ComboBox();
            this.comboBox_Encoding = new System.Windows.Forms.ComboBox();
            this.label_Title_B = new System.Windows.Forms.Label();
            this.label_Title_A = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_SearchOption = new System.Windows.Forms.ComboBox();
            this.button_SaveSet = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.comboBox_DownLoadEngine);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.textBox_DL_ThreadNum);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(12, 100);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(260, 51);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "下载选项";
            // 
            // comboBox_DownLoadEngine
            // 
            this.comboBox_DownLoadEngine.FormattingEnabled = true;
            this.comboBox_DownLoadEngine.Items.AddRange(new object[] {
            "全引擎",
            "仅Cnlryic",
            "仅百度乐库",
            "仅网易云音乐"});
            this.comboBox_DownLoadEngine.Location = new System.Drawing.Point(152, 24);
            this.comboBox_DownLoadEngine.Name = "comboBox_DownLoadEngine";
            this.comboBox_DownLoadEngine.Size = new System.Drawing.Size(102, 20);
            this.comboBox_DownLoadEngine.TabIndex = 3;
            this.comboBox_DownLoadEngine.Text = "全引擎";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(105, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "歌词源:";
            // 
            // textBox_DL_ThreadNum
            // 
            this.textBox_DL_ThreadNum.Location = new System.Drawing.Point(71, 24);
            this.textBox_DL_ThreadNum.Name = "textBox_DL_ThreadNum";
            this.textBox_DL_ThreadNum.Size = new System.Drawing.Size(18, 21);
            this.textBox_DL_ThreadNum.TabIndex = 2;
            this.textBox_DL_ThreadNum.Text = "8";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "线程数目:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBox_DownLoadPath);
            this.groupBox2.Controls.Add(this.comboBox_Encoding);
            this.groupBox2.Controls.Add(this.label_Title_B);
            this.groupBox2.Controls.Add(this.label_Title_A);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(260, 82);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "输出选项";
            // 
            // comboBox_DownLoadPath
            // 
            this.comboBox_DownLoadPath.FormattingEnabled = true;
            this.comboBox_DownLoadPath.Items.AddRange(new object[] {
            "写到同目录",
            "自定义目录"});
            this.comboBox_DownLoadPath.Location = new System.Drawing.Point(71, 50);
            this.comboBox_DownLoadPath.Name = "comboBox_DownLoadPath";
            this.comboBox_DownLoadPath.Size = new System.Drawing.Size(183, 20);
            this.comboBox_DownLoadPath.TabIndex = 1;
            this.comboBox_DownLoadPath.Text = "写到同目录";
            // 
            // comboBox_Encoding
            // 
            this.comboBox_Encoding.FormattingEnabled = true;
            this.comboBox_Encoding.Items.AddRange(new object[] {
            "UTF-8",
            "GB2312",
            "GBK",
            "BIG5(繁体)",
            "日语 (Shift-JIS)"});
            this.comboBox_Encoding.Location = new System.Drawing.Point(95, 20);
            this.comboBox_Encoding.Name = "comboBox_Encoding";
            this.comboBox_Encoding.Size = new System.Drawing.Size(159, 20);
            this.comboBox_Encoding.TabIndex = 0;
            this.comboBox_Encoding.Text = "UTF-8";
            // 
            // label_Title_B
            // 
            this.label_Title_B.AutoSize = true;
            this.label_Title_B.Location = new System.Drawing.Point(6, 53);
            this.label_Title_B.Name = "label_Title_B";
            this.label_Title_B.Size = new System.Drawing.Size(59, 12);
            this.label_Title_B.TabIndex = 0;
            this.label_Title_B.Text = "输出方式:";
            // 
            // label_Title_A
            // 
            this.label_Title_A.AutoSize = true;
            this.label_Title_A.Location = new System.Drawing.Point(6, 23);
            this.label_Title_A.Name = "label_Title_A";
            this.label_Title_A.Size = new System.Drawing.Size(83, 12);
            this.label_Title_A.TabIndex = 0;
            this.label_Title_A.Text = "输出编码格式:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboBox_SearchOption);
            this.groupBox1.Location = new System.Drawing.Point(12, 157);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(260, 48);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "扫描选项";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "文件名搜索设置(*):";
            // 
            // comboBox_SearchOption
            // 
            this.comboBox_SearchOption.FormattingEnabled = true;
            this.comboBox_SearchOption.Items.AddRange(new object[] {
            "使用全文件名",
            "采用\'-\'分割读取信息"});
            this.comboBox_SearchOption.Location = new System.Drawing.Point(119, 20);
            this.comboBox_SearchOption.Name = "comboBox_SearchOption";
            this.comboBox_SearchOption.Size = new System.Drawing.Size(135, 20);
            this.comboBox_SearchOption.TabIndex = 4;
            this.comboBox_SearchOption.Text = "使用全文件名";
            // 
            // button_SaveSet
            // 
            this.button_SaveSet.Location = new System.Drawing.Point(83, 211);
            this.button_SaveSet.Name = "button_SaveSet";
            this.button_SaveSet.Size = new System.Drawing.Size(108, 44);
            this.button_SaveSet.TabIndex = 5;
            this.button_SaveSet.Text = "保存设置";
            this.button_SaveSet.UseVisualStyleBackColor = true;
            this.button_SaveSet.Click += new System.EventHandler(this.button_SaveSet_Click);
            // 
            // Seting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 262);
            this.Controls.Add(this.button_SaveSet);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Seting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置";
            this.Load += new System.EventHandler(this.Seting_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox comboBox_DownLoadEngine;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_DL_ThreadNum;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboBox_DownLoadPath;
        private System.Windows.Forms.ComboBox comboBox_Encoding;
        private System.Windows.Forms.Label label_Title_B;
        private System.Windows.Forms.Label label_Title_A;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_SaveSet;
        private System.Windows.Forms.ComboBox comboBox_SearchOption;
        private System.Windows.Forms.Label label1;

    }
}
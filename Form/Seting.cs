using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Zony_Lrc_Download_2._0
{
    public partial class Seting : Form
    {
        public Seting()
        {
            InitializeComponent();
        }

        private void Seting_Load(object sender, EventArgs e)
        {
            this.Icon = Zony_Lrc_Download_2._0.Resource1._6;
            
            Config.Load();
            // 设置界面属性
            comboBox_Encoding.SelectedIndex = Config.m_EncodingOption;
            if (Config.m_LrcDownDirectory != "None")
            {
                comboBox_DownLoadPath.SelectedIndex = 1;
                comboBox_DownLoadPath.Text = Config.m_LrcDownDirectory;
            }
            textBox_DL_ThreadNum.Text = Config.m_DownLoadThreadNum.ToString();
            comboBox_DownLoadEngine.SelectedIndex = Config.m_LrcDownSource;
            comboBox_SearchOption.SelectedIndex = Config.m_SearchFileNameOption;
        }

        private void button_SaveSet_Click(object sender, EventArgs e)
        {
            Config.m_EncodingOption = comboBox_Encoding.SelectedIndex;
            Config.m_DownLoadThreadNum = int.Parse(textBox_DL_ThreadNum.Text);
            Config.m_LrcDownSource = comboBox_DownLoadEngine.SelectedIndex;
            Config.m_SearchFileNameOption = comboBox_SearchOption.SelectedIndex;

            Config.Save();
            Close();
        }

        private void comboBox_DownLoadPath_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBox_DownLoadPath.SelectedIndex == 1)
            {
                FolderBrowserDialog fd = new FolderBrowserDialog();
                fd.Description = "请选择歌词要下载到的目录:";
                fd.ShowDialog();

                if (fd.SelectedPath != "")
                {
                    Config.m_LrcDownDirectory = fd.SelectedPath;
                }
                else
                {
                    Config.m_LrcDownDirectory = "None";
                    comboBox_DownLoadPath.SelectedIndex = 0;
                }
            }
        }
    }
}

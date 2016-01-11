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
            if(Config.m_LrcDownDirectory != "null")
            {
                comboBox_DownLoadPath.SelectedIndex = 1;
            }
            textBox_DL_ThreadNum.Text = Config.m_DownLoadThreadNum.ToString();
            comboBox_DownLoadEngine.SelectedIndex = Config.m_LrcDownSource;
            comboBox_SearchOption.SelectedIndex = Config.m_SearchFileNameOption;
        }

        private void button_SaveSet_Click(object sender, EventArgs e)
        {
            Config.m_EncodingOption = comboBox_Encoding.SelectedIndex;
            Config.m_LrcDownDirectory = "null";
            Config.m_DownLoadThreadNum = int.Parse(textBox_DL_ThreadNum.Text);
            Config.m_LrcDownSource = comboBox_DownLoadEngine.SelectedIndex;
            Config.m_SearchFileNameOption = comboBox_SearchOption.SelectedIndex;

            Config.Save();
            Close();
        }
    }
}

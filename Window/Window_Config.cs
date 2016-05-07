/*
 * 描述：设置窗口。
 * 作者：Zony
 * 创建日期：2016/05/06
 * 最后修改日期：2016/05/06
 * 版本：1.0
 */
using System;
using System.Windows.Forms;
using Zony_Lrc_Download_2._0.Class.Configs;

namespace Zony_Lrc_Download_2._0.Window
{
    public partial class Window_Config : Form
    {
        public Window_Config()
        {
            InitializeComponent();
        }

        private void Window_Config_Load(object sender, EventArgs e)
        {
            Icon = Resource1._6;

            Config.Load();
            comboBox_EncodingOption.SelectedIndex = Config.option_Encoding;
            if(Config.option_UserDirectory == "null")
            {
                comboBox_UserDirectoryOption.SelectedIndex = 0;
            }
            else
            {
                comboBox_UserDirectoryOption.SelectedIndex = 1;
                comboBox_UserDirectoryOption.Text = Config.option_UserDirectory;
            }
            comboBox_LrcSourceOption.SelectedIndex = Config.option_LrcSource;
            textBox_ThreadNumberOption.Text = Config.option_ThreadNumber.ToString();
            textBox_FileSuffix.Text = Config.option_FileSuffix;
            checkBox_IgnoreFileOption.Checked = Config.option_IgnoreFile == 0 ? false : true;
            checkBox_Update.Checked = Config.option_Update == 0 ? false : true;
        }

        private void Window_Config_FormClosing(object sender, FormClosingEventArgs e)
        {
            Config.option_Encoding = comboBox_EncodingOption.SelectedIndex;
            Config.option_LrcSource = comboBox_LrcSourceOption.SelectedIndex;
            Config.option_ThreadNumber = int.Parse(textBox_ThreadNumberOption.Text);
            Config.option_FileSuffix = textBox_FileSuffix.Text;
            Config.option_IgnoreFile = checkBox_IgnoreFileOption.Checked ? 1 : 0;
            Config.option_Update = checkBox_Update.Checked ? 1 : 0;
            Config.Save();
            // 同步
            System.Net.ServicePointManager.DefaultConnectionLimit = Config.option_ThreadNumber;
        }
    }
}

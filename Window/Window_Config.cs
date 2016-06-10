/*
 * 描述：设置窗口。
 * 作者：Zony
 * 创建日期：2016/05/06
 * 最后修改日期：2016/05/10
 * 版本：1.0
 */
using System;
using System.Windows.Forms;
using Zony_Lrc_Download_2._0.Class.Configs;
using Zony_Lrc_Download_2._0.Class.UI;

namespace Zony_Lrc_Download_2._0.Window
{
    public partial class Window_Config : UI_From
    {
        public Window_Config()
        {
            InitializeComponent();
        }

        private void Window_Config_Load(object sender, EventArgs e)
        {
            Config.Load();
            comboBox_EncodingOption.SelectedIndex = Config.option_Encoding;
            if(Config.option_UserDirectory != "null")
            {
                comboBox_UserDirectoryOption.SelectedIndex = 1;
                comboBox_UserDirectoryOption.Text = Config.option_UserDirectory;
            }
            textBox_ThreadNumberOption.Text = Config.option_ThreadNumber.ToString();
            textBox_FileSuffix.Text = Config.option_FileSuffix;
            checkBox_IgnoreFileOption.Checked = Config.option_IgnoreFile == 0 ? false : true;
            checkBox_Update.Checked = Config.option_Update == 0 ? false : true;
        }

        private void Window_Config_FormClosing(object sender, FormClosingEventArgs e)
        {
            Config.option_Encoding = comboBox_EncodingOption.SelectedIndex;
            Config.option_ThreadNumber = int.Parse(textBox_ThreadNumberOption.Text);
            Config.option_FileSuffix = textBox_FileSuffix.Text;
            Config.option_IgnoreFile = checkBox_IgnoreFileOption.Checked ? 1 : 0;
            Config.option_Update = checkBox_Update.Checked ? 1 : 0;
            if (comboBox_UserDirectoryOption.SelectedIndex == 0)
            {
                Config.option_UserDirectory = "null";
            }
            Config.Save();

            // 同步并发链接数
            System.Net.ServicePointManager.DefaultConnectionLimit = Config.option_ThreadNumber;
        }

        private void comboBox_UserDirectoryOption_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBox_UserDirectoryOption.SelectedIndex == 1)
            {
                var fl = new FolderBrowserDialog();
                fl.Description = "请选择自定义路径:";
                if (fl.ShowDialog() == DialogResult.OK)
                {
                    Config.option_UserDirectory = fl.SelectedPath;
                }
            }
        }

    }
}

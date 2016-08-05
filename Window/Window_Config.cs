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
            ConfigBind();
        }

        private void Window_Config_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfigSave();   
        }

        private void comboBox_UserDirectoryOption_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBox_UserDirectoryOption.SelectedIndex == 1)
            {
                var fl = new FolderBrowserDialog();
                fl.Description = "请选择自定义路径:";
                if (fl.ShowDialog() == DialogResult.OK)
                {
                    Config.configValue.option_UserDirectory = fl.SelectedPath;
                }
            }
        }

        // 设置数据绑定
        private void ConfigBind()
        {
            Config.Load();
            comboBox_EncodingOption.SelectedIndex = Config.configValue.option_Encoding;
            if (Config.configValue.option_UserDirectory != "null")
            {
                comboBox_UserDirectoryOption.SelectedIndex = 1;
                comboBox_UserDirectoryOption.Text = Config.configValue.option_UserDirectory;
            }
            textBox_ThreadNumberOption.Text = Config.configValue.option_ThreadNumber.ToString();
            textBox_FileSuffix.Text = Config.configValue.option_FileSuffix;
            checkBox_IgnoreFileOption.Checked = Config.configValue.option_IgnoreFile;
            checkBox_Update.Checked = Config.configValue.option_Update;
        }
        // 保存设置数据
        private void ConfigSave()
        {
            Config.configValue.option_Encoding = comboBox_EncodingOption.SelectedIndex;
            Config.configValue.option_ThreadNumber = int.Parse(textBox_ThreadNumberOption.Text);
            Config.configValue.option_FileSuffix = textBox_FileSuffix.Text;
            Config.configValue.option_IgnoreFile = checkBox_IgnoreFileOption.Checked;
            Config.configValue.option_Update = checkBox_Update.Checked;
            if (comboBox_UserDirectoryOption.SelectedIndex == 0)
            {
                Config.configValue.option_UserDirectory = "null";
            }
            Config.Save();

            // 同步并发链接数
            System.Net.ServicePointManager.DefaultConnectionLimit = Config.configValue.option_ThreadNumber;
        }
    }
}

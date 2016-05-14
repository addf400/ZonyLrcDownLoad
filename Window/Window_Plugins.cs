using System;
using System.Windows.Forms;
using System.Reflection;
using Zony_Lrc_Download_2._0.Class.Plugins;
using LibIPlug;
using System.Text;
using Zony_Lrc_Download_2._0.Class.Configs;

namespace Zony_Lrc_Download_2._0.Window
{
    public partial class Window_Plugins : Form
    {
        public Window_Plugins()
        {
            InitializeComponent();
        }

        private void Window_Plugins_Load(object sender, EventArgs e)
        {
            Icon = Resource1._6;
            // 加载插件状态
            Config.Load();
            string[] stra = Config.option_PlugState.Split(',');
            int count=0;
            // 加载插件列表
            foreach (PluginInfoAttribute info in Untiy.piProperties)
            {
                listView_Plugins.Items.Add(new ListViewItem(new string[] 
                {
                    info.Name,info.Descript,info.Version,info.Author,info.Ptype.ToString()
                }));

                try
                {
                    if (stra[count] == "0")
                    {
                        listView_Plugins.Items[count].Checked = false;
                    }
                    else
                    {
                        listView_Plugins.Items[count].Checked = true;
                    }
                }
                catch (Exception)
                {
                    listView_Plugins.Items[count].Checked = false;
                }

                count++;
            }
        }

        private void Window_Plugins_FormClosing(object sender, FormClosingEventArgs e)
        {
            var str = new StringBuilder();
            for (int i=0;i<listView_Plugins.Items.Count;i++)
            {
                if(listView_Plugins.Items[i].Checked)
                {
                    str.Append("1,");
                }
                else
                {
                    str.Append("0,");
                }
            }
            str.Remove(str.Length-1, 1);
            Config.option_PlugState = str.ToString();
            Config.Save();
        }
    }
}

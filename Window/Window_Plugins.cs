﻿/*
 * 描述：用于插件的管理
 * 作者：Zony
 * 创建日期：2016/06/10
 * 最后修改日期：2016/06/10
 * 版本：1.0
 */
using System;
using System.Windows.Forms;
using Zony_Lrc_Download_2._0.Class.Configs;
using Zony_Lrc_Download_2._0.Class.UI;
using Zony_Lrc_Download_2._0.Class.Utils;

namespace Zony_Lrc_Download_2._0.Window
{
    public partial class Window_Plugins : UI_From
    {
        public Window_Plugins()
        {
            InitializeComponent();
        }

        private void Window_Plugins_Load(object sender, EventArgs e)
        {
            LongLife.Plug_Lrc.LoadPlugs();
            LongLife.Plug_High.LoadPlugs();
            loadPlug();
        }

        private void Window_Plugins_FormClosing(object sender, FormClosingEventArgs e)
        {
            savePlugsList();
        }

        private void loadPlug()
        {
            // 加载插件状态
            Config.Load();

            int count = 0;
            // 加载插件列表
            foreach (var item in LongLife.Plug_Lrc.PlugsInfo)
            {
                listView_Plugins.Items.Add(new ListViewItem(new string[]
                {
                    item.Name,
                    item.Descript,
                    item.Version,
                    item.Author,
                    item.Ptype.ToString()
                }));

                listView_Plugins.Items[count].Checked = Config.configValue.option_PlugStatus[count].IsOpen;
                count++;
            }
        }
        private void savePlugsList()
        {
            Config.Load();
            int count = 0;
            foreach (var item in Config.configValue.option_PlugStatus)
            {
                item.IsOpen = listView_Plugins.Items[count].Checked;
                count++;
            }
            Config.Save();
        }
    }
}

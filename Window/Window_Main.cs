using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Zony_Lrc_Download_2._0.Class.Utils.FileOperate;
using Zony_Lrc_Download_2._0.Class.Utils;
using Zony_Lrc_Download_2._0.Class.Utils.DownLoad;
using Zony_Lrc_Download_2._0.Class.Configs;
using Zony_Lrc_Download_2._0.Class.Plugins;
using System.IO;
using System.Threading.Tasks;
using LibIPlug;

namespace Zony_Lrc_Download_2._0.Window
{
    public partial class Window_Main : Form
    {
        public Window_Main()
        {
            InitializeComponent();
        }

        private void Window_Main_Load(object sender, EventArgs e)
        {
            if (Untiy.LoadPlugins() == 0)
            {
                MessageBox.Show("插件加载失败，无法启动程序。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
            Config.Load();
            // 允许跨线程操作控件
            CheckForIllegalCrossThreadCalls = false;
            System.Net.ServicePointManager.DefaultConnectionLimit = Config.option_ThreadNumber;
            Icon = Resource1._6;

            #region 更新检测
            if(Config.option_Update == 1)
            {
                new Thread(() =>
                {
                    var result = new Tools().Http_Get("http://myzony.com/update.txt", Encoding.UTF8).Split(',');
                    if (int.Parse(result[0]) > Ver.CurrentVersion)
                    {
                        if (MessageBox.Show("检测到新版本，是否下载新版本？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            System.Diagnostics.Process.Start(result[1]);
                        }
                    }
                }).Start();
            }
            #endregion
        }

        private void toolStripButton_Search_Click(object sender, EventArgs e)
        {
            var fb = new FolderBrowserDialog();
            fb.Description = "请选择你歌曲所在的目录:";
            if (fb.ShowDialog() == DialogResult.OK)
            {
                // 清空
                LongLife.MusicPathList.Clear();
                LongLife.MusicPathFailedList.Clear();
                listView_Music.Items.Clear();

                if (new FileSearch().Search(ref LongLife.MusicPathList,fb.SelectedPath,new Tools().SplitString(Config.option_FileSuffix,';')) == FileSearch.FileSearchResult.Normal)
                {
                    foreach (KeyValuePair<int,string> key in LongLife.MusicPathList)
                    {
                        // 向ListView添加条目
                        string[] str = { Path.GetFileNameWithoutExtension(key.Value), "" };
                        listView_Music.Items.Insert(key.Key, new ListViewItem(str));
                    }

                    toolStripStatusLabel_Information.Text = string.Format("扫描到{0}个文件...", listView_Music.Items.Count);
                    toolStripProgressBar_DownLoad.Maximum = listView_Music.Items.Count;
                }
                else
                {
                    MessageBox.Show("没有找到音乐文件。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void toolStripButton_DownLoad_Click(object sender, EventArgs e)
        {
            if (listView_Music.Items.Count != 0)
            {
                new Thread(() =>
                {
                    // 禁用控件
                    toolStripButton_DownLoad.Enabled = false;
                    toolStripButton_Search.Enabled = false;

                    LongLife.MusicPathFailedList.Clear();
                    
                    // 检测插件开关
                    string[] state = Config.option_PlugState.Split(',');
                    for(int i=0,j=0;i<Untiy.PluginsList.Count;i++)
                    {
                        if(Untiy.piProperties[i].Ptype == 0 && state[i] == "1" && j==0)
                        {
                            j++;
                            ParallelDownLoad(LongLife.MusicPathList, Untiy.piProperties[i].Name + "正在下载...", Untiy.PluginsList[i]);
                        }
                        else if(Untiy.piProperties[i].Ptype ==0 && state[i] == "1" && j > 0)
                        {
                            // 拷贝失败字典
                            var no = Functions.DictionaryCopy(ref LongLife.MusicPathFailedList);
                            ParallelDownLoad(no, Untiy.piProperties[i].Name + "正在下载...", Untiy.PluginsList[i]);
                        }
                    }

                    toolStripStatusLabel_Information.Text = string.Format("下载完成，总文件：{0}成功：{1} 失败{2}...", LongLife.MusicPathList.Count,LongLife.MusicPathList.Count - LongLife.MusicPathFailedList.Count, LongLife.MusicPathFailedList.Count);

                    //初始化进度条
                    toolStripProgressBar_DownLoad.Value = 0;
                    toolStripProgressBar_DownLoad.Maximum = 0;

                    // 启用控件
                    toolStripButton_DownLoad.Enabled = true;
                    toolStripButton_Search.Enabled = true;
                }).Start();
            }
        }

        private void ParallelDownLoad(Dictionary<int,string> container,string info,IPlugin lrcDown)
        {
            toolStripStatusLabel_Information.Text = info;
            toolStripProgressBar_DownLoad.Value = 0;
            toolStripProgressBar_DownLoad.Maximum = container.Count;

            Parallel.ForEach(container, new ParallelOptions() { MaxDegreeOfParallelism = Config.option_ThreadNumber }, (item) =>
            {
                try
                {
                    var fs = new FileWrite();
                    byte[] lrcData = null;
                    if (Config.option_IgnoreFile == 1 && File.Exists(Path.GetDirectoryName(item.Value) + "\\" + Path.GetFileNameWithoutExtension(item.Value) + ".lrc"))
                    {
                        listView_Music.Items[item.Key].SubItems[1].Text = "略过";
                    }
                    else
                    {
                        // 下载歌词
                        if (lrcDown.Down(item.Value, ref lrcData,Config.option_ThreadNumber) == true)
                        {
                            listView_Music.Items[item.Key].SubItems[1].Text = "成功";
                            if (fs.Write(ref lrcData, item.Value, Config.option_Encoding, Config.option_UserDirectory))
                            {
                                lock (LongLife.MusicPathFailedList) LongLife.MusicPathFailedList.Remove(item.Key);
                            }
                            else
                            {
                                listView_Music.Items[item.Key].SubItems[1].Text = "失败";
                            }
                        }
                        else
                        {
                            listView_Music.Items[item.Key].SubItems[1].Text = "失败";
                            lock (LongLife.MusicPathFailedList)
                            {
                                if (!LongLife.MusicPathFailedList.ContainsKey(item.Key))
                                {
                                    LongLife.MusicPathFailedList.Add(item.Key, item.Value);
                                }
                            }
                        }
                    }
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.ToString());
                }
                toolStripProgressBar_DownLoad.Value++;
            });

        }

        #region 界面互操作
        private void Window_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void toolStripButton_Plugins_Click(object sender, EventArgs e)
        {
            new Window_Plugins().ShowDialog();
        }

        private void toolStripButton_Set_Click(object sender, EventArgs e)
        {
            new Window_Config().ShowDialog();
        }

        private void toolStripButton_Donate_Click(object sender, EventArgs e)
        {
            new Window_Donate().ShowDialog();
        }

        private void toolStripButton_Discuz_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://jq.qq.com/?_wv=1027&k=Zrl68q");
        }
        #endregion
    }
}

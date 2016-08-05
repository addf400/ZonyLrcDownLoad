using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using Zony_Lrc_Download_2._0.Class.Utils.FileOperate;
using Zony_Lrc_Download_2._0.Class.Utils;
using Zony_Lrc_Download_2._0.Class.Utils.DownLoad;
using Zony_Lrc_Download_2._0.Class.Configs;
using Zony_Lrc_Download_2._0.Class.Plugins;
using Zony_Lrc_Download_2._0.Class.UI;
using LibIPlug;

namespace Zony_Lrc_Download_2._0.Window
{
    public partial class Window_Main : UI_From
    {
        public Window_Main()
        {
            InitializeComponent();
        }

        private void Window_Main_Load(object sender, EventArgs e)
        {
            if (Untiy.LoadPlugins() == 0)
            {
                MessageBox.Show("基础插件加载失败，无法正常运行程序，请点击反馈按钮寻找技术支持。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Config.Load();
                applicationSet();
                updateCheck();
            }
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

                // 搜寻文件
                if (new FileSearch().Search(ref LongLife.MusicPathList,fb.SelectedPath,FuncUtils.SplitString(Config.configValue.option_FileSuffix,';')) == FileSearch.FileSearchResult.Normal)
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
                    bool firstPlug = true;
                    int count = 0;

                    // 检测各项插件开关
                    foreach(var item in Config.configValue.option_PlugStatus)
                    {
                        if(item.IsOpen && Untiy.piProperties[count].Ptype == 0)
                        {
                            if(firstPlug)
                            {
                                ParallelDownLoad(LongLife.MusicPathList, Untiy.piProperties[count].Name + "正在下载...", Untiy.Plugs[count]);
                                firstPlug = false;
                            }
                            else
                            {
                                /* 拷贝失败字典是为了防止在Foreach当中对集合进行删除操作所导致的程序崩溃 */
                                var no = FuncUtils.DictionaryCopy(ref LongLife.MusicPathFailedList);
                                ParallelDownLoad(no, Untiy.piProperties[count].Name + "正在下载...", Untiy.Plugs[count]);
                            }
                            count++;
                        }
                    }

                    uiRest();
                }).Start();
            }
        }

        private void ParallelDownLoad(Dictionary<int,string> container,string info,IPlugin lrcDown)
        {
            toolStripStatusLabel_Information.Text = info;
            toolStripProgressBar_DownLoad.Value = 0;
            toolStripProgressBar_DownLoad.Maximum = container.Count;

            Parallel.ForEach(container, new ParallelOptions() { MaxDegreeOfParallelism = Config.configValue.option_ThreadNumber }, (item) =>
            {
                try
                {
                    FileWrite fs = new FileWrite();
                    byte[] lrcData = null;
                    if (Config.configValue.option_IgnoreFile && checkMusicExits(item.Value))
                    {
                        listView_Music.Items[item.Key].SubItems[1].Text = "略过";
                    }
                    else
                    {
                        // 下载歌词
                        if (lrcDown.Down(item.Value, ref lrcData,Config.configValue.option_ThreadNumber) == true)
                        {
                            listView_Music.Items[item.Key].SubItems[1].Text = "成功";
                            if (fs.Write(ref lrcData, item.Value, Config.configValue.option_Encoding, Config.configValue.option_UserDirectory))
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

        // 检测lrc文件是否存在
        private bool checkMusicExits(string musicPath)
        {
            return File.Exists(Path.GetDirectoryName(musicPath) + "\\" + Path.GetFileNameWithoutExtension(musicPath) + ".lrc");
        }

        // ui重置
        private void uiRest()
        {
            toolStripStatusLabel_Information.Text = string.Format("下载完成，总文件：{0}成功：{1} 失败{2}...", LongLife.MusicPathList.Count, LongLife.MusicPathList.Count - LongLife.MusicPathFailedList.Count, LongLife.MusicPathFailedList.Count);
            toolStripProgressBar_DownLoad.Value = 0;
            toolStripProgressBar_DownLoad.Maximum = 0;
            toolStripButton_DownLoad.Enabled = true;
            toolStripButton_Search.Enabled = true;
        }

        #region 应用程序设置
        /// <summary>
        /// 应用程序设置
        /// </summary>
        private void applicationSet()
        {
            // 允许跨线程操作控件
            CheckForIllegalCrossThreadCalls = false;
            // 设定网络最大并发链接数目
            System.Net.ServicePointManager.DefaultConnectionLimit = Config.configValue.option_ThreadNumber;
        }
        #endregion

        #region 更新检测
        /// <summary>
        /// 更新检测
        /// </summary>
        private void updateCheck()
        {
            if (Config.configValue.option_Update)
            {
                new Thread(() =>
                {
                    var result = new NetUtils().Http_Get("http://myzony.com/update.txt", Encoding.UTF8);
                    if (result != null)
                    {
                        var resultAray = result.Split(',');
                        if (int.Parse(resultAray[0]) > Ver.CurrentVersion)
                        {
                            if (MessageBox.Show("检测到新版本，是否下载新版本？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                            {
                                System.Diagnostics.Process.Start(resultAray[1]);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("与更新服务器网络连接异常。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }).Start();
            }
        }
        #endregion

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

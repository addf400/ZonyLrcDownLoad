using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing; 
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Collections;
using ID3;

namespace Zony_Lrc_Download_2._0
{
    public partial class Lrc_Main : Form
    {
        public Lrc_Main()
        {
            InitializeComponent();
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://jq.qq.com/?_wv=1027&k=Zrl68q");   
        }
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new About().ShowDialog();
        }

        private void Button_SelectDirectory_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder_Lrc = new FolderBrowserDialog();

            folder_Lrc.Description = "请选择歌曲所在的文件夹：";
            folder_Lrc.ShowDialog();
            LrcPath = folder_Lrc.SelectedPath;
            
            if(LrcPath == "")
            {
                MessageBox.Show(null,"请选择正确的文件夹路径！","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                Log.WriteLog(Log.Class.INFO,"文件夹选择错误。");
            }
            else
            {
                toolStripStatusLabel1.Text = "正在扫描......";
                Button_SelectDirectory.Enabled = false;

                Thread Search = new Thread(SearchFile);
                Search.Start();

                Log.WriteLog(Log.Class.INFO,"扫描线程启动，线程ID："+Search.ManagedThreadId.ToString());
            }

        }
       private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
       {
           new About().Show();
       }
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
       {
           if (WindowState == FormWindowState.Minimized)
           {
               //还原窗体显示 
               WindowState = FormWindowState.Normal;
               //激活窗体并给予它焦点 
               this.Activate();
               //任务栏区显示图标 
               this.ShowInTaskbar = true;
           }
       }
        private void Lrc_Main_Load(object sender, EventArgs e)
        {
            // 允许非安全线程代码
            Control.CheckForIllegalCrossThreadCalls = false;
            // 加载图标
            this.Icon = Zony_Lrc_Download_2._0.Resource1._6;
            if(!File.Exists(Environment.CurrentDirectory+@"\log.txt"))
            {
                var temp = File.Open(Environment.CurrentDirectory + @"\log.txt", FileMode.Create);
                temp.Close();
            }
            // 初始化LOG类
            Log.init_Log();
        }
        private void Lrc_Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 销毁托盘图标
            notifyIcon1.Dispose();
            Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 设置最大并行链接数
            System.Net.ServicePointManager.DefaultConnectionLimit = int.Parse(textBox1.Text);
            toolStripStatusLabel1.Text = "下载歌词......";
            Thread Down = new Thread(DownLoadLrc);
            Down.Start();

            Log.WriteLog(Log.Class.INFO,"歌词下载线程启动，线程ID：" + Down.ManagedThreadId.ToString());
        }

        private void LrcListItem_Click(object sender, EventArgs e)
        {
            if(LrcListItem.SelectedItems.Count>0)
            {
                try
                {
                    label5.Text = "歌曲路径:" + m_ThreadDownLoadList[LrcListItem.SelectedItems[0].Index];

                    ID3Info id3 = new ID3Info(m_ThreadDownLoadList[LrcListItem.SelectedItems[0].Index], true);

                    label2.Text = id3.ID3v1Info.Title != "" ? "歌曲名称:" + id3.ID3v1Info.Title : "歌曲名称:" + id3.ID3v2Info.GetTextFrame("TIT2");
                    label1.Text = id3.ID3v1Info.Artist != "" ? "歌手:" + id3.ID3v1Info.Artist : "歌手:" + id3.ID3v2Info.GetTextFrame("TPE1");
                }
                catch(System.OverflowException)
                {
                    label2.Text = "歌曲名称:" + "none";
                    label1.Text = "歌手:" + "none";
                }catch(Exception exp)
                {
                    Log.WriteLog(Log.Class.INFO,exp.ToString());
                }
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 销毁托盘图标
            notifyIcon1.Dispose();
            Environment.Exit(0);
        }

        private void Lrc_Main_SizeChanged(object sender, EventArgs e)
        {
            //判断是否选择的是最小化按钮 
            if (WindowState == FormWindowState.Minimized)
            {
                //隐藏任务栏区图标 
                this.ShowInTaskbar = false;
            }
        }

        private void 显示主窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                //还原窗体显示 
                WindowState = FormWindowState.Normal;
                //激活窗体并给予它焦点 
                this.Activate();
                //任务栏区显示图标 
                this.ShowInTaskbar = true;
            }
        }

        // 对文本框进行限定，只能输入数字
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!Char.IsNumber(e.KeyChar) && e.KeyChar !=(char)8)
            {
                e.Handled = true;
            }
        }
    }
}

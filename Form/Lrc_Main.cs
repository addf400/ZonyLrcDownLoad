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
                MessageBox.Show("请选择正确的文件夹路径！","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
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
            this.Icon = Zony_Lrc_Download_2._0.Resource1._6;
            
            if(!File.Exists(Environment.CurrentDirectory+@"\log.txt"))
            {
                var temp = File.Open(Environment.CurrentDirectory + @"\log.txt", FileMode.Create);
                temp.Close();
            }

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
            System.Net.ServicePointManager.DefaultConnectionLimit = int.Parse(textBox_DL_ThreadNum.Text);
            toolStripStatusLabel1.Text = "下载歌词......";

            Thread Down = new Thread(DownLoadLrc);
            Down.Start();

            Log.WriteLog(Log.Class.INFO,"歌词下载线程启动，线程ID：" + Down.ManagedThreadId.ToString());
        }

        private void LrcListItem_Click(object sender, EventArgs e)
        {
            if(LrcListItem.SelectedItems.Count>0)
            {
                SongInfo info = new SongInfo();
                info.GetSongInfo(m_ThreadDownLoadList[LrcListItem.SelectedItems[0].Index]);

                Label_FilePath.Text = "歌曲路径:" + info.m_SongFilePath;
                Label_SongName.Text = "歌曲名称："+ info.m_SongName;
                Label_SongSinger.Text = "歌手：" + info.m_SongSinger;
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox_DownLoadPath.SelectedIndex == 1)
            {
                FolderBrowserDialog fb = new FolderBrowserDialog();

                fb.Description = "请选择歌词要下载到的目录：";
                fb.ShowDialog();

                if(fb.SelectedPath != "")
                {
                    DownLoadLrcPath = fb.SelectedPath;
                    toolStripStatusLabel1.Text = DownLoadLrcPath;
                }
            }
        }
    }
}

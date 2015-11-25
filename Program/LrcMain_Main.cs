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
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Zony_Lrc_Download_2._0
{
    public partial class Lrc_Main
    {
        private string LrcPath;// 搜索文件的路径
        /// <summary>
        /// 歌词保存路径列表
        /// </summary>
        private List<string> m_mp3Path = new List<string>();

        /// <summary>
        /// 文件搜索线程
        /// </summary>
        private void SearchFile()
        {
            // 清空数组与列表框
            m_mp3Path.Clear();
            LrcListItem.Items.Clear();

            // 搜索对象
            FileSearch search = new FileSearch();
            // 函数返回值
            FileSearchReturn FuncReturn;
            FuncReturn = search.SearchFile(ref m_mp3Path, LrcPath, "*.mp3");
            if(FuncReturn==FileSearchReturn.NORMAL)
            {
                // 设定进度条
                toolStripProgressBar1.Maximum = m_mp3Path.Count;
                foreach (string str in m_mp3Path)
                {
                    toolStripStatusLabel1.Text = str;
                    toolStripProgressBar1.Value++;

                    // listview条目
                    string[] str_listitem = { Path.GetFileNameWithoutExtension(str), "" };
                    LrcListItem.Items.Insert(LrcListItem.Items.Count, new ListViewItem(str_listitem));

                }
                MessageBox.Show("扫描完毕！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                toolStripStatusLabel1.Text = "一共扫描到：" + LrcListItem.Items.Count.ToString() + " 个文件。";
            }
            else if(FuncReturn==FileSearchReturn.NO_SEARCH_FILE)
            {
                MessageBox.Show("没有扫描到音乐文件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Button_SelectDirectory.Enabled = true;
            }else if(FuncReturn==FileSearchReturn.EXCEPTION)
            {
                MessageBox.Show("程序发生异常！","错误",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }

            // 进度条重置
            toolStripProgressBar1.Maximum = 0;
            toolStripProgressBar1.Value = 0;

            // 控件解锁
            button1.Enabled = true;
            Button_SelectDirectory.Enabled = true;
        }
        /// <summary>
        /// LRC歌词下载线程
        /// </summary>
        private void DownLoadLrc()
        {
            int increment = 0;
            // 设定进度条
            toolStripProgressBar1.Maximum = m_mp3Path.Count;
            toolStripProgressBar1.Value = 0;
            // 禁用控件
            Button_SelectDirectory.Enabled = false;
            button1.Enabled = false;

            // 下载对象
            LrcDownLoad lrcDown = new LrcDownLoad();

            // 单线程下载
            foreach(string mp3Path in m_mp3Path)
            {
                byte[] lrcData = null;
                // 下载歌词并返回
                if (lrcDown.DownLoad_Ex(mp3Path, ref lrcData) == DownLoadReturn.NORMAL)
                {
                    LrcListItem.Items[increment].SubItems[1].Text = "成功";
                    // 写入到文件
                    if (lrcDown.WriteFile(ref lrcData, mp3Path, 2) != DownLoadReturn.NORMAL)
                    {
                        LrcListItem.Items[increment].SubItems[1].Text = "失败";
                    }
                }
                else
                {
                    LrcListItem.Items[increment].SubItems[1].Text = "失败";
                }

                toolStripProgressBar1.Value++; increment++;
            }

            // 多线程下载歌词
//             Parallel.ForEach(m_mp3Path, (item) =>
//             {
//                 byte[] lrcData = null;
//                 // 下载歌词并返回
//                 if (lrcDown.DownLoad(item, ref lrcData) == DownLoadReturn.NORMAL)
//                 {
//                     LrcListItem.Items[increment].SubItems[1].Text = "成功";
//                     // 写入到文件
//                     if (lrcDown.WriteFile(ref lrcData, item, comboBox1.SelectedIndex) != DownLoadReturn.NORMAL)
//                     {
//                         LrcListItem.Items[increment].SubItems[1].Text = "失败";
//                     }
//                 }
//                 else
//                 {
//                     LrcListItem.Items[increment].SubItems[1].Text = "失败";
//                 }
// 
//                 toolStripProgressBar1.Value++; increment++;
//             });

            toolStripStatusLabel1.Text = "下载完成！";
            notifyIcon1.ShowBalloonTip(5000, "提示", "所有歌词已经下载完成！", ToolTipIcon.Info);

            // 初始化进度条
            toolStripProgressBar1.Maximum = 0;
            toolStripProgressBar1.Value = 0;

            // 启用控件
            button1.Enabled = true;
            Button_SelectDirectory.Enabled = true;
        }
    }


}
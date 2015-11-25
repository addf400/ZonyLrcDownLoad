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


namespace Zony_Lrc_Download_2._0
{
    public partial class Lrc_Main
    {
        private string LrcPath;// 搜索文件的路径
        /// <summary>
        /// 歌词保存路径列表
        /// </summary>
        private ArrayList m_lrcPath = new ArrayList();
        private const string BAIDULRC = "http://music.baidu.com/search/lrc?key=";
        private const string BAIDUMUSCI = "http://music.baidu.com";

        /// <summary>
        /// 文件搜索线程
        /// </summary>
        private void SearchFile()
        {
            LrcPath = LrcPath.Trim();
            if (LrcPath != "")
            {
                string[] files = Directory.GetFiles(LrcPath, "*.mp3", System.IO.SearchOption.AllDirectories);// 扫描目录下所有mp3文件
                
                // 非安全跨线程调用控件
                if (files.Length != 0)
                {
                    // 清空数组与列表框
                    m_lrcPath.Clear();
                    LrcListItem.Items.Clear();

                    toolStripProgressBar1.Maximum = files.Length;
                    
                    for (int i = 0; i < files.Length; i++)
                    {
                        toolStripStatusLabel1.Text = files[i];
                        toolStripProgressBar1.Value +=1;

                        // 加入容器
                        m_lrcPath.Add(files[i]);
                        string []str={Path.GetFileNameWithoutExtension(files[i]),""};
                        LrcListItem.Items.Insert(LrcListItem.Items.Count, new ListViewItem(str));
                        
                    }
                    
                    MessageBox.Show(null, "扫描完毕！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    toolStripStatusLabel1.Text = "一共扫描到：" + LrcListItem.Items.Count.ToString() + " 个文件。";

                    // 控件初始化操作
                    toolStripProgressBar1.Maximum = 0;
                    toolStripProgressBar1.Value = 0;

                    button1.Enabled = true;
                    Button_SelectDirectory.Enabled = true;
                }
                else
                {
                    MessageBox.Show(null,"没有扫描到音乐文件！","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    Button_SelectDirectory.Enabled = true;
                }
                #region 日志点
                Log.WriteLog("扫描线程完成。");
                #endregion
            }
        }
        /// <summary>
        /// LRC歌词下载线程
        /// </summary>
        private void DownLoadLrc()
        {
            int increment = 0;
            // 设定进度条
            toolStripProgressBar1.Maximum = m_lrcPath.Count;
            // 禁用控件
            Button_SelectDirectory.Enabled = false;
            button1.Enabled = false;

            // 下载对象
            LrcDownLoad lrcDown = new LrcDownLoad();

            // 开始下载
            foreach(string mp3Path in m_lrcPath)
            {
                toolStripProgressBar1.Value++; increment++;
                byte [] lrcData=null;
                // 下载歌词并返回
                if(lrcDown.DownLoad(mp3Path,ref lrcData)!=DownLoadReturn.NORMAL)
                {
                    continue;
                }
                // 写入到文件
                if(lrcDown.WriteFile(ref lrcData, mp3Path, comboBox1.SelectedIndex)!=DownLoadReturn.NORMAL)
                {
                    continue;
                }


                LrcListItem.Items[increment].SubItems[1].Text = "成功";
            }

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
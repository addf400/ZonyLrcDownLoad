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
        #region 全局对象
        /// <summary>
        /// 搜索文件的路径
        /// </summary>
        private string LrcPath;
        /// <summary>
        /// 并行下载列表
        /// </summary>
        private Dictionary<int, string> m_ThreadDownLoadList = new Dictionary<int, string>();
        /// <summary>
        /// 下载失败的歌曲
        /// </summary>
        private Dictionary<int, string> m_FailedList = new Dictionary<int, string>();
        #endregion

        /// <summary>
        /// 文件搜索线程
        /// </summary>
        private void SearchFile()
        {
            // 清空容器与列表框
            m_ThreadDownLoadList.Clear();
            m_FailedList.Clear();
            LrcListItem.Items.Clear();

            // 搜索对象
            FileSearch search = new FileSearch();
            // 函数返回值
            FileSearchReturn FuncReturn;
            FuncReturn = search.SearchFile(ref m_ThreadDownLoadList, LrcPath, "*.mp3");
            if (FuncReturn == FileSearchReturn.NORMAL)
            {
                // 设定进度条
                toolStripProgressBar1.Maximum = m_ThreadDownLoadList.Count;
                foreach (KeyValuePair<int, string> str in m_ThreadDownLoadList)
                {
                    toolStripStatusLabel1.Text = str.Value;
                    toolStripProgressBar1.Value++;

                    // listview条目
                    string[] str_listitem = { Path.GetFileNameWithoutExtension(str.Value), "" };
                    LrcListItem.Items.Insert(LrcListItem.Items.Count, new ListViewItem(str_listitem));

                }
                MessageBox.Show("扫描完毕！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                toolStripStatusLabel1.Text = "一共扫描到：" + LrcListItem.Items.Count.ToString() + " 个文件。";
            }
            else if (FuncReturn == FileSearchReturn.NO_SEARCH_FILE)
            {
                MessageBox.Show("没有扫描到音乐文件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Button_SelectDirectory.Enabled = true;
            }
            else if (FuncReturn == FileSearchReturn.EXCEPTION)
            {
                MessageBox.Show("程序发生异常！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            // 禁用控件
            Button_SelectDirectory.Enabled = false;
            button1.Enabled = false;

            // 下载对象
            BaiDuLrcDownLoad baidu = new BaiDuLrcDownLoad();
            CnLyricDownLoad cnlyric = new CnLyricDownLoad();
            WYLrcDownLoad wy = new WYLrcDownLoad();
            ParallelDownLoad(m_ThreadDownLoadList, "开始从CnLryic乐库下载...", cnlyric,false);
            ParallelDownLoad(m_FailedList, "开始从百度乐库下载...", baidu);
            ParallelDownLoad(m_FailedList, "开始从网易云乐库下载...", baidu);

            toolStripStatusLabel1.Text = "下载完成！";
            notifyIcon1.ShowBalloonTip(5000, "提示", "所有歌词已经下载完成！", ToolTipIcon.Info);

            // 初始化进度条
            toolStripProgressBar1.Maximum = 0;
            toolStripProgressBar1.Value = 0;

            // 启用控件
            button1.Enabled = true;
            Button_SelectDirectory.Enabled = true;
        }

        private void ParallelDownLoad(Dictionary<int, string> container, string info, ILrcDownLoad lrcDown, bool isFailed = true)
        {
            toolStripStatusLabel1.Text = info;
            toolStripProgressBar1.Value = 0;
            toolStripProgressBar1.Maximum = container.Count;

            Utils tool=new Utils();

            Parallel.ForEach(container, (item) => {
                byte[] lrcData = null;
                // 下载歌词并返回
                if (lrcDown.DownLoad(item.Value, ref lrcData) == DownLoadReturn.NORMAL)
                {
                    LrcListItem.Items[item.Key].SubItems[1].Text = "成功";
                    // 写入到文件
                    if (tool.WriteFile(ref lrcData, item.Value, comboBox1.SelectedIndex) != DownLoadReturn.NORMAL)
                    {
                        LrcListItem.Items[item.Key].SubItems[1].Text = "失败";
                    }
                    else
                    {
                        if (isFailed) m_FailedList.Remove(item.Key);
                    }
                }
                else
                {
                    LrcListItem.Items[item.Key].SubItems[1].Text = "失败";
                    m_FailedList.Add(item.Key, item.Value);
                }

                toolStripProgressBar1.Value++;
            });
        }
    }


}
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
                Log.WriteLog("扫描线程完成。\r\n");
                #endregion
            }
        }
        /// <summary>
        /// LRC歌词下载线程
        /// </summary>
        private void DownLoadLrc()
        {
            toolStripProgressBar1.Maximum = m_lrcPath.Count;
            // 禁用控件
            Button_SelectDirectory.Enabled = false;
            button1.Enabled = false;

            for (int i = 0; i < m_lrcPath.Count;i++ )
            {
                toolStripProgressBar1.Value += 1;

                string searchLrcPath = BAIDULRC + Path.GetFileNameWithoutExtension((string)m_lrcPath[i]);
                WebClient myWebClient = new WebClient();
                byte[] myDataBuffer = myWebClient.DownloadData(searchLrcPath);

                // 以UTF-8 编码获取网页内容
                string HTMLString = Encoding.UTF8.GetString(myDataBuffer);
                if(HTMLString == "")
                {
                    LrcListItem.Items[i].SubItems[1].Text = "失败";
                    #region 日志点
                    Log.WriteLog(LrcListItem.Items[i].SubItems[1].Text+"  下载失败..." + "\r\n");
                    #endregion
                    break;
                }
                
                // 正则匹配
                Regex reg = new Regex(@"/data2.*.lrc");
                try
                {
                    string result = reg.Match(HTMLString).ToString();
                    if(result=="")
                    {
                        LrcListItem.Items[i].SubItems[1].Text = "失败";
                        #region 日志点
                        Log.WriteLog(LrcListItem.Items[i].SubItems[1].Text + "  下载失败..." + "\r\n");
                        #endregion
                        continue;
                    }
                    byte[] myDataBuffer2 = myWebClient.DownloadData(BAIDUMUSCI + result);
                    // 写到文件

                    FileStream f1 = new FileStream(Path.GetDirectoryName((string)m_lrcPath[i]) + "\\" + Path.GetFileNameWithoutExtension((string)m_lrcPath[i]) + ".lrc", FileMode.Create);
                    
                    // 编码转换
                    switch (comboBox1.SelectedIndex)
                    {
                        case 0:
                            {
                                myDataBuffer2 = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding("gb2312"), myDataBuffer2);
                                break;
                            }
                        case 1:
                            {
                                myDataBuffer2 = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding("GBK"), myDataBuffer2);
                                break;
                            }
                        case 2:
                            {
                                // UTF-8
                                break;
                            }
                        case 3:
                            {
                                myDataBuffer2 = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding("BIG5"), myDataBuffer2);
                                break;
                            }
                        case 4:
                            {
                                myDataBuffer2 = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding("shift_jis"), myDataBuffer2);
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                    f1.Write(myDataBuffer2, 0, myDataBuffer2.Length);
                    f1.Close();
                    LrcListItem.Items[i].SubItems[1].Text = "成功";
                }
                catch (Exception ex)
                {
                    #region 日志点
                    Log.WriteLog("发生异常：" + ex.ToString()+ "\r\n");
                    #endregion
                    MessageBox.Show(ex.ToString());
                    break;
                }
                
            }

            toolStripStatusLabel1.Text = "下载完成！";
            notifyIcon1.ShowBalloonTip(5000, "提示", "所有歌词已经下载完成！", ToolTipIcon.Info);

            // 控件初始化操作
            toolStripProgressBar1.Maximum = 0;
            toolStripProgressBar1.Value = 0;

            button1.Enabled = true;
            Button_SelectDirectory.Enabled = true;
        }
        /// <summary>
        /// 检测更新
        /// </summary>
        private void UpDataProgram()
        {
            
        }
    }


}
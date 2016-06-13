using System;
using System.Text;
using LibNet;
using LibIPlug;
using System.Net;
using System.Text.RegularExpressions;
using ID3;
using System.IO;
using System.Windows.Forms;

namespace LibLrcgc
{
    [PluginInfoAttribute("Lrcgc歌词源","v1.0","Zony","支持从lrcgc.com抓取歌词",0)]
    public class Lrcgc : IPlugin
    {
        public PluginInfoAttribute PluginInfo { get; set; }

        public bool Down(string filePath, ref byte[] lrcData, int iThread)
        {
            ServicePointManager.DefaultConnectionLimit = iThread;
            string mainDomain = @"http://www.lrcgc.com/";
            var m_tools = new NetUtils();

            try
            {
                // 获得歌词网页列表
                string getPageLink = mainDomain + "so/?q=" + m_tools.URL_Encoding(new MusicInfo(filePath,0).Title, Encoding.UTF8);
                string lrcHtml = m_tools.Http_Get(getPageLink, Encoding.UTF8);
                
                // 获得第一个歌词页面
                if (lrcHtml == "") return false;
                Regex reg = new Regex(@"lyric-\d+-\d+.html");
                string page = reg.Match(lrcHtml).ToString();
                if (page == "") return false;
                // 筛选ID
                Regex regId = new Regex(@"\d+-\d+");
                string id = regId.Match(page).ToString();
                if (id == "") return false;

                // 获得歌词具体下载地址
                string getPage = m_tools.Http_Get(mainDomain + page, Encoding.UTF8);
                Regex reg2 = new Regex(@"lrc-" + id + @"/.*.lrc\"">");
                string lrcURL = reg2.Match(getPage).ToString();

                // 下载歌词
                if (lrcURL == "") return false;
                lrcData = new WebClient().DownloadData(mainDomain + lrcURL.Remove(lrcURL.Length - 2, 2));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        class MusicInfo
        {
            #region 歌曲信息字段
            /// <summary>
            /// 歌曲标题
            /// </summary>
            public string Title { get; set; }
            /// <summary>
            /// 歌手名称
            /// </summary>
            public string Singer { get; set; }
            /// <summary>
            /// 歌曲路径
            /// </summary>
            public string MusicPath { get; set; }
            #endregion

            /// <summary>
            /// 获得歌曲文件信息
            /// </summary>
            /// <param name="filePath">源歌曲文件路径</param>
            /// <param name="flag">标志位，根据设置而定</param>
            public MusicInfo(string filePath, int flag)
            {
                try
                {
                    // 尝试获取ID3标签
                    ID3Info id3 = new ID3Info(filePath, true);
                    Title = id3.ID3v1Info.Title != null ? id3.ID3v1Info.Title : id3.ID3v2Info.GetTextFrame("TIT2");
                    Singer = id3.ID3v1Info.Artist != null ? id3.ID3v1Info.Artist : id3.ID3v2Info.GetTextFrame("TPE1");
                    if (Title == "")
                    {
                        // 尝试手工分割文件名
                        string fileName = Path.GetFileNameWithoutExtension(filePath);
                        Title = fileName;
                        Singer = fileName;
                    }
                }
                catch (Exception ex)
                {
                    // 尝试手工分割文件名
                    string fileName = Path.GetFileNameWithoutExtension(filePath);
                    Title = fileName;
                    Singer = fileName;
                }

                MusicPath = filePath;
            }
        }
    }
}

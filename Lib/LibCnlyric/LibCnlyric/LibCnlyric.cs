using System;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using LibNet;
using LibIPlug;
using ID3;

namespace LibCnlyric
{

    [PluginInfo("Cnlyric歌词源","v1.0","Zony","从Cnlyric下载歌词。",0)]
    public class Cnlyric : IPlugin
    {
        public PluginInfoAttribute PluginInfo { get; set; }

        public bool Down(string filePath,ref byte[] lrcData,int iThread)
        {
            System.Net.ServicePointManager.DefaultConnectionLimit = iThread;
            string RequestURL = "http://www.cnlyric.com/search.php?k=";
            string ResponseURL = "http://www.cnlyric.com/";
            var m_tools = new NetUtils();
            var m_info = new MusicInfo(filePath, 0);

            string getURL = RequestURL + m_tools.URL_Encoding(m_info.Title, Encoding.GetEncoding("gb2312")) + "&t=s";
            string lrcHtml = m_tools.Http_Get(getURL, Encoding.GetEncoding("gb2312"));

            if (lrcHtml == "") return false;

            Regex reg = new Regex(@"LrcDown/\d*/\d*.lrc");
            try
            {
                string result = reg.Match(lrcHtml).ToString();
                if (result == "") return false;

                byte[] gb2312Bytes = new WebClient().DownloadData(ResponseURL + result);
                // 编码统一转换为UTF-8
                lrcData = Encoding.Convert(Encoding.GetEncoding("gb2312"), Encoding.UTF8, gb2312Bytes);
                return true;
            }
            catch (Exception exp)
            {
                return false;
            }
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

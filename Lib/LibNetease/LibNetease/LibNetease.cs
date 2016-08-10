using LibNet;
using LibIPlug;
using Newtonsoft.Json.Linq;
using System.Text;
using System;
using ID3;
using System.IO;
using System.Text.RegularExpressions;

namespace LibNetease
{
    [PluginInfoAttribute("网易云音乐","v1.2","Zony","支持从网易云音乐下载歌词。",0)]
    public class Netease : IPlugin
    {
        public PluginInfoAttribute PluginInfo
        {
            get;set;
        }

        public bool Down(string filePath, ref byte[] lrcData,int iThread)
        {
            System.Net.ServicePointManager.DefaultConnectionLimit = iThread;
            string RequestURL = "http://music.163.com/api/search/get/web?csrf_token=";
            string ResponseURL = "http://music.163.com/api/song/lyric?os=osx&id=";
            var m_tools = new NetUtils();
            var m_info = new MusicInfo(filePath);

            try
            {
                string postStr = "&s=" + m_tools.URL_Encoding(m_info.Title, Encoding.UTF8) + "&type=1&offset=0&total=true&limit=5";
                string resultJson = m_tools.Http_Post(RequestURL, postStr);

                if (resultJson == "") return false;

                // 获得歌曲ID
                JObject jsonSID = JObject.Parse(resultJson);
                JArray jsonArraySID = (JArray)jsonSID["result"]["songs"];
                string sid = null;

                // 二级匹配
                bool flag = false;
                foreach (var item in jsonArraySID)
                {
                    if (item["artists"][0]["name"].ToString() == m_info.Singer)
                    {
                        sid = item["id"].ToString();
                        flag = true;
                    }
                }
                if(!flag) sid = jsonArraySID[0]["id"].ToString();


                string lrcUrl = ResponseURL + sid + "&lv=-1&kv=-1&tv=-1";
                string lrcString = m_tools.Http_Get(lrcUrl, Encoding.UTF8, true);

                // 从Json当中分析歌词信息
                JObject lrcObject = JObject.Parse(lrcString);
                string lrcDataString = lrcObject["lrc"]["lyric"].ToString();
                if (lrcDataString == "") return false;
                // 去掉网易时间轴后一位
                Regex reg = new Regex(@"\[\d+:\d+.\d+\]");
                string deal_ok = reg.Replace(lrcDataString, new MatchEvaluator((Match machs)=> 
                {
                    string time = machs.ToString();
                    return time.Remove(time.Length - 2, 1);
                }));

                // 将字符串转换为字节流进行存储
                lrcData = Encoding.UTF8.GetBytes(deal_ok);
                return true;

            }
            catch (Exception)
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
        public MusicInfo(string filePath)
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
            catch (Exception)
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Zony_Lrc_Download_2._0
{
    public class WYLrcDownLoad : ILrcDownLoad
    {
        private const string WangYiGetID = "http://music.163.com/api/search/get/web?csrf_token=";
        private const string WangYiLrcURL = "http://music.163.com/api/song/lyric?os=osx&id=";
        
        private Utils m_Tool = new Utils();
        private SongInfo m_Info = new SongInfo();

        /// <summary>
        /// 歌词下载函数
        /// </summary>
        /// <param name="filename">歌曲完整路径</param>
        /// <param name="filedata">返回的歌词数据</param>
        /// <returns>状态</returns>
        public DownLoadReturn DownLoad(string filepath, ref byte[] filedata)
        {
            m_Info.GetSongInfo(filepath);
            string t_songName = m_Info.m_SongName;

            try
            {
                string m_PostStr = "&s=" + m_Tool.URL_ENCODING(t_songName, Encoding.UTF8) + "&type=1&offset=0&total=true&limit=5";
                string returnJson = m_Tool.Http_Post(WangYiGetID, m_PostStr);
                if("".Equals(returnJson) || returnJson == "")
                {
                    Log.WriteLog(Log.Class.ERROR, t_songName, "无法从网易云音乐获取json反馈信息。");
                    return DownLoadReturn.HTML_INVALID;
                }
                JObject JsonSID = JObject.Parse(returnJson);
                JArray JsonArraySID = (JArray)JsonSID["result"]["songs"];
                string sid = JsonArraySID[0]["id"].ToString();

                string m_DownLrcURL = WangYiLrcURL + sid + "&lv=-1&kv=-1&tv=-1";
                string lrcString = m_Tool.Http_Get(m_DownLrcURL, Encoding.UTF8, true);
                JObject lrcObjet = JObject.Parse(lrcString);
                string result = lrcObjet["lrc"]["lyric"].ToString();
                if ("".Equals(result) || result == "")
                {
                    return DownLoadReturn.HTML_INVALID;
                }

                filedata = Encoding.UTF8.GetBytes(result);
                return DownLoadReturn.NORMAL;
            }
            catch (Exception exp)
            {
                Log.WriteLog(Log.Class.EXCEPTION,t_songName, "发生异常：" + exp.ToString());
                return DownLoadReturn.EXCEPTION;
            }
        }
    }
}

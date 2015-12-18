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

        /// <summary>
        /// 歌词下载函数
        /// </summary>
        /// <param name="filename">歌曲完整路径</param>
        /// <param name="filedata">返回的歌词数据</param>
        /// <returns>状态</returns>
        public DownLoadReturn DownLoad(string filepath, ref byte[] filedata)
        {
            string t_songName = Path.GetFileNameWithoutExtension(filepath);
            try
            {
                string m_PostStr = "&s=" + m_Tool.URL_ENCODING(t_songName, Encoding.UTF8) + "&type=1&offset=0&total=true&limit=5";
                string returnJson = m_Tool.Http_Post(WangYiGetID, m_PostStr);
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
                #region 日志点
                Log.WriteLog(t_songName, "发生异常：" + exp.ToString());
                #endregion
                /*throw (exp); 并不抛出，直接返回异常*/
                return DownLoadReturn.EXCEPTION;
            }
        }
    }
}

/*
 * 描述：定义了各种歌词下载器对象。
 * 作者：Zony
 * 创建日期：2016/05/06
 * 最后修改日期：2016/05/06
 * 版本：1.1
 */
using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Text;
using Zony_Lrc_Download_2._0.Class.Music;
using Zony_Lrc_Download_2._0.Class.Configs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Zony_Lrc_Download_2._0.Class.Utils.DownLoad
{
    abstract class LrcDownLoad
    {
        /// <summary>
        /// Down函数返回值
        /// </summary>
        public enum DownLoadResult
        {
            /// <summary>
            /// 函数正常
            /// </summary>
            NORMAL = 0,
            /// <summary>
            /// HTML页面无效/未找到页面
            /// </summary>
            ERR_HTML_INVALID = -1,
            /// <summary>
            /// 发生异常
            /// </summary>
            ERR_EXCEPTION = -2,
            /// <summary>
            /// 没有合适的匹配项
            /// </summary>
            ERR_REGEX_NO_FIND = -3,
            /// <summary>
            /// 网络不通
            /// </summary>
            ERR_INTERNET = -4
        }

        protected MusicInfo m_info = null;
        protected Tools m_tools = new Tools();

        protected string RequestURL = null;
        protected string ResponseURL = null;

        /// <summary>
        /// 下载歌词
        /// </summary>
        /// <param name="filePath">源歌曲路径</param>
        /// <param name="lrcData">歌词数据引用</param>
        /// <returns>歌词下载状态</returns>
        public abstract DownLoadResult Down(string filePath, ref byte[] lrcData);
    }

    class LrcDownLoad_Baidu : LrcDownLoad
    {
        public override DownLoadResult Down(string filePath, ref byte[] lrcData)
        {
            RequestURL = "http://music.baidu.com/search/lrc?key=";
            ResponseURL = "http://music.baidu.com";

            m_info = new MusicInfo(filePath, 0);
            string getURL = RequestURL + m_info.Title;
            string lrcHtml = m_tools.Http_Get(getURL, Encoding.UTF8);

            if (lrcHtml == "") return DownLoadResult.ERR_HTML_INVALID;

            Regex reg = new Regex(@"/data2/lrc/\d*/\d*.lrc");
            try
            {
                string result = reg.Match(lrcHtml).ToString();
                if (result == "") return DownLoadResult.ERR_REGEX_NO_FIND;

                lrcData = new WebClient().DownloadData(ResponseURL + result);
                return DownLoadResult.NORMAL;
            }
            catch (Exception exp)
            {
                return DownLoadResult.ERR_EXCEPTION;
            }
        }
    }

    // 禁用
    class LrcDownLoad_Cnlyric : LrcDownLoad
    {
        public override DownLoadResult Down(string filePath, ref byte[] lrcData)
        {
            RequestURL = "http://www.cnlyric.com/search.php?k=";
            ResponseURL = "http://www.cnlyric.com/";

            m_info = new MusicInfo(filePath, 0);
            string getURL = RequestURL + m_tools.URL_Encoding(m_info.Title, Encoding.GetEncoding("gb2312")) + "&t=s";
            string lrcHtml = m_tools.Http_Get(getURL, Encoding.GetEncoding("gb2312"));

            if (lrcHtml == "") return DownLoadResult.ERR_HTML_INVALID;

            Regex reg = new Regex(@"LrcDown/\d*/\d*.lrc");
            try
            {
                string result = reg.Match(lrcHtml).ToString();
                if (result == "") return DownLoadResult.ERR_REGEX_NO_FIND;

                byte[] gb2312Bytes = new WebClient().DownloadData(ResponseURL + result);
                // 编码统一转换为UTF-8
                lrcData = Encoding.Convert(Encoding.GetEncoding("gb2312"), Encoding.UTF8, gb2312Bytes);
                return DownLoadResult.NORMAL;
            }
            catch (Exception exp)
            {
                return DownLoadResult.ERR_EXCEPTION;
            }
        }
    }

    class LrcDownLoad_NetEase : LrcDownLoad
    {
        public override DownLoadResult Down(string filePath, ref byte[] lrcData)
        {
            RequestURL = "http://music.163.com/api/search/get/web?csrf_token=";
            ResponseURL = "http://music.163.com/api/song/lyric?os=osx&id=";

            m_info = new MusicInfo(filePath, 0);

            try
            {
                string postStr = "&s=" + m_tools.URL_Encoding(m_info.Title, Encoding.UTF8) + "&type=1&offset=0&total=true&limit=5";
                string resultJson = m_tools.Http_Post(RequestURL, postStr);

                if (resultJson == "") return DownLoadResult.ERR_HTML_INVALID;

                // 获得歌曲ID
                JObject jsonSID = JObject.Parse(resultJson);
                JArray jsonArraySID = (JArray)jsonSID["result"]["songs"];
                string sid = jsonArraySID[0]["id"].ToString();

                string lrcUrl = ResponseURL + sid + "&lv=-1&kv=-1&tv=-1";
                string lrcString = m_tools.Http_Get(lrcUrl, Encoding.UTF8, true);

                // 从Json当中分析歌词信息
                JObject lrcObject = JObject.Parse(lrcString);
                string lrcDataString = lrcObject["lrc"]["lyric"].ToString();
                if (lrcDataString == "") return DownLoadResult.ERR_HTML_INVALID;
                // 将字符串转换为字节流进行存储
                lrcData = Encoding.UTF8.GetBytes(lrcDataString);
                return DownLoadResult.NORMAL;
                
            }catch(Exception exp)
            {
                return DownLoadResult.ERR_EXCEPTION;
            }
        }
    }
}

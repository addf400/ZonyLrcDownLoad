using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Text.RegularExpressions;
using System.Net;

namespace Zony_Lrc_Download_2._0
{
    public class BaiDuLrcDownLoad : ILrcDownLoad
    {
        private const string BAIDULRC = "http://music.baidu.com/search/lrc?key=";
        private const string BAIDUMUSCI = "http://music.baidu.com";

        private Utils m_Tool= new Utils();

        /// <summary>
        /// 歌词下载函数
        /// </summary>
        /// <param name="filename">歌曲完整路径</param>
        /// <param name="filedata">返回的歌词数据</param>
        /// <returns>状态</returns>
        public DownLoadReturn DownLoad(string filepath,ref byte[] filedata)
        {
            string t_songName = Path.GetFileNameWithoutExtension(filepath);
            string m_strSearchURL = BAIDULRC + t_songName;

            string lrcHtmlString = m_Tool.Http_Get(m_strSearchURL, Encoding.UTF8);
            if ("".Equals(lrcHtmlString) || lrcHtmlString == "")
            {
                #region 日志点
                Log.WriteLog(t_songName, "在DownLoad函数中发生：HTML页面数据为空。");
                #endregion
                return DownLoadReturn.HTML_INVALID;
            }
            //正则搜寻下载链接
            Regex reg = new Regex(@"/data2/lrc/\d*/\d*.lrc");
            try
            {
                string result = reg.Match(lrcHtmlString).ToString();
                if (result == "" || "".Equals(result))
                {
                    #region 日志点
                    Log.WriteLog(t_songName, "在DownLoad函数中发生：百度乐库没有结果。");
                    #endregion
                    return DownLoadReturn.REGEX_ERROR;
                }

                // 获得LRC文件数据
                filedata = new WebClient().DownloadData(BAIDUMUSCI + result);

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

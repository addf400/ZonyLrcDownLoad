﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Text.RegularExpressions;
using System.Net;
using System.IO;

namespace Zony_Lrc_Download_2._0
{
    public class CnLyricDownLoad : ILrcDownLoad
    {
        private const string CNLYRIC = "http://www.cnlyric.com/search.php?k=";
        private const string CnLyricDown = "http://www.cnlyric.com/";

        private Utils m_Tool = new Utils();

        /// <summary>
        /// 歌词下载函数
        /// </summary>
        /// <param name="filename">歌曲完整路径</param>
        /// <param name="filedata">返回的歌词数据</param>
        /// <returns>状态</returns>
        public DownLoadReturn DownLoad(string filepath,ref byte[] filedata)
        {
            string t_songName = Path.GetFileNameWithoutExtension(filepath);
            string m_strSearchURL = CNLYRIC + m_Tool.URL_ENCODING(t_songName, Encoding.GetEncoding("gb2312")) + "&t=s";

            string lrcHtmlString = m_Tool.Http_Get(m_strSearchURL, Encoding.GetEncoding("gb2312"));

            if ("".Equals(lrcHtmlString) || lrcHtmlString == "")
            {
                #region 日志点
                Log.WriteLog(t_songName, "在DownLoadEx函数中发生：HTML页面数据为空。");
                #endregion
                return DownLoadReturn.HTML_INVALID;
            }

            //正则搜寻下载链接
            Regex reg = new Regex(@"LrcDown/\d*/\d*.lrc");
            try
            {
                string result = reg.Match(lrcHtmlString).ToString();
                if (result == "" || "".Equals(result))
                {
                    #region 日志点
                    Log.WriteLog(t_songName, "在DownLoadEx函数中发生：Cnlryic没有结果。");
                    #endregion
                    return DownLoadReturn.REGEX_ERROR;
                }

                // 获得LRC文件数据
                byte[] gb2312Bytes = new WebClient().DownloadData(CnLyricDown + result);
                // 编码统一转换为UTF-8
                filedata = Encoding.Convert(Encoding.GetEncoding("gb2312"), Encoding.UTF8, gb2312Bytes);

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
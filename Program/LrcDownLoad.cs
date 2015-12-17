using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace Zony_Lrc_Download_2._0
{
    /// <summary>
    /// 函数状态返回值
    /// </summary>
    public enum DownLoadReturn
    {
        /// <summary>
        /// 正常返回
        /// </summary>
        NORMAL=0,
        /// <summary>
        /// HTML页面无数据
        /// </summary>
        HTML_INVALID=-1,
        /// <summary>
        /// 发生异常
        /// </summary>
        EXCEPTION=-2,
        /// <summary>
        /// 搜寻下载链接失败
        /// </summary>
        REGEX_ERROR=-3,
        /// <summary>
        /// 文件创建失败
        /// </summary>
        FILE_CREAT_ERROR=-4
    }

    /// <summary>
    /// 百度歌词下载类
    /// </summary>
    public class BaiDuLrcDownLoad 
    {
        private const string BAIDULRC = "http://music.baidu.com/search/lrc?key=";
        private const string BAIDUMUSCI = "http://music.baidu.com";

        /// <summary>
        /// 歌词下载函数
        /// </summary>
        /// <param name="filepath">歌曲完整路径</param>
        /// <param name="filedata">下载回来的数据</param>
        /// <returns>状态</returns>
        public DownLoadReturn DownLoad(string filepath, ref byte[] filedata)
        {
            string t_songName = Path.GetFileNameWithoutExtension(filepath);
            string m_strSearchURL = BAIDULRC + t_songName;

            string lrcHtmlString = Utils.Http_Get(filepath, Encoding.UTF8);
            if("".Equals(lrcHtmlString) || lrcHtmlString == "")
            {
                #region 日志点
                Log.WriteLog(t_songName, "在DownLoad函数中发生：HTML页面数据为空。");
                #endregion
                return DownLoadReturn.HTML_INVALID;
            }

            // 正则表达式搜寻下载链接
            Regex reg = new Regex(@"/data2/lrc/\d*/\d*.lrc");
            try
            {
                string reslut = reg.Match(lrcHtmlString).ToString();
                if("".Equals(lrcHtmlString) || lrcHtmlString == "")
                {
                    #region 日志点
                    Log.WriteLog(t_songName,"在DownLoad函数中发生：百度乐库没有结果。");
                    #endregion
                    return DownLoadReturn.REGEX_ERROR;
                }
                // 获得LRC文件数据
                filedata = new WebClient().DownloadData(BAIDUMUSCI + reslut);
                return DownLoadReturn.NORMAL;
            }catch(Exception exp)
            {
                #region 日志点
                Log.WriteLog(t_songName, "发生异常：" + exp.ToString());
                #endregion
                return DownLoadReturn.EXCEPTION;
            }
        }
    }

    public class CnLryicDownLoad
    {
        private const string CNLYRIC = "http://www.cnlyric.com/search.php?k=";
        private const string CnLyricDown = "http://www.cnlyric.com/";

        /// <summary>
        /// 歌词下载函数
        /// </summary>
        /// <param name="filepath">歌曲完整路径</param>
        /// <param name="filedata">下载回来的数据</param>
        /// <returns>状态</returns>
        public DownLoadReturn DownLoad(string filepath,ref byte[] filedata)
        {
            string t_songName = Path.GetFileNameWithoutExtension(filepath);
            string m_strSearchURL = CNLYRIC + Utils.URL_GB2312_ENCODING(t_songName) + "&t=s";

            string lrcHtmlString = Utils.Http_Get(m_strSearchURL, Encoding.GetEncoding("gb2312"));
            if("".Equals(lrcHtmlString) || lrcHtmlString == "")
            {
                #region 日志点
                Log.WriteLog(t_songName, "在DownLoadEx函数中发生：HTML页面数据为空。");
                #endregion
                return DownLoadReturn.HTML_INVALID;
            }
            // 正则搜寻下载链接
            Regex reg = new Regex(@"LrcDown/\d*/\d*.lrc");
            try
            {
                string result = reg.Match(lrcHtmlString).ToString();
                if("".Equals(result) || result == "")
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
            }catch(Exception exp)
            {
                #region 日志点
                Log.WriteLog(t_songName, "发生异常：" + exp.ToString());
                #endregion
                return DownLoadReturn.EXCEPTION;
            }
        }
    }
}

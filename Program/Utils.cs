using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Zony_Lrc_Download_2._0
{
    public static class Utils
    {
        /// <summary>
        /// Get操作
        /// </summary>
        /// <param name="url">要提交的URL地址</param>
        /// <returns>返回结果</returns>
        public static string Http_Get(string url, Encoding encode)
        {
            try
            {
                HttpWebRequest myReq = (HttpWebRequest)HttpWebRequest.Create(url);
                myReq.Method = "get";

                HttpWebResponse res = (HttpWebResponse)myReq.GetResponse();
                Stream s = res.GetResponseStream();

                StreamReader reader = new StreamReader(s, encode);
                StringBuilder responseData = new StringBuilder();
                String line;
                while ((line = reader.ReadLine()) != null)
                {
                    responseData.Append(line);
                }

                return responseData.ToString();
            }
            catch (Exception e)
            {
                #region 日志点
                Log.WriteLog("在函数Http_Get()当中发生异常：" + e.ToString());
                #endregion
                return "";
            }
        }

        /// <summary>
        /// URL 2312编码
        /// </summary>
        /// <param name="str">要编码的字符串</param>
        /// <returns>编码结果</returns>
        public static string URL_GB2312_ENCODING(string str)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byStr = Encoding.GetEncoding("gb2312").GetBytes(str);

            for (int i = 0; i < byStr.Length; i++)
            {
                sb.Append(@"%" + Convert.ToString(byStr[i], 16));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 将数据写入文件
        /// </summary>
        /// <param name="filedata">lrc文件数据</param>
        /// <param name="filepath">歌曲路径</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>状态</returns>
        public static DownLoadReturn WriteFile(ref byte[] filedata, string filepath, int encoding)
        {
            string t_songName = Path.GetFileNameWithoutExtension(filepath);
            try
            {
                string lrcPath = Path.GetDirectoryName(filepath) + "\\" + t_songName + ".lrc";
                FileStream lrcFileStream = new FileStream(lrcPath, FileMode.Create);

                if (!File.Exists(lrcPath))
                {
                    Log.WriteLog(t_songName, "歌词文件创建失败。");
                    return DownLoadReturn.FILE_CREAT_ERROR;
                }
                // 输出编码选择
                switch (encoding)
                {
                    case 0:
                        // 默认UTF-8
                        break;
                    case 1:
                        // gb2312
                        filedata = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding("gb2312"), filedata);
                        break;
                    case 2:
                        filedata = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding("gbk"), filedata);
                        break;
                    case 3:
                        filedata = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding("BIG5"), filedata);
                        break;
                    case 4:
                        filedata = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding("shift_jis"), filedata);
                        break;
                }

                lrcFileStream.Write(filedata, 0, filedata.Length);
                lrcFileStream.Close();
                return DownLoadReturn.NORMAL;
            }
            catch (Exception e)
            {
                #region 日志点
                Log.WriteLog(t_songName, "发生异常：" + e.ToString());
                #endregion
                /*throw (exp); 并不抛出，直接返回异常*/
                return DownLoadReturn.EXCEPTION;
            }
        }
    }
}

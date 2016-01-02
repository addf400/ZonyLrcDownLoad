using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.IO;

namespace Zony_Lrc_Download_2._0
{
    /// <summary>
    /// 歌词下载类绑定的工具类
    /// </summary>
    public class Utils
    {
        /// <summary>
        /// URL 2312编码
        /// </summary>
        /// <param name="str">要编码的字符串</param>
        /// <returns>编码结果</returns>
        public string URL_ENCODING(string str, Encoding encoding)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byStr = encoding.GetBytes(str);

            for (int i = 0; i < byStr.Length; i++)
            {
                sb.Append(@"%" + Convert.ToString(byStr[i], 16));
            }
            return sb.ToString();
        }
        /// <summary>
        /// Get操作
        /// </summary>
        /// <param name="url">要提交的URL地址</param>
        /// <returns>返回结果</returns>
        public string Http_Get(string url, Encoding encode, bool Is163 = false)
        {
            try
            {
                HttpWebRequest myReq = (HttpWebRequest)HttpWebRequest.Create(url);
                myReq.Method = "get";
                // 如果是网易云音乐
                if (Is163)
                {
                    myReq.Referer = "http://music.163.com/";
                    myReq.ContentType = "application/x-www-form-urlencoded";
                }

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
                Log.WriteLog(Log.Class.EXCEPTION,"在函数Http_Get()当中发生异常：" + e.ToString());
                return "";
            }

        }
        public string Http_Post(string url, string postStr)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            byte[] postData = Encoding.UTF8.GetBytes(postStr);

            req.Method = "post";
            req.Referer = "http://music.163.com/";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = postData.Length;

            Stream writeStream = req.GetRequestStream();
            writeStream.Write(postData, 0, postData.Length);
            writeStream.Close();

            // 发送请求
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
            // 返回结果
            string result = sr.ReadToEnd();
            return result;
        }

        /// <summary>
        /// 将数据写入文件
        /// </summary>
        /// <param name="filedata">lrc文件数据</param>
        /// <param name="filepath">歌曲路径</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>状态</returns>
        public DownLoadReturn WriteFile(ref byte[] filedata, string filepath, int encoding,string diyPath)
        {
            string t_songName = Path.GetFileNameWithoutExtension(filepath);
            try
            {
                string lrcPath=null;
                // 自定义路径
                if(diyPath!="None")
                {
                    lrcPath = diyPath + "\\" + t_songName + ".lrc";
                }else{
                    lrcPath = Path.GetDirectoryName(filepath) + "\\" + t_songName + ".lrc";
                }
                
                FileStream lrcFileStream = new FileStream(lrcPath, FileMode.Create);

                if (!File.Exists(lrcPath))
                {
                    Log.WriteLog(Log.Class.ERROR,t_songName, "歌词文件创建失败。");
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
                Log.WriteLog(Log.Class.EXCEPTION,t_songName, "发生异常：" + e.ToString());
                return DownLoadReturn.EXCEPTION;
            }
        }
        
    }
}

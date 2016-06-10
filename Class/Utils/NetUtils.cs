/*
 * 描述：提供了一些网络常用方法。
 * 作者：Zony
 * 创建日期：2016/05/04
 * 最后修改日期：2016/06/10
 * 版本：1.0
 */
using System;
using System.Text;
using System.Net;
using System.IO;
using System.Collections.Generic;

namespace Zony_Lrc_Download_2._0.Class.Utils.DownLoad
{
    /// <summary>
    /// 歌词下载类绑定工具类
    /// </summary>
    class NetUtils
    {
        #region 网页操作相关
        /// <summary>
        /// 将字符串进行URL编码
        /// </summary>
        /// <param name="str">要编码的字符串</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>URL编码完成后的字符串</returns>
        public string URL_Encoding(string str,Encoding encoding)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byStr = encoding.GetBytes(str);

            for(int i=0;i<byStr.Length;i++)
            {
                sb.Append(@"%" + Convert.ToString(byStr[i], 16));
            }

            return sb.ToString();
        }

        /// <summary>
        /// 对目标地址进行Get操作
        /// </summary>
        /// <param name="url">目标URL</param>
        /// <param name="encoding">编码方式</param>
        /// <param name="Is163">是否为网易云</param>
        /// <returns>获得的内容</returns>
        public string Http_Get(string url,Encoding encoding,bool Is163 = false)
        {
            try
            {
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);
                myReq.Method = "get";
                if (Is163)
                {
                    myReq.Referer = "http://music.163.com";
                    myReq.ContentType = "application/x-www-form-urlencoded";
                }

                HttpWebResponse myRes = (HttpWebResponse)myReq.GetResponse();
                Stream st = myRes.GetResponseStream();
                StreamReader sr = new StreamReader(st,encoding);
                StringBuilder sb = new StringBuilder();
                string line;

                while((line = sr.ReadLine()) != null)
                {
                    sb.Append(line);
                }

                return sb.ToString();
            }catch(WebException)
            {
                return null;
            }
        }

        /// <summary>
        /// 对目标地址进行Post操作，注意本函数为网易云音乐专有函数
        /// </summary>
        /// <param name="url">目标地址</param>
        /// <param name="postStr">提交的内容</param>
        /// <returns>获得的内容</returns>
        public string Http_Post(string url,string postStr)
        {
            try
            {
                var myReq = (HttpWebRequest)WebRequest.Create(url);
                byte[] byPost = Encoding.UTF8.GetBytes(postStr);
                myReq.Method = "post";
                myReq.Referer = "http://music.163.com";
                myReq.ContentType = "application/x-www-form-urlencoded";
                myReq.ContentLength = byPost.Length;

                Stream st = myReq.GetRequestStream();
                st.Write(byPost, 0, byPost.Length);
                st.Close();

                // 提交请求
                var myRes = (HttpWebResponse)myReq.GetResponse();
                StreamReader sr = new StreamReader(myRes.GetResponseStream(), Encoding.UTF8);

                string result = sr.ReadToEnd();
                return result;
            }catch(Exception)
            {
                return null;
            }
        }
        #endregion
    }
}

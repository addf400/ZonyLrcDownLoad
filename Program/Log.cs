﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Zony_Lrc_Download_2._0
{
    /// <summary>
    /// 日志操作类
    /// </summary>
    static class Log
    {
        static string CurrentDir;
        static FileStream logFileStream;
        static StreamWriter write;

        /// <summary>
        /// 日志级别
        /// </summary> 
        public enum Class
        {
            /// <summary>
            /// 信息
            /// </summary>
            INFO=0,
            /// <summary>
            /// 警告
            /// </summary>
            WARNING=1,
            /// <summary>
            /// 异常
            /// </summary>
            EXCEPTION=2,
            /// <summary>
            /// 错误
            /// </summary>
            ERROR=3
        }

        public static void init_Log()
        {
            // 检测日志文件是否存在
            if (!File.Exists(Environment.CurrentDirectory + @"\log.txt"))
            {
                var temp = File.Open(Environment.CurrentDirectory + @"\log.txt", FileMode.Create);
                temp.Close();
            }

            CurrentDir = Environment.CurrentDirectory;
            logFileStream = new FileStream(CurrentDir + @"\log.txt", FileMode.Append);
            write = new StreamWriter(logFileStream, Encoding.GetEncoding("utf-8"));
        }
        public static void WriteLog(Class cls, string information)
        {
            write.WriteLine(cls + "-" + DateTime.Now.ToString() + "-" + information);
        }
        public static void WriteLog(Class cls,string name, string info)
        {
            write.WriteLine(cls + "-" + DateTime.Now.ToString() + "-" + "歌曲：" + name + " " + info);
        }

        public static string LoadLog()
        {
            // 记得关闭流，否则会造成文件占用
            Close();

            FileStream logFileStream = new FileStream(CurrentDir + @"\log.txt", FileMode.Open);
            StreamReader read = new StreamReader(logFileStream, Encoding.UTF8);
            StringBuilder strBuilder = new StringBuilder();
            string str=null;
            
            // 从文件当中读取日志信息
            while ((str = read.ReadLine()) != null)
            {
                strBuilder.Append(str + "\r\n");
            }

            read.Close();
            logFileStream.Close();

            init_Log();
            return strBuilder.ToString();
        }

        public static void Close()
        {
            write.Close();
            logFileStream.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Zony_Lrc_Download_2._0
{
    static class Log
    {
        static string CurrentDir;
        static FileStream logFileStream;
        static StreamWriter write;
        public static void init_Log()
        {
            CurrentDir = Environment.CurrentDirectory;
            logFileStream = new FileStream(CurrentDir + "/log.txt", FileMode.Append);
            write = new StreamWriter(logFileStream, Encoding.GetEncoding("utf-8"));
        }
        public static void WriteLog(string information)
        {
            write.WriteLine(DateTime.Now.ToString() + "-" + information);
        }
        public static void WriteLog(string name, string info)
        {
            write.WriteLine(DateTime.Now.ToString() + "-" + "歌曲：" + name + " " + info);
        }

        public static string LoadLog()
        {
            Close();
            FileStream logFileStream = new FileStream(CurrentDir + "/log.txt", FileMode.Open);
            StreamReader read = new StreamReader(logFileStream, Encoding.UTF8);
            StringBuilder strBuilder = new StringBuilder();
            string str;
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

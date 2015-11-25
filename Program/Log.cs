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
        public static void init_Log()
        {
            CurrentDir= Environment.CurrentDirectory;
        }
        public static void WriteLog(string information)
        {
            FileStream logFileStream = new FileStream(CurrentDir + "/log.txt", FileMode.Append);
            StreamWriter write = new StreamWriter(logFileStream, Encoding.GetEncoding("utf-8"));
            write.WriteLine(DateTime.Now.ToString() + "-" + information);
            write.Close();
            logFileStream.Close();
        }
        public static void WriteLog(string name,string info)
        {
            FileStream logFileStream = new FileStream(CurrentDir + "/log.txt", FileMode.Append);
            StreamWriter write = new StreamWriter(logFileStream, Encoding.GetEncoding("utf-8"));
            write.WriteLine(DateTime.Now.ToString() + "-" + "歌曲："+ name + " " + info);
            write.Close();
            logFileStream.Close();
        }

        public static string LoadLog()
        {
            FileStream logFileStream = new FileStream(CurrentDir + "/log.txt", FileMode.Open);
            StreamReader read = new StreamReader(logFileStream, Encoding.UTF8);
            StringBuilder strBuilder = new StringBuilder();
            string str;
            while ((str = read.ReadLine()) != null)
            {
                strBuilder.Append(str+"\r\n");
            }
            read.Close();
            logFileStream.Close();
            return strBuilder.ToString();
        }

    }
}

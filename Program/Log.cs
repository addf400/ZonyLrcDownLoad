using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Zony_Lrc_Download_2._0
{
    static class Log
    {
        static string CurrentDir = Environment.CurrentDirectory;
        static FileStream logFileStream = new FileStream(CurrentDir + "/log.txt", FileMode.Append);
        static StreamWriter write = new StreamWriter(logFileStream, Encoding.GetEncoding("utf-8")); //统一使用UTF-8编码
        public static void WriteLog(string information)
        {
            write.WriteLine(DateTime.Now.ToString() + "-" + information+"\n");
        }
        public static void WriteLog(string name,string info)
        {
            write.WriteLine(DateTime.Now.ToString() + "-" + "歌曲："+ name + " " + info  + "\n");
        }

        public static void Close()
        {
            write.Close();
            logFileStream.Close();
        }
    }
}

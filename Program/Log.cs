using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Zony_Lrc_Download_2._0
{
    static class Log
    {
        public static void WriteLog(string information)
        {
            string CurrentDir = Environment.CurrentDirectory;
            FileStream logFileStream = new FileStream(CurrentDir+"/log.txt",FileMode.Append);
            
            StreamWriter write = new StreamWriter(logFileStream, Encoding.GetEncoding("utf-8")); //统一使用UTF-8编码
            write.WriteLine(DateTime.Now.ToString() + "-" + information+"\n");
            
            write.Close();
            logFileStream.Close();
        }
    }
}

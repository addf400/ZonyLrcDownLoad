using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace Zony_Lrc_Download_2._0
{
    /// <summary>
    /// 公用设置类
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// 编码选项
        /// </summary>
        public static int m_EncodingOption { get; set; }
        /// <summary>
        /// 歌词下载目录
        /// </summary>
        public static string m_LrcDownDirectory { get; set; }
        /// <summary>
        /// 歌词下载线程
        /// </summary>
        public static int m_DownLoadThreadNum { get; set; }
        /// <summary>
        /// 歌词源选项
        /// </summary>
        public static int m_LrcDownSource { get; set; }
        /// <summary>
        /// 歌词文件名搜索选项
        /// </summary>
        public static int m_SearchFileNameOption { get; set; }

        private static string iniFilePath = Environment.CurrentDirectory + @"\set.ini";

        /// <summary>
        /// 检测配置文件是否存在，如果不存在，创建一个新的配置文件。
        /// </summary>
        public static void Check_And_New()
        {
            if(!File.Exists(iniFilePath))
            {
                FileStream fs = new FileStream(iniFilePath, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("[Set]");
                sw.WriteLine("EncodingOption=0");
                sw.WriteLine("LrcDownDirectory=\"null\"");
                sw.WriteLine("DownLoadThreadNum=8");
                sw.WriteLine("LrcDownSource=0");
                sw.WriteLine("SearchFileNameOption=0");
                sw.Close();
                fs.Close();
            }
        }
        /// <summary>
        /// 将设置选项保存在配置文件当中
        /// </summary>
        public static void Save()
        {
            Write("Set", "EncodingOption", m_EncodingOption.ToString(), iniFilePath);
            Write("Set", "LrcDownDirectory", m_LrcDownDirectory, iniFilePath);
            Write("Set", "DownLoadThreadNum", m_DownLoadThreadNum.ToString(), iniFilePath);
            Write("Set", "LrcDownSource", m_LrcDownSource.ToString(), iniFilePath);
            Write("Set", "SearchFileNameOption", m_SearchFileNameOption.ToString(), iniFilePath);
        }

        /// <summary>
        /// 从配置文件当中读取设置选项
        /// </summary>
        public static void Load()
        {
            m_EncodingOption = int.Parse(Read("Set", "EncodingOption", iniFilePath));
            m_LrcDownDirectory = Read("Set", "LrcDownDirectory", iniFilePath);
            m_DownLoadThreadNum = int.Parse(Read("Set", "DownLoadThreadNum", iniFilePath));
            m_LrcDownSource = int.Parse(Read("Set", "LrcDownSource", iniFilePath));
            m_SearchFileNameOption = int.Parse(Read("Set", "SearchFileNameOption", iniFilePath));
        }

        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def,StringBuilder retVal, int size, string filePath);
        private static string Read(string section,string key,string path)
        {
            StringBuilder tmp = new StringBuilder(255);
            GetPrivateProfileString(section, key, "", tmp, 255, path);
            return tmp.ToString();
        }
        private static void Write(string section,string key,string value,string path)
        {
            WritePrivateProfileString(section, key, value, path);
        }
    }
}

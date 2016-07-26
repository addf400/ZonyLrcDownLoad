/*
 * 描述：封装了对INI文件的操作，提供对程序设置项目保存与读取的接口。
 * 作者：Zony
 * 创建日期：2016/05/04
 * 最后修改日期：2016/05/19
 * 版本：1.1
 */
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Zony_Lrc_Download_2._0.Class.Plugins;

namespace Zony_Lrc_Download_2._0.Class.Configs
{
    /// <summary>
    /// 公用设置类
    /// </summary>
    public static class Config
    {
        #region 设置项
        /// <summary>
        /// 编码方式
        /// </summary>
        public static int option_Encoding { get; set; }
        /// <summary>
        /// 下载线程
        /// </summary>
        public static int option_ThreadNumber { get; set; }
        /// <summary>
        /// 是否忽略已有歌词文件的歌曲
        /// </summary>
        public static int option_IgnoreFile { get; set; }
        /// <summary>
        /// 用户自定义输出目录
        /// </summary>
        public static string option_UserDirectory { get; set; }
        /// <summary>
        /// 更新选项
        /// </summary>
        public static int option_Update { get; set; }
        /// <summary>
        /// 自定义搜索后缀
        /// </summary>
        public static string option_FileSuffix { get; set; }
        /// <summary>
        /// 插件状态
        /// </summary>
        public static string option_PlugState { get; set; }
        #endregion

        private static string iniPath = Environment.CurrentDirectory + "\\Set.ini";

        /// <summary>
        /// 检测配置项是否存在，不存在即创建一个新的配置文件。
        /// </summary>
        public static void Check()
        {
            if (!File.Exists(iniPath))
            {
                FileStream iniFile = File.Create(iniPath);
                StreamWriter sw = new StreamWriter(iniFile);
                sw.WriteLine("[Set]");
                sw.WriteLine("Encoding=0");
                sw.WriteLine("ThreadNumber=4");
                sw.WriteLine("IgnoreFile=0");
                sw.WriteLine("UserDirectory=null");
                sw.WriteLine("Update=1");
                sw.WriteLine("FileSuffix=*.acc;*.mp3;*.ape;*.flac");
                #region 写入默认插件状态
                if(Untiy.Plugs.Count != 0)
                {
                    var str = new StringBuilder();
                    for (int i = 0; i < Untiy.Plugs.Count; i++)
                    {
                        str.Append("1,");
                    }
                    str.Remove(str.Length - 1, 1);
                    sw.Write("PlugState=" + str.ToString());
                }
                #endregion
                sw.Close();
                iniFile.Close();
            }
        }

        /// <summary>
        /// 加载设置
        /// </summary>
        public static void Load()
        {
            Check();
            option_Encoding = int.Parse(INIRead("Set", "Encoding", iniPath));
            option_IgnoreFile = int.Parse(INIRead("Set", "IgnoreFile", iniPath));
            option_Update = int.Parse(INIRead("Set", "Update", iniPath));
            option_ThreadNumber = int.Parse(INIRead("Set", "ThreadNumber", iniPath));
            option_UserDirectory = INIRead("Set", "UserDirectory", iniPath);
            option_FileSuffix = INIRead("Set", "FileSuffix", iniPath);
            option_PlugState = INIRead("Set", "PlugState", iniPath);

            #region 重加载插件状态
            if (option_PlugState != null)
            {
                // 判断插件增删
                if (option_PlugState.Split(',').Length < Untiy.Plugs.Count)
                {
                    int duartion = Untiy.Plugs.Count - option_PlugState.Split(',').Length;
                    StringBuilder sb = new StringBuilder(option_PlugState);
                    for (int i = 0; i < duartion; i++)
                    {
                        sb.Append(",0");
                    }
                    option_PlugState = sb.ToString();
                    Save();
                }
                else
                {
                    int duartion = option_PlugState.Split(',').Length - Untiy.Plugs.Count;
                    StringBuilder sb = new StringBuilder(option_PlugState);
                    sb.Remove(sb.Length - duartion * 2, duartion * 2);
                    option_PlugState = sb.ToString();
                    Save();
                }
            }
            #endregion
        }

        /// <summary>
        /// 保存设置
        /// </summary>
        public static void Save()
        {
            INIWrite("Set", "Encoding", option_Encoding.ToString(), iniPath);
            INIWrite("Set", "IgnoreFile", option_IgnoreFile.ToString(), iniPath);
            INIWrite("Set", "Update", option_Update.ToString(), iniPath);
            INIWrite("Set", "ThreadNumber", option_ThreadNumber.ToString(), iniPath);
            INIWrite("Set", "UserDirectory", option_UserDirectory, iniPath);
            INIWrite("Set", "FileSuffix", option_FileSuffix, iniPath);
            INIWrite("Set", "PlugState", option_PlugState, iniPath);
        }

        #region ini文件读写封装
        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        /// <summary>
        /// 读INI配置项
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">键名</param>
        /// <param name="path">ini文件路径</param>
        /// <returns>读取到的数据</returns>
        private static string INIRead(string section, string key, string path)
        {
            StringBuilder tmp = new StringBuilder(255);
            GetPrivateProfileString(section, key, "", tmp, 255, path);
            return tmp.ToString();
        }
        /// <summary>
        /// 写INI配置项
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">键名</param>
        /// <param name="value">要写的值</param>
        /// <param name="path">ini文件路径</param>
        private static void INIWrite(string section, string key, string value, string path)
        {
            WritePrivateProfileString(section, key, value, path);
        }
        #endregion
    }
}

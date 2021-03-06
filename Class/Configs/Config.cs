﻿/*
 * 描述：封装了对INI文件的操作，提供对程序设置项目保存与读取的接口。
 * 作者：Zony
 * 创建日期：2016/05/04
 * 最后修改日期：2016/09/14
 * 版本：2.0
 */
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Zony_Lrc_Download_2._0.Class.Plugins;
using Newtonsoft.Json;
using Zony_Lrc_Download_2._0.Class.Utils;

namespace Zony_Lrc_Download_2._0.Class.Configs
{
    /// <summary>
    /// 公用设置类
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// 设置项值
        /// </summary>
        public static ConfigModel configValue = new ConfigModel();
        private static FileStream fs;
        private static string _setPath = Environment.CurrentDirectory + @"\set.conf";

        /// <summary>
        /// 保存设置
        /// </summary>
        public static void Save()
        {
            string jsonResult = JsonConvert.SerializeObject(configValue);
            byte[] jsonByte = Encoding.ASCII.GetBytes(jsonResult);
            fs = File.Open(_setPath, FileMode.OpenOrCreate);
            fs.SetLength(0);
            fs.Write(jsonByte, 0, jsonByte.Length);
            fs.Close();
        }

        /// <summary>
        /// 加载设置
        /// </summary>
        public static void Load()
        {
            if (!File.Exists(_setPath))
            {
                defaultOption();
                Save();
                return;
            }

            fs = File.Open(_setPath, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string jsonStr = sr.ReadToEnd();
            configValue = JsonConvert.DeserializeObject<ConfigModel>(jsonStr);
            sr.Close();
            fs.Close();
            // 检测到插件更改
            if (configValue.option_PlugStatus.Count != LongLife.Plug_Lrc.PlugsInfo.Count)
            {
                configValue.option_PlugStatus.Clear();
                initPlugStatus();
            }
        }

        #region 私有方法
        /// <summary>
        /// 默认选项设置
        /// </summary>
        private static void defaultOption()
        {
            configValue.option_Encoding = 0;
            configValue.option_FileSuffix = "*.acc;*.mp3;*.ape;*.flac";
            configValue.option_IgnoreFile = false;
            configValue.option_Update = true;
            configValue.option_UserDirectory = "null";
            configValue.option_ThreadNumber = 4;
            configValue.option_PlugStatus = new List<PlugStatus>();
            initPlugStatus();
        }

        /// <summary>
        /// 设置默认插件状态
        /// </summary>
        private static void initPlugStatus()
        {
            foreach (var item in LongLife.Plug_Lrc.PlugsInfo)
            {
                configValue.option_PlugStatus.Add(new PlugStatus { IsOpen = true });
            }
        }
        #endregion
    }

    /// <summary>
    /// 插件存储模型
    /// </summary>
    public class ConfigModel
    {
        /// <summary>
        /// 编码方式
        /// </summary>
        public int option_Encoding { get; set; }
        /// <summary>
        /// 下载线程
        /// </summary>
        public int option_ThreadNumber { get; set; }
        /// <summary>
        /// 是否忽略已有歌词文件的歌曲
        /// </summary>
        public bool option_IgnoreFile { get; set; }
        /// <summary>
        /// 用户自定义输出目录
        /// </summary>
        public string option_UserDirectory { get; set; }
        /// <summary>
        /// 更新选项
        /// </summary>
        public bool option_Update { get; set; }
        /// <summary>
        /// 自定义搜索后缀
        /// </summary>
        public string option_FileSuffix { get; set; }
        /// <summary>
        /// 插件状态
        /// </summary>
        public List<PlugStatus> option_PlugStatus { get; set; }
    }
}

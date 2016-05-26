/*
 * 描述：负责插件加载与管理的静态类
 * 作者：Zony
 * 创建日期：2016/05/10
 * 最后修改日期：2016/05/26
 * 版本：1.0
 */
using LibIPlug;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Zony_Lrc_Download_2._0.Class.Configs;
using System.Text;

namespace Zony_Lrc_Download_2._0.Class.Plugins
{
    public static class Untiy
    {
        /// <summary>
        /// 插件列表
        /// </summary>
        public static List<IPlugin> PluginsList = new List<IPlugin>();
        public static List<PluginInfoAttribute> piProperties = new List<PluginInfoAttribute>();

        /// <summary>
        /// 载入插件
        /// </summary>
        /// <returns>成功载入的插件数目，返回0则是出现错误</returns>
        public static int LoadPlugins()
        {
            PluginsList.Clear();
            piProperties.Clear();
            if (!Directory.Exists(Environment.CurrentDirectory + @"\Plugins")) return 0;

            string[] files = Directory.GetFiles(Environment.CurrentDirectory + @"\Plugins");
            PluginInfoAttribute typeAttribute = new PluginInfoAttribute();

            foreach (string file in files)
            {
                string ext = file.Substring(file.LastIndexOf("."));
                if (ext != ".dll") continue;
                try
                {
                    Assembly tmp = Assembly.LoadFile(file);
                    Type[] types = tmp.GetTypes();
                    // 遍历实例，获得实现接口的类
                    foreach (Type t in types)
                    {
                        if (t.GetInterface("IPlugin") != null)
                        {
                            IPlugin plugin = (IPlugin)tmp.CreateInstance(t.FullName);
                            PluginsList.Add(plugin);
                            object[] attbs = t.GetCustomAttributes(typeAttribute.GetType(),false);
                            PluginInfoAttribute attribute = null;
                            foreach (object attb in attbs)
                            {
                                if(attb is PluginInfoAttribute)
                                {
                                    attribute = (PluginInfoAttribute)attb;
                                    break;
                                }
                            }

                            // 载入插件信息
                            if(attribute != null)
                            {
                                piProperties.Add(attribute);
                                plugin.PluginInfo = attribute;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    return 0;
                }
            }

            // 判断插件增删
            if (Config.option_PlugState.Split(',').Length < PluginsList.Count)
            {
                int duartion = PluginsList.Count - Config.option_PlugState.Split(',').Length;
                StringBuilder sb = new StringBuilder(Config.option_PlugState);
                for(int i=0;i< duartion;i++)
                {
                    sb.Append(",0");
                }
                Config.option_PlugState = sb.ToString();
                Config.Save();
            }
            else
            {
                int duartion = Config.option_PlugState.Split(',').Length - PluginsList.Count;
                StringBuilder sb = new StringBuilder(Config.option_PlugState);
                sb.Remove(sb.Length - duartion * 2, duartion*2);
                Config.option_PlugState = sb.ToString();
                Config.Save();
            }

            return PluginsList.Count;
        }
    }
}

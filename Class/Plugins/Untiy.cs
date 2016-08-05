/*
 * 描述：负责插件加载与管理的静态类
 * 作者：Zony
 * 创建日期：2016/05/10
 * 最后修改日期：2016/08/05
 * 版本：1.1
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using LibIPlug;

namespace Zony_Lrc_Download_2._0.Class.Plugins
{
    public static class Untiy
    {
        /// <summary>
        /// 插件列表
        /// </summary>
        public static List<IPlugin> Plugs = new List<IPlugin>();
        /// 插件信息列表
        /// </summary>
        public static List<PluginInfoAttribute> piProperties = new List<PluginInfoAttribute>();

        /// <summary>
        /// 载入插件
        /// </summary>
        /// <returns>成功载入的插件数目，返回0则是出现错误</returns>
        public static int LoadPlugins()
        {
            Plugs.Clear();
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
                            Plugs.Add(plugin);
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
                catch (Exception exp)
                {
                    MessageBox.Show("产生异常：\r\n" + exp.ToString());
                    return 0;
                }
            }

            return Plugs.Count;
        }
    }
}

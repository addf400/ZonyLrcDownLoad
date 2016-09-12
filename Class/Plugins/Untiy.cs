/*
 * 描述：负责插件加载与管理的静态类
 * 作者：Zony
 * 创建日期：2016/05/10
 * 最后修改日期：2016/08/26
 * 版本：1.2
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using LibIPlug;

namespace Zony_Lrc_Download_2._0.Class.Plugins
{
    /// <summary>
    /// 插件加载基类定义
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BasePlug<T> where T : class
    {
        public List<T> Plugs { get; }
        public List<PluginInfoAttribute> PlugsInfo { get; }
        private Type interType;

        public BasePlug()
        {
            interType = typeof(T);
            PlugsInfo = new List<PluginInfoAttribute>();
            Plugs = new List<T>();
        }

        public int LoadPlugs()
        {
            Plugs.Clear();
            PlugsInfo.Clear();

            if (!Directory.Exists(Environment.CurrentDirectory + @"\Plugins")) return 0;

            string[] files = Directory.GetFiles(Environment.CurrentDirectory + @"\Plugins");
            PluginInfoAttribute typeAttribute = new PluginInfoAttribute();

            foreach (var item in files)
            {
                string ext = Path.GetExtension(item);
                if (ext != ".dll") continue;

                try
                {
                    Assembly tmp = Assembly.UnsafeLoadFrom(item);
                    Type[] types = tmp.GetTypes();
                    // 遍历实例获得实现接口的类
                    foreach (Type t in types)
                    {
                        if (t.GetInterface(interType.Name) != null)
                        {
                            T plug = (T)tmp.CreateInstance(t.FullName);
                            Plugs.Add(plug);

                            object[] attbs = t.GetCustomAttributes(typeAttribute.GetType(), false);
                            PluginInfoAttribute attribute = null;
                            foreach (object attb in attbs)
                            {
                                if (attb is PluginInfoAttribute)
                                {
                                    attribute = (PluginInfoAttribute)attb;
                                    break;
                                }
                            }

                            if (attribute != null)
                            {
                                PlugsInfo.Add(attribute);
                            }

                            CallBack();
                        }
                    }
                }
                catch (Exception exp)
                {
                    throw exp;
                }
            }

            return Plugs.Count;
        }

        protected virtual void CallBack() { }
    }

    /// <summary>
    /// 歌词下载插件加载
    /// </summary>
    public class Plug_LrcDown : BasePlug<IPlugin>
    {
        protected override void CallBack()
        {
            for (int i = 0; i < Plugs.Count; i++)
            {
                Plugs[i].PluginInfo = PlugsInfo[i];
            }
        }
    }

    /// <summary>
    /// 高级扩展插件加载
    /// 
    /// </summary>
    public class Plug_Hight : BasePlug<IPlugin_Hight>
    {
        protected override void CallBack()
        {
            for (int i = 0; i < Plugs.Count; i++)
            {
                Plugs[i].PluginInfo = PlugsInfo[i];
            }
        }
    }
}

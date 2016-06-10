/*
 * 描述：提供了一些常用工具函数。
 * 作者：Zony
 * 创建日期：2016/05/10
 * 最后修改日期：2016/06/10
 * 版本：1.0
 */
using System.Collections.Generic;

namespace Zony_Lrc_Download_2._0.Class.Utils
{
    public static class FuncUtils
    {
        /// <summary>
        /// 从目标字典集合创建一个新的副本
        /// </summary>
        /// <param name="target">目标字典的引用</param>
        /// <returns>创建好的副本</returns>
        public static Dictionary<int, string> DictionaryCopy(ref Dictionary<int, string> target)
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            foreach (KeyValuePair<int, string> key in target)
            {
                dic.Add(key.Key, key.Value);
            }
            return dic;
        }

        /// <summary>
        /// 根据指定字符分割字符串，并按顺序保存在List
        /// </summary>
        /// <param name="context">要分割的内容</param>
        /// <param name="splitChar">分割依据</param>
        /// <returns>列表容器</returns>
        public static List<string> SplitString(string context, char splitChar)
        {
            string[] split = context.Split(splitChar);
            List<string> list = new List<string>();

            foreach (string str in split)
            {
                list.Add(str);
            }

            return list;
        }
    }
}

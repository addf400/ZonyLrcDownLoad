/*
 * 描述：提供了一些常用工具函数。
 * 作者：Zony
 * 创建日期：2016/05/10
 * 最后修改日期：2016/05/10
 * 版本：1.0
 */
using System.Collections.Generic;

namespace Zony_Lrc_Download_2._0.Class.Utils
{
    public static class Functions
    {
        public static Dictionary<int, string> DictionaryCopy(ref Dictionary<int, string> target)
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            foreach (KeyValuePair<int, string> key in target)
            {
                dic.Add(key.Key, key.Value);
            }
            return dic;
        }
    }
}

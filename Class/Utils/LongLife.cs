/*
 * 描述：提供程序运行时常存对象存储类。
 * 作者：Zony
 * 创建日期：2016/05/06
 * 最后修改日期：2016/05/06
 * 版本：1.0
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zony_Lrc_Download_2._0.Class.Utils
{
    public static class LongLife
    {
        public static Dictionary<int, string> MusicPathList = new Dictionary<int, string>();
        public static Dictionary<int, string> MusicPathFailedList = new Dictionary<int, string>();
    }
}

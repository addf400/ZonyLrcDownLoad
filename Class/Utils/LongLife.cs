/*
 * 描述：提供程序运行时常存对象存储类。
 * 作者：Zony
 * 创建日期：2016/05/06
 * 最后修改日期：2016/05/10
 * 版本：1.0
 */
using System.Collections.Generic;

namespace Zony_Lrc_Download_2._0.Class.Utils
{
    public static class LongLife
    {
        /// <summary>
        /// 搜索到的所有音乐文件路径集合
        /// </summary>
        public static Dictionary<int, string> MusicPathList = new Dictionary<int, string>();
        /// <summary>
        /// 下载失败的音乐文件路径集合
        /// </summary>
        public static Dictionary<int, string> MusicPathFailedList = new Dictionary<int, string>();
    }
}

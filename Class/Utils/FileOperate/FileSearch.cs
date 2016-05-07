/*
 * 描述：提供对多后缀文件搜索的支持。
 * 作者：Zony
 * 创建日期：2016/05/04
 * 最后修改日期：2016/05/04
 * 版本：1.3
 */
using System;
using System.Collections.Generic;
using System.IO;

namespace Zony_Lrc_Download_2._0.Class.Utils.FileOperate
{
    /// <summary>
    /// 文件搜索类
    /// </summary>
    class FileSearch
    {
        #region 返回值设定
        /// <summary>
        /// search 函数返回值设定
        /// </summary>
        public enum FileSearchResult
        {
            /// <summary>
            /// 正常
            /// </summary>
            Normal=0,
            /// <summary>
            /// 目录位置无效
            /// </summary>
            Directory_Path_Invalid=1,
            /// <summary>
            /// 没有搜索到文件
            /// </summary>
            No_Search_File=2,
            /// <summary>
            /// 发生异常
            /// </summary>
            Exception_Occurs=3
        }
        #endregion
        
        /// <summary>
        /// 搜索指定目录下符合后缀名的文件
        /// </summary>
        /// <param name="list">存放索引与歌曲路径的键值对</param>
        /// <param name="searchDirectoryPath">要搜索的目录</param>
        /// <param name="suffix">后缀名集合</param>
        /// <returns>搜索结果状态</returns>
        public FileSearchResult Search(ref Dictionary<int,string> list,string searchDirectoryPath,List<string> suffix)
        {
            int count=0;
            int invalidCount=0;
            searchDirectoryPath.Trim();

            if(searchDirectoryPath == "" || !Directory.Exists(searchDirectoryPath))
            {
                return FileSearchResult.Directory_Path_Invalid;
            }

            try
            {
                foreach (string ext in suffix) // 轮询搜索指定后缀文件
                {
                    string[] files = Directory.GetFiles(searchDirectoryPath, ext, SearchOption.AllDirectories);

                    if(files.Length != 0)
                    {
                        for (int i = 0; i < files.Length; i++,count++)
                        {
                            list.Add(count, files[i]);
                        }
                    }
                    else
                    {
                        invalidCount++;
                    }
                }

                if (invalidCount == suffix.Count)
                {
                    return FileSearchResult.No_Search_File;
                }
                else
                {
                    return FileSearchResult.Normal;
                }
            }catch(Exception exp)
            {
                return FileSearchResult.Exception_Occurs;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Zony_Lrc_Download_2._0
{
    /// <summary>
    /// 文件搜索类返回值
    /// </summary>
    public enum FileSearchReturn{
        /// <summary>
        /// 正常
        /// </summary>
        NORMAL=0,
        /// <summary>
        /// 文件路径无效
        /// </summary>
        FILE_PATH_INVALID=-1,
        /// <summary>
        /// 没有搜索到文件
        /// </summary>
        NO_SEARCH_FILE=-2,
        EXCEPTION=-3
    }
    public class FileSearch
    {
        /// <summary>
        /// 搜索指定后缀文件并将其添加到容器内
        /// </summary>
        /// <param name="list"></param>
        /// <param name="filepath"></param>
        /// <param name="ext"></param>
        /// <returns></returns>
        public FileSearchReturn SearchFile(ref Dictionary<int, string> list, string filepath, string ext)
        {
            filepath.Trim();

            if (filepath == "" || "".Equals(filepath))
            {
                return FileSearchReturn.FILE_PATH_INVALID;
            }
            try
            {
                string[] files = Directory.GetFiles(filepath, ext, SearchOption.AllDirectories);

                if (files.Length != 0)
                {
                    // 添加到List<string>容器
                    for (int i = 0; i < files.Length; i++)
                    {
                        list.Add(i, files[i]);
                    }

                    return FileSearchReturn.NORMAL;
                }
                else
                {
                    return FileSearchReturn.NO_SEARCH_FILE;
                }
            }
            catch (Exception exp)
            {
                #region 日志点
                Log.WriteLog("在类FileSearch中发生异常：" + exp.ToString());
                #endregion
                return FileSearchReturn.EXCEPTION;
            }
            
        }

    }
}

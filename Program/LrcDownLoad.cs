using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Zony_Lrc_Download_2._0
{
    /// <summary>
    /// 函数状态返回值
    /// </summary>
    public enum DownLoadReturn
    {
        /// <summary>
        /// 正常返回
        /// </summary>
        NORMAL = 0,
        /// <summary>
        /// HTML页面无数据
        /// </summary> 
        HTML_INVALID = -1,
        /// <summary>
        /// 发生异常
        /// </summary>
        EXCEPTION = -2,
        /// <summary>
        /// 搜寻下载链接失败
        /// </summary>
        REGEX_ERROR = -3,
        /// <summary>
        /// 文件创建失败
        /// </summary>
        FILE_CREAT_ERROR = -4,
        /// <summary>
        /// 网络错误
        /// </summary>
        INET_ERROR = -5
    }

    /// <summary>
    /// 公用Lrc接口
    /// </summary>
    public interface ILrcDownLoad
    {
        DownLoadReturn DownLoad(string filepath, ref byte[] filedata);
    }
}

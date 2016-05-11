/*
 * 描述：写出歌词文件操作。
 * 作者：Zony
 * 创建日期：2016/05/04
 * 最后修改日期：2016/05/04
 * 版本：1.0
 */
using System;
using System.Text;
using System.IO;

namespace Zony_Lrc_Download_2._0.Class.Utils.FileOperate
{
    /// <summary>
    /// 歌词文件写出类
    /// </summary>
    class FileWrite
    {
        /// <summary>
        /// 将字节数据写入文件
        /// </summary>
        /// <param name="data">要写入的数据</param>
        /// <param name="filePath">源歌曲文件路径</param>
        /// <param name="encoding">编码方式</param>
        /// <param name="usrPath">用户自定义路径</param>
        /// <returns>是否写入成功</returns>
        public bool Write(ref byte[] data,string filePath,int encoding,string usrPath)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);

            try
            {
                string lrcPath = usrPath == "null" ? 
                    lrcPath = Path.GetDirectoryName(filePath) + "\\" + fileName + ".lrc" : lrcPath = usrPath + "\\" + fileName + ".lrc"; ;
                
                var fs = File.Create(lrcPath);
                
                // 编码转换
                switch(encoding)
                {
                    case 0:
                        // 默认编码
                        break;
                    case 1:
                        // gb2312
                        data = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding("gb2312"), data);
                        break;
                    case 2:
                        // gbk
                        data = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding("gbk"), data);
                        break;
                    case 3:
                        // BIG5，台湾大五码
                        data = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding("BIG5"), data);
                        break;
                    case 4:
                        // 日文编码
                        data = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding("shift_jis"), data);
                        break;
                    case 5:
                        // ANSI
                        data = Encoding.Convert(Encoding.UTF8, Encoding.Default, data);
                        break;
                }

                fs.Write(data, 0, data.Length);
                fs.Close();
                return true;
            }catch(Exception)
            {
                return false;
            }
        }
    }
}

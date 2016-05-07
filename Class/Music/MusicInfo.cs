/*
 * 描述：获取歌曲信息
 * 作者：Zony
 * 创建日期：2016/05/04
 * 最后修改日期：2016/05/04
 * 版本：1.0
 */
using ID3;
using System.IO;
using System;

namespace Zony_Lrc_Download_2._0.Class.Music
{
    class MusicInfo
    {
        #region 歌曲信息字段
        /// <summary>
        /// 歌曲标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 歌手名称
        /// </summary>
        public string Singer { get; set; }
        /// <summary>
        /// 歌曲路径
        /// </summary>
        public string MusicPath { get; set; }
        #endregion

        /// <summary>
        /// 获得歌曲文件信息
        /// </summary>
        /// <param name="filePath">源歌曲文件路径</param>
        /// <param name="flag">标志位，根据设置而定</param>
        public MusicInfo(string filePath,int flag)
        {
            try
            {
                // 尝试获取ID3标签
                ID3Info id3 = new ID3Info(filePath, true);
                Title = id3.ID3v1Info.Title != null ? id3.ID3v1Info.Title : id3.ID3v2Info.GetTextFrame("TIT2");
                Singer = id3.ID3v1Info.Artist != null ? id3.ID3v1Info.Artist : id3.ID3v2Info.GetTextFrame("TPE1");
                if(Title == "")
                {
                    // 尝试手工分割文件名
                    string fileName = Path.GetFileNameWithoutExtension(filePath);
                    Title = fileName;
                    Singer = fileName;
                }
            }
            catch (Exception ex)
            {
                // 尝试手工分割文件名
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                Title = fileName;
                Singer = fileName;
            }

            MusicPath = filePath;
        }
    }
}

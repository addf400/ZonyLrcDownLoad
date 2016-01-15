using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ID3;
using System.Text.RegularExpressions;

namespace Zony_Lrc_Download_2._0
{
    /// <summary>
    /// 歌曲文件信息类
    /// </summary>
    public class SongInfo
    {
        #region 歌曲信息字段
        /// <summary>
        /// 歌曲名
        /// </summary>
        public string m_SongName { get; set; }
        /// <summary>
        /// 歌手信息
        /// </summary>
        public string m_SongSinger { get; set; }
        /// <summary>
        /// 歌曲文件路径
        /// </summary>
        public string m_SongFilePath { get; set; }
        #endregion

        /// <summary>
        /// 获得歌曲相关信息
        /// </summary>
        /// <param name="filepath">歌曲文件路径</param>
        /// <param name="flag">文件名搜索方式</param>
        public void GetSongInfo(string filepath,int flag)
        {
            try
            {
                ID3Info id3 = new ID3Info(filepath, true);
                m_SongName = id3.ID3v1Info.Title != null ? id3.ID3v1Info.Title : id3.ID3v2Info.GetTextFrame("TIT2");
                m_SongSinger = id3.ID3v1Info.Artist != null ? id3.ID3v1Info.Artist : id3.ID3v2Info.GetTextFrame("TPE1");

            }catch(System.OverflowException)
            {
                m_SongName = "";
                m_SongSinger = "";
            }catch(Exception exp)
            {
                Log.WriteLog(Log.Class.EXCEPTION, exp.ToString());
            }

            // 分割文件名获取歌曲信息
            string fileName = Path.GetFileNameWithoutExtension(filepath);
            string[] result = fileName.Split('-');

            if((m_SongName == null || m_SongName == ""))
            {
                if(flag==0)
                {
                    m_SongName = Path.GetFileNameWithoutExtension(filepath);
                }
                else
                {
                    m_SongName = result[1];
                }
            }

            if (m_SongSinger == null || m_SongSinger == "")
            {
                m_SongSinger = result[0];
                if(flag == 0)
                {
                    m_SongSinger = Path.GetFileNameWithoutExtension(filepath);
                }
                else
                {
                    m_SongSinger = result[1];
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ID3;

namespace Zony_Lrc_Download_2._0
{
    public class SongInfo
    {
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

        /// <summary>
        /// 获得歌曲相关信息
        /// </summary>
        /// <param name="filepath">歌曲文件路径</param>
        public void GetSongInfo(string filepath)
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

            if(m_SongName == null || m_SongName == "")
            {
                m_SongName = Path.GetFileNameWithoutExtension(filepath);
            }

            if (m_SongSinger == null || m_SongSinger == "")
            {
                m_SongSinger = Path.GetFileNameWithoutExtension(filepath);
            }
        }
    }
}

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
        public string m_SongName { get; set; }
        public string m_SongSinger { get; set; }

        public void GetSongInfo(string filepath)
        {
            try
            {
                ID3Info id3 = new ID3Info(filepath, true);
                m_SongName = id3.ID3v1Info.Title != "" ? id3.ID3v1Info.Title : id3.ID3v2Info.GetTextFrame("TIT2");
                m_SongSinger = id3.ID3v1Info.Artist != "" ? id3.ID3v1Info.Artist : id3.ID3v2Info.GetTextFrame("TPE1");
            }catch(System.OverflowException)
            {
                m_SongName = "";
                m_SongSinger = "";
            }catch(Exception exp)
            {
                Log.WriteLog(Log.Class.EXCEPTION, exp.ToString());
            }

            if(m_SongName == "")
            {
                m_SongName = Path.GetFileNameWithoutExtension(filepath);
            }

            if(m_SongSinger == "")
            {
                m_SongSinger = Path.GetFileNameWithoutExtension(filepath);
            }
        }
    }
}

using ID3;
using LibNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace LibTestWindow
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string RequestURL = "http://music.163.com/api/search/get/web?csrf_token=";
            string ResponseURL = "http://music.163.com/api/song/lyric?os=osx&id=";
            var m_tools = new NetUtils();
            var m_info = new MusicInfo(@"D:\Test\Music\戴佩妮 - 小小.mp3");

            string postStr = "&s=" + m_tools.URL_Encoding(m_info.Title, Encoding.UTF8) + "&type=1&offset=0&total=true&limit=5";
            string resultJson = m_tools.Http_Post(RequestURL, postStr);

            if (resultJson == "") return;

            // 获得歌曲ID
            JObject jsonSID = JObject.Parse(resultJson);
            JArray jsonArraySID = (JArray)jsonSID["result"]["songs"];
            string sid = null;

            // 二级匹配
            bool flag = false;
            foreach (var item in jsonArraySID)
            {
                if (item["artists"][0]["name"].ToString() == m_info.Singer)
                {
                    sid = item["id"].ToString();
                    flag = true;
                }

            }
            if (!flag) sid = jsonArraySID[0]["id"].ToString();
        }

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
            public MusicInfo(string filePath)
            {
                try
                {
                    // 尝试获取ID3标签
                    ID3Info id3 = new ID3Info(filePath, true);
                    Title = id3.ID3v1Info.Title != null ? id3.ID3v1Info.Title : id3.ID3v2Info.GetTextFrame("TIT2");
                    Singer = id3.ID3v1Info.Artist != null ? id3.ID3v1Info.Artist : id3.ID3v2Info.GetTextFrame("TPE1");
                    if (Title == "")
                    {
                        // 尝试手工分割文件名
                        string fileName = Path.GetFileNameWithoutExtension(filePath);
                        Title = fileName;
                        Singer = fileName;
                    }
                }
                catch (Exception)
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
}

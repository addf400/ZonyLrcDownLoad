using System;

namespace LibIPlug
{
    #region 插件接口
    public interface IPlugin
    {
        PluginInfoAttribute PluginInfo { get; set; }
        string filePath { get; set; }
        /// <summary>
        /// 歌词下载接口
        /// </summary>
        /// <param name="filePath">歌曲文件路径</param>
        /// <param name="lrcData">的传出歌词数据</param>
        /// <param name="iThread">并发链接设置</param>
        /// <returns></returns>
        bool Down(string filePath, ref byte[] lrcData,int iThread);
    }
    #endregion

    #region 插件描述信息
    public class PluginInfoAttribute : Attribute
    {
        public PluginInfoAttribute() { }
        public PluginInfoAttribute(string name, string version, string author, string descript, int ptype)
        {
            _Name = name;
            _Version = version;
            _Author = author;
            _Descript = descript;
            _Ptype = ptype;
        }

        public string Name
        {
            get
            {
                return _Name;
            }
        }

        public string Version
        {
            get
            {
                return _Version;
            }
        }

        public string Author
        {
            get
            {
                return _Author;
            }
        }

        public string Descript
        {
            get
            {
                return _Descript;
            }
        }

        public int Ptype
        {
            get
            {
                return _Ptype;
            }
        }

        private string _Name = "";
        private string _Version = "";
        private string _Author = "";
        private string _Descript = "";
        private int _Ptype;
    }
    #endregion
}

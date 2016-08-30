using LibIPlug;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using LibNet;
using System.Threading;
using System;

namespace LibLibNeteaseTmp
{
    [PluginInfo("单歌词下载插件", "v1.0", "Zony", "可以下载指定歌曲的歌词。", 1)]
    public class LibNeteaseTmp : IPlugin_Hight
    {
        public PluginInfoAttribute PluginInfo{ get;set; }
        private ResourceModule module;

        public void Init(ref ResourceModule module)
        {
            this.module = module;
            bind();
        }

        private void bind()
        {
            var _currentMenu = module.ListContextMenu.MenuItems.Add("扫描网易云临时文件");
            _currentMenu.Click += _currentMenu_Click;
            module.MainListBox.ContextMenu = module.ListContextMenu;
        }

        private void _currentMenu_Click(object sender, System.EventArgs e)
        {
            #region 界面归零
            module.MainListBox.Items.Clear();
            module.MusicPathList.Clear();
            module.MainProgressBar.Maximum = 0;
            module.MainProgressBar.Value = 0;
            #endregion

            FolderBrowserDialog _fd = new FolderBrowserDialog();
            _fd.Description = "请选择网易云音乐临时文件的路径:";
            _fd.ShowDialog();
            string[] files = null;
            if (_fd.SelectedPath != "")
            {
                // 删除该目录下面所有lrc文件，防止文件干扰
                files = Directory.GetFiles(_fd.SelectedPath, "*.lrc", SearchOption.AllDirectories);
                foreach(var item in files)
                {
                    File.Delete(item);
                }
                // 扫描临时文件
                files = Directory.GetFiles(_fd.SelectedPath, "*.*", SearchOption.AllDirectories);
                foreach (var item in files)
                {
                    module.MainListBox.Items.Add(new ListViewItem(new string[] { item, "" }));
                }
                // 转换
                module.MainProgressBar.Maximum = files.Length;
                module.MainStatusStrip.Text = "开始对临时文件进行转换..";
                new Thread(() =>
                {
                    int _count = 0;
                    int _success = 0;
                    int _failed = 0;
                    foreach (var item in files)
                    {
                        if (tmpConvert(item))
                        {
                            module.MainListBox.Items[_count].SubItems[1].Text = "成功";
                            _success++;
                        }
                        else
                        {
                            module.MainListBox.Items[_count].SubItems[1].Text = "失败";
                            _failed++;
                        }

                        module.MainProgressBar.Value++;
                        _count++;
                    }

                    module.MainStatusStrip.Text = "网易云音乐文件转换完成!";
                    MessageBox.Show(string.Format("已经转换完成：\n总文件数目：{0}\n转换成功：{1}\n转换失败：{2}",files.Length,_success,_failed), "转换完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }).Start();
            }
        }

        private bool tmpConvert(string filePath)
        {
            try
            {
                FileStream _rdfs = new FileStream(filePath, FileMode.Open);
                StreamReader _rdsr = new StreamReader(_rdfs);

                string _lrcData = _rdsr.ReadToEnd();
                JObject _jsonLrc = JObject.Parse(_lrcData);
                List<string> _lrcItem = new List<string>();

                // 判断是否有翻译歌词
                if (!_jsonLrc["translateLyric"].ToString().Equals(""))
                {
                    string _orglrc = _jsonLrc["lyric"].ToString();
                    Regex _orgReg = new Regex(@"\[\d+:\d+.\d+\].+");
                    var _orgResult = _orgReg.Matches(_orglrc);
                    foreach (var item in _orgResult)
                    {
                        _lrcItem.Add(item.ToString());
                    }
                    // 获得翻译歌词
                    string _transLrc = _jsonLrc["translateLyric"].ToString();
                    Regex _transReg = new Regex(@"\[\d+:\d+.\d+\].+");
                    var _transResult = _transReg.Matches(_transLrc);
                    // 分割并且并入英文歌词当中
                    int _count = 0;
                    foreach (var item in _transResult)
                    {
                        var _tmp = item.ToString();
                        string[] _lrcItemArray = _tmp.Split(']');
                        _lrcItem[_count] = string.Format("{0} {1}", _lrcItem[_count], _lrcItemArray[1]);
                        _count++;
                    }
                }
                else
                {
                    string _orglrc = _jsonLrc["lyric"].ToString();
                    Regex _orgReg = new Regex(@"\[\d+:\d+.\d+\].+");
                    var _orgResult = _orgReg.Matches(_orglrc);
                    foreach (var item in _orgResult)
                    {
                        _lrcItem.Add(item.ToString());
                    }
                }
                // 获得歌手信息与歌曲名称
                NetUtils nt = new NetUtils();
                string lrcresult = nt.Http_Get(string.Format("http://music.163.com/api/song/detail/?id={0}&ids=%5B{0}%5D", _jsonLrc["musicId"].ToString()), Encoding.UTF8, true);
                JObject _jsonMusic = JObject.Parse(lrcresult);
                JArray _jsonSongs = (JArray)_jsonMusic["songs"];
                JArray _jsonArtists = (JArray)_jsonSongs[0]["artists"];
                string _artist = _jsonArtists[0]["name"].ToString();
                string _title = _jsonSongs[0]["name"].ToString();
                // 构建文件名
                string _fileName = string.Format("{0} - {1}", _artist, _title);

                // 关闭文件流
                _rdsr.Close();
                _rdfs.Close();

                //构造歌词数据
                StringBuilder _sb = new StringBuilder();
                foreach (var item in _lrcItem)
                {
                    _sb.Append(item + "\n");
                }
                // 去掉网易时间轴后一位
                Regex _reg = new Regex(@"\[\d+:\d+.\d+\]");
                string _covertOkString = _reg.Replace(_sb.ToString(), new MatchEvaluator((Match machs) =>
                {
                    string time = machs.ToString();
                    return time.Remove(time.Length - 2, 1);
                }));
                // 输出到文件
                string path = Path.GetDirectoryName(filePath) + @"\" + _fileName + ".lrc";
                FileStream _wrfs = new FileStream(path, FileMode.Create);
                byte[] _lrcbyte = Encoding.UTF8.GetBytes(_covertOkString);
                _wrfs.Write(_lrcbyte, 0, _lrcbyte.Length);
                _wrfs.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

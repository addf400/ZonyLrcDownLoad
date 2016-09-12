using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibIPlug;
using ID3;
using System.Windows.Forms;
using System.IO;

namespace LibLrcToMp3
{
    [PluginInfo("歌词写入插件","v1.0","Zony", "将LRC歌词写入MP3文件的IDV2标签。",1)]
    public class LibLrcToMp3 : IPlugin_Hight
    {
        public PluginInfoAttribute PluginInfo { get;set; }
        private ResourceModule module;

        public void Init(ref ResourceModule module)
        {
            this.module = module;
            bind();
        }

        private void bind()
        {
            var _currentMenu = module.ListContextMenu.MenuItems.Add("歌词写入插件");
            _currentMenu.Click += _currentMenu_Click;
            module.MainListBox.ContextMenu = module.ListContextMenu;
        }

        private void _currentMenu_Click(object sender, EventArgs e)
        {
            #region 界面归零
            module.MainListBox.Items.Clear();
            module.MusicPathList.Clear();
            module.MainProgressBar.Maximum = 0;
            module.MainProgressBar.Value = 0;
            #endregion

            FolderBrowserDialog _fd = new FolderBrowserDialog();
            _fd.Description = "请选择歌曲与歌词所在的文件夹";
            _fd.ShowDialog();
            string[] files = null;

            if (_fd.SelectedPath != "")
            {
                files = Directory.GetFiles(_fd.SelectedPath, "*.mp3",SearchOption.AllDirectories);
                foreach (var item in files)
                {
                    module.MainListBox.Items.Add(new ListViewItem(new string[] { item, "" }));
                }
                // 写入
                module.MainProgressBar.Maximum = files.Length;
                int _count = 0;
                module.MainStatusStrip.Text = "开始写入LRC歌词文件...";
                foreach (var item in files)
                {
                    var _str = Path.GetDirectoryName(item) + @"\" + Path.GetFileNameWithoutExtension(item) + ".lrc";
                    if(File.Exists(_str))
                    {
                        var _fr = new FileStream(_str, FileMode.Open);
                        var _sr = new StreamReader(_fr);
                        var _text = _sr.ReadToEnd();
                        try
                        {
                            // 写入ID3标签
                            ID3Info info = new ID3Info(item, true);
                            info.ID3v2Info.SetTextFrame("TEXT", _text);
                            info.Save();
                            module.MainListBox.Items[_count].SubItems[1].Text = "成功";
                        }
                        catch
                        {
                            module.MainListBox.Items[_count].SubItems[1].Text = "失败";
                        }
                        _sr.Close();
                        _fr.Close();
                    }
                    else
                    {
                        module.MainListBox.Items[_count].SubItems[1].Text = "不存在LRC";
                    }

                    _count++;
                    module.MainProgressBar.Value++;
                }
                module.MainStatusStrip.Text = "歌词写入完成！";
            }
        }
    }
}

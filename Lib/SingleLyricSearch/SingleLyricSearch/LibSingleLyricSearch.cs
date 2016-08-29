using LibIPlug;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            if(_fd.SelectedPath != "")
            {
                string[] files = Directory.GetFiles(_fd.SelectedPath, "*.tmp", SearchOption.AllDirectories);
                foreach(var item in files)
                {
                    module.MainListBox.Items.Add(new ListViewItem(new string[] { item,""}));
                }
            }

            module.MainStatusStrip.Text = "开始对临时文件进行转换..";
            
        }

        private bool tmpConvert(string filePath)
        {
            return false;
        }
    }
}

using LibIPlug;

namespace LibSingleLyricSearch
{
    [PluginInfo("单歌词下载插件", "v1.0", "Zony", "可以下载指定歌曲的歌词。", 1)]
    public class SingleLyricSearch : IPlugin_Hight
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
            module.ListContextMenu.MenuItems.Add("下载歌词");
            module.MainListBox.ContextMenu = module.ListContextMenu;
        }
    }
}

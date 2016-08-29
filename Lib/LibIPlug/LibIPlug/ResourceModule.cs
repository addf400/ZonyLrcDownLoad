/*
 * 描述：资源模型，用与存放插件与主程序的交互资源。
 * 作者：Zony
 * 创建日期：2016/08/29
 * 最后修改日期：2016/08/29
 * 版本：v1.0
 */
using System.Collections.Generic;
using System.Windows.Forms;

namespace LibIPlug
{
    /// <summary>
    /// 资源模型
    /// </summary>
    public class ResourceModule
    {
        /// <summary>
        /// ListView绑定的右键菜单
        /// </summary>
        public ContextMenu ListContextMenu { get; set; }
        /// <summary>
        /// 存放歌曲文件信息的Listview
        /// </summary>
        public ListViewNF MainListBox { get; set; }
        /// <summary>
        /// 用于存放歌曲文件路径的键值对
        /// </summary>
        public Dictionary<int,string> MusicPathList { get; set; }
        /// <summary>
        /// 底部进度条
        /// </summary>
        public ProgressBar MainProgressBar { get; set; }
        /// <summary>
        /// 底部状态栏
        /// </summary>
        public StatusStrip MainStatusStrip { get; set; }
    }
}

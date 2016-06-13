/*
 * 描述：重载主窗口加载事件
 * 作者：Zony
 * 创建日期：2016/06/13
 * 最后修改日期：2016/06/13
 * 版本：1.0
 */
using System;
using System.Windows.Forms;

namespace Zony_Lrc_Download_2._0.Class.UI
{
    /// <summary>
    /// 重载主窗口加载事件
    /// </summary>
    public class UI_From : Form
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // 加载图标
            Icon = Resource1._6;
        }
    }
}

/*
 * 描述：捐赠界面
 * 作者：Zony
 * 创建日期：2016/06/10
 * 最后修改日期：2016/06/10
 * 版本：1.0
 */
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Zony_Lrc_Download_2._0.Class.UI;
using Zony_Lrc_Download_2._0.Class.Utils.DownLoad;

namespace Zony_Lrc_Download_2._0.Window
{
    public partial class Window_Donate : UI_From
    {
        public Window_Donate()
        {
            InitializeComponent();
        }

        private void Window_Donate_Load(object sender, EventArgs e)
        {
            new Thread(() => {
                textBox1.Text = "正在加载...";
                string donateStr = new NetUtils().Http_Get("http://git.oschina.net/jokers/ZonyLrcDownload/blob/master/readme.md", Encoding.UTF8);
                Regex reg = new Regex(@"捐赠记录.+(?=&#x000A;&#x000A;###)");
                string str = reg.Match(donateStr).ToString();
                var textStr = str.Replace("&#x000A;", "\r\n");
                textBox1.Text = textStr;
            }).Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

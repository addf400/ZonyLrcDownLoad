using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Threading;
using Zony_Lrc_Download_2._0.Class.Utils.DownLoad;

namespace Zony_Lrc_Download_2._0.Window
{
    public partial class Window_Donate : Form
    {
        public Window_Donate()
        {
            InitializeComponent();
        }

        private void Window_Donate_Load(object sender, EventArgs e)
        {
            Icon = Resource1._6;
            new Thread(() => {
                textBox1.Text = "正在加载...";
                string donateStr = new Tools().Http_Get("http://git.oschina.net/jokers/ZonyLrcDownload/blob/master/readme.md", Encoding.UTF8);
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Zony_Lrc_Download_2._0
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void About_Load(object sender, EventArgs e)
        {
            // 加载图标
            this.Icon = Zony_Lrc_Download_2._0.Resource1._6;
            // 加载日志
            textBox2.Text = Log.LoadLog();
        }
    }
}

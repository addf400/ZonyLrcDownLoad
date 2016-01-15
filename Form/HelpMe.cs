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
    public partial class HelpMe : Form
    {
        public HelpMe()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void HelpMe_Load(object sender, EventArgs e)
        {
            this.Icon = Zony_Lrc_Download_2._0.Resource1._6;

        }


    }
}

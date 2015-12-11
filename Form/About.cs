using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Zony_Lrc_Download_2._0
{
    public partial class About : Form
    {
        Socket socket;
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
            new Thread(update).Start();
        }

        /// <summary>
        /// 更新检测线程
        /// </summary>
        private void update()
        {

            IPAddress ip = IPAddress.Parse("139.129.119.134");
            IPEndPoint ipe = new IPEndPoint(ip, 7500);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                socket.Connect(ipe);
                Package pack = PackageDeal.Package_New((byte)PackageDeal.PType.UPDATE, 0, null);
                PackageDeal.Package_Write(socket, pack);
                pack = PackageDeal.Package_Read(socket);
                if (int.Parse(Encoding.UTF8.GetString(pack.data)) > PackageDeal.CurrentVersion)
                {
                    MessageBox.Show("有新版本！\n");
                }
            }
            catch (SocketException exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        private void About_FormClosing(object sender, FormClosingEventArgs e)
        {
            socket.Close();
        }
    }
}

using System;
using System.Windows.Forms;
using Zony_Lrc_Download_2._0.Window;

namespace Zony_Lrc_Download_2._0
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Window_Main());
        }
    }
}

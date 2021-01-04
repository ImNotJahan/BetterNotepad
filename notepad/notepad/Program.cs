using System;
using System.Windows.Forms;

namespace notepad
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            if (Environment.OSVersion.Version.Major == 6)
                SetProcessDPIAware();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1((args.Length > 0) ? args[0] : ""));
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}

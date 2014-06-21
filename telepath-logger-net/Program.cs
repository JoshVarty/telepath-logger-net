using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace telepath_logger_net
{
    class Program
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        [STAThread]
        static void Main(string[] args)
        {
            //Need to handle high DPIs on Windows 7+
            if (Environment.OSVersion.Version.Major >= 6)
                SetProcessDPIAware();

            var tracker = new ScreenTracker();
            tracker.Run();
            while (true)
            {
                System.Threading.Thread.Sleep(5000);
            }
        }
    }
}

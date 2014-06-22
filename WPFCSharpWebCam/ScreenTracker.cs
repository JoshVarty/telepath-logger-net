using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;

namespace telepath_logger_net
{
    /// <summary>
    /// Periodically takes a screenshot of the user's desktop.
    /// These screenshots are then saved, and can be later used 
    /// to render a video.
    /// </summary>
    public class ScreenTracker
    {
        private int _intervalInMs;
        private static System.Timers.Timer _timer;

        public ScreenTracker(int intervalInMs = 60000)
        {
            _intervalInMs = intervalInMs;
            _timer = new System.Timers.Timer(_intervalInMs);
        }

        public void Run()
        {
            _timer.Elapsed += _timer_Elapsed;
            _timer.Enabled = true;
        }

        public void Stop()
        {
            _timer.Enabled = false;
            _timer.Elapsed -= _timer_Elapsed; 
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //Disable timer to avoid overlapping ticks
            _timer.Enabled = false;

            //Do work
            System.Diagnostics.Debug.WriteLine("ScreenTracker timer elapsed");
            try
            {
                var screenshot = takeScreenShot();
                saveScreenShot(screenshot);
            }
            catch (Exception error)
            {
                System.Diagnostics.Debug.WriteLine(error);
            }

            //Work is complete, enable timer
            _timer.Enabled = true;
        }


        /// <summary>
        /// Takes a screenshot of the user's desktop.
        /// </summary>
        private Bitmap takeScreenShot()
        {
            //Create a bitmap in which we can save the screen.
            var bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                           Screen.PrimaryScreen.Bounds.Height,
                                           PixelFormat.Format32bppArgb);

            var gfx = Graphics.FromImage(bmpScreenshot);

            //Take the screenshot
            gfx.CopyFromScreen(Screen.PrimaryScreen.Bounds.X
                , Screen.PrimaryScreen.Bounds.Y
                , 0
                , 0
                , Screen.PrimaryScreen.Bounds.Size
                , CopyPixelOperation.SourceCopy);

            return bmpScreenshot;
        }

        private void saveScreenShot(Bitmap screenShot)
        {
            //This is where we're running the application
            string assemblyLocation = Utilities.AssemblyDirectory;
            const string CAPTURE_DIR = "screenshots";
            var date = DateTime.UtcNow.ToString("yyyyMMdd");

            var directoryPath = Path.Combine(assemblyLocation, CAPTURE_DIR, date);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var fileName = DateTime.UtcNow.ToString("yyyyMMddHHmmss") + ".jpg";
            var fullPath = Path.Combine(directoryPath, fileName);

            screenShot.Save(fullPath);
       } 
    }
}

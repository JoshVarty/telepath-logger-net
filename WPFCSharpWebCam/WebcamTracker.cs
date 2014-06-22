
using System.Windows.Controls;
using WebCam_Capture;

namespace telepath_logger_net
{
    public class WebCamTracker
    {
        private int _intervalInMs;
        private System.Timers.Timer _timer;

        WebCam webcam;
        public WebCamTracker(Image imgVideo, int intervalInMs = 60000)
        {
            webcam = new WebCam();
            webcam.InitializeWebCam(ref imgVideo);
            _intervalInMs = intervalInMs;
            _timer = new System.Timers.Timer();
        }

        public void Run()
        {
             webcam.Start();
            _timer.Elapsed += _timer_Elapsed;
            _timer.Enabled = true;
        }

        public void Stop()
        {
            _timer.Enabled = false;
            _timer.Elapsed -= _timer_Elapsed;
        }

        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //Disable timer to avoid overlapping ticks
            _timer.Enabled = false;

            //Do work
            System.Diagnostics.Debug.WriteLine("WebcamTracker timer elapsed");

            //Work is complete, enable timer
            _timer.Enabled = true;
        }
    }
}

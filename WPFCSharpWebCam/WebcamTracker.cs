
using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WebCam_Capture;

namespace telepath_logger_net
{
    public class WebCamTracker
    {
        private int _intervalInMs;
        private System.Timers.Timer _timer;
        private Image _imgVideo = new Image();
        private static Image _imgCapturedFrame = new Image();
        Window1 _parent;

        WebCam webcam;
        public WebCamTracker(Window1 parent, Image imgVideo, int intervalInMs = 1000)
        {
            _parent = parent;
            _imgVideo = imgVideo;
            webcam = new WebCam();
            webcam.InitializeWebCam(ref _imgVideo);
            _intervalInMs = intervalInMs;
            _timer = new System.Timers.Timer(intervalInMs);
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
            try
            {
                captureWebCamImage();
                saveWebCamImage();
            }
            catch (Exception error)
            {
                System.Diagnostics.Debug.WriteLine(error);
            }

            //Work is complete, enable timer
            _timer.Enabled = true;
        }

        private void captureWebCamImage()
        {
            _parent.Dispatcher.Invoke((Action)(() =>
            {
                _imgCapturedFrame.Source = _imgVideo.Source;
            }));
        }

        private void saveWebCamImage()
        {
            _parent.Dispatcher.Invoke((Action)(() =>
            {
                if (_imgCapturedFrame == null || _imgCapturedFrame.Source == null)
                {
                    System.Diagnostics.Debug.WriteLine("imgCapturedFrame or source was null");
                    return;
                }
                BitmapSource bitmap = (BitmapSource)(_imgCapturedFrame.Source);
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmap));
                encoder.QualityLevel = 100;

                string assemblyLocation = Utilities.AssemblyDirectory;
                const string WEBCAM_DIR = "webcam_captures";
                var date = DateTime.UtcNow.ToString("yyyyMMdd");

                var directoryPath = Path.Combine(assemblyLocation, WEBCAM_DIR, date);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var fileName = DateTime.UtcNow.ToString("yyyyMMddHHmmss") + ".jpg";
                fileName = Path.Combine(directoryPath, fileName);

                FileStream fstream = new FileStream(fileName, FileMode.Create);
                encoder.Save(fstream);
                fstream.Close();
            }));
        }
    }
}

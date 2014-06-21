using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCam_Capture;

namespace telepath_logger_net
{
    public class WebCam
    {
        private WebCamCapture webCam;
        private System.Windows.Forms.PictureBox _FrameImage;
        private int FrameNumber = 30;

        public void InitializeWebCam(ref System.Windows.Forms.PictureBox ImageControl)
        {
            webCam = new WebCamCapture();
            webCam.FrameNumber = ((ulong)(0ul));
            webCam.TimeToCapture_milliseconds = FrameNumber;
            webCam.ImageCaptured += new WebCamCapture.WebCamEventHandler(webcam_ImageCaptured);
            _FrameImage = ImageControl;
        }

        void webcam_ImageCaptured(object source, WebcamEventArgs e)
        {
            _FrameImage.Image = e.WebCamImage;
        }

        public void Start()
        {
            webCam.TimeToCapture_milliseconds = FrameNumber;
            webCam.Start(0);
        }

        public void Stop()
        {
            webCam.Stop();
        }

        public void Continue()
        {
            //change the capture time frame
            webCam.TimeToCapture_milliseconds = FrameNumber;

            // resume the video capture from the stop
            webCam.Start(this.webCam.FrameNumber);
        }

        public void ResolutionSetting()
        {
            webCam.Config();
        }

        public void AdvanceSetting()
        {
            webCam.Config2();
        }
    }
}

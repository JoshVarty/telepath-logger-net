using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public ScreenTracker(int intervalInMs = 1000)
        {
            _intervalInMs = intervalInMs;
        }
    }
}

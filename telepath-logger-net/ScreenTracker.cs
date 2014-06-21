using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

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
        private static Timer _timer;

        public ScreenTracker(int intervalInMs = 1000)
        {
            _intervalInMs = intervalInMs;
            _timer = new Timer(_intervalInMs);
        }

        public void Run()
        {
            _timer.Enabled = true;
            _timer.Elapsed += _timer_Elapsed;
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
            System.Diagnostics.Debug.WriteLine("Timer elapsed");

            //Work is complete, enable timer
            _timer.Enabled = true;
        }
    }
}

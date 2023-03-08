using System;

namespace PA.MarketApi.Bases
{
    public class TimeEventArgs : EventArgs
    {
        public TimeEventArgs(DateTime time)
        {
            Time = time;
        }

        public DateTime Time { get; private set; }
    }
}
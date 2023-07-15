using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace PA.Market.Model.BackTester
{
    public class BackTestClock
    {
        private Timer timer;
        public event EventHandler Tick = null;
        public DateTime Today { get; private set; }
        public int TickDelay { get; set; }
        public bool Auto { get; set; }
        public int MinuteInterval { get; set; }
        public BackTestClock(DateTime starttime) 
        {
            Today = starttime;
        }
        public void Start()
        {
            if (Auto)
            {
                timer = new Timer();
                timer.Interval = TickDelay;
                timer.Elapsed += Timer_Elapsed;
                timer.Start();
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Next();
        }

        private void Next()
        {
            Today = Today.AddMinutes(MinuteInterval);
            Tick?.Invoke(this, EventArgs.Empty);
        }
    }
}

using PA.MarketApi.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace PA.Market.Model.Bases
{
    public static class BinanceWeightChecker
    {
        static Timer dailytimer = new Timer();
        static Timer tenSecTimer = new Timer();
        static Timer wTimer = new Timer();
        static int total = 0;
        static int tenSec = 0;
        static int weight = 0;
        public static Dictionary<SessionBase, TimeSpan> utcDeff = new Dictionary<SessionBase, TimeSpan>();
        public static void Initilize()
        {
            dailytimer = new Timer();
            dailytimer.Interval = (24 * 60 * 60 * 1000);
            dailytimer.Elapsed += Dailytimer_Elapsed;

            tenSecTimer = new Timer();
            tenSecTimer.Interval = (10 * 1000);
            tenSecTimer.Elapsed += TenSecTimer_Elapsed;

            wTimer = new Timer();
            wTimer.Interval = (60 * 1000);
            wTimer.Elapsed += WTimer_Elapsed;

            total = 0;
            weight = 0;
            tenSec = 0;

            dailytimer.Start();
            tenSecTimer.Start();
            wTimer.Start();        }

        private static void WTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            weight = 0;
        }

        private static void TenSecTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            tenSec = 0;
        }

        private static void Dailytimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            dailytimer.Stop();
            tenSecTimer.Stop();
            wTimer.Stop();
            Initilize();
        }

        /* For the hard-limits, exceeding the total request weight per minute limit(currently 1,200) will result in an IP ban.
            The order limits(currently it is 50 times/10 seconds and 160,000 times/24 hours) will be dependent on the account.
            If the order limit has been exceeded, users will also be restricted from creating new orders on the website(or our other applications). */

        public static int LockIfNessesery()
        {
            if (tenSec >= 45)
            {
                return 10 * 1000;
            }
            if (weight >= 1100)
            {
                return 60 * 1000;
            }
            if (total >= 159900)
            {
                return (24 * 60 * 60 * 1000);
            }
            return 0;
        }

        public static void ProceedWeight(int count)
        {
            tenSec++;
            total++;
            AddWeight(count);
        }

        /* count > weight
                1,100 >	1
                100, 500 > 2
                500, 1000 >	5
                >1000 >	10
        */
        private static void AddWeight(int count)
        {
            if (count <= 100)
                weight++;
            else if (count <= 500)
                weight += 2;
            else if (count <= 1000)
                weight += 5;
            else
                weight += 10;
        }

        //public static DateTime GetServerTime(SessionBase session)
        //{
        //    if(utcDeff[session] == null)
        //    {
        //        int wait = BinanceWeightChecker.LockIfNessesery();
        //        if (wait > 0)
        //            Thread.Sleep(wait);
        //        DateTime d = await market.GetServerTime();
        //        BinanceWeightChecker.ProceedWeight(1);
        //        base.RaiseServerTimeReceived(d);
        //        return d;
        //    }
        //}
    }
}

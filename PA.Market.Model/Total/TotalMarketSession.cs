using Binance.Shared.Models;
using PA.MarketApi.Bases;
using PA.Trading.UAPI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PA.MarketApi.Total
{
    public class TotalSession : SessionBase
    {
        private TotalMarket market = null;
        public TotalSession(Interval interval) : base(interval)
        {
            market = new TotalMarket();
        }
        public override async Task<List<Candlestick>> FillGapAsync(DateTime start, DateTime end, List<Candlestick> existsKlines)
        {
            return await market.KlineCandlestick("", "", Symbol.ID, Interval, start, end, 0);
        }
        public override async Task<List<Candlestick>> GetCandlesAsync()
        {
            List<Candlestick> data;
            if (LastLoadedCandle != null)
            {
                DateTime? start = LastLoadedCandle?.OpenTime;
                DateTime? end = await GetServerTimeAsync();
                data = await market.KlineCandlestick("", "", Symbol.ID, Interval,
                start ?? DateTime.UtcNow,
                end ?? DateTime.UtcNow, 0);
            }
            else
            {
                data = await market.KlineCandlestick("", "", Symbol.ID, Interval, null, null, 264);
            }
            return data;
        }
        public override async Task<DateTime> GetServerTimeAsync()
        {
            DateTime d = await market.GetServerTime();
            base.RaiseServerTimeReceived(d);
            return d;
        }
        public override void LoadLastFromDB()
        {
            base.LoadLastFromDB();
        }
        public override async void StartAsync(int waitBefore)
        {
            base.StartAsync(waitBefore);
        }
        public override void Stop()
        {
            base.Stop();
        }
        protected override void DoWorkAsync()
        {
            //base.RaiseStarted();
            //while (!(processor.ThreadState == ThreadState.Aborted))
            //{
            //    base.RaiseFetching();
            //    List<Candlestick> candles = new List<Candlestick>();
            //    candles = await GetCandlesAsync();
            //    candles = candles.OrderByDescending(x => x.OpenTime).ToList();
            //    if (candles.Count > 0)
            //    {
            //        DataReceived?.Invoke(this, new MarketDataReceiveEventArgs(candles));
            //        AddNewCandles(candles);
            //    }
            //    if (processor.ThreadState == ThreadState.Aborted)
            //        return;
            //    TimeSpan t = Helper.GetSleepTime(Interval, await GetServerTimeAsync());
            //    Sleeping?.Invoke(this, new TimeEventArgs(new DateTime(t.Ticks)));
            //    Thread.Sleep(t);
            //}
            //Finished?.Invoke(this, EventArgs.Empty);
        }
        protected override void AddNewCandles(List<Candlestick> data)
        {
            base.AddNewCandles(data);
        }
    }
}

using Binance.Shared.Models;
using PA.Market.Model.BackTester;
using PA.MarketApi.Bases;
using PA.StockMarket.Data;
using PA.StockMarket.Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PA.MarketFetcher.Server
{
    public class BackTestFetcher : IFetcher
    {
        public ApplicationActiveSessionLogger Logger { get; private set; }
        public SessionBase Session { get; set; }
        public Interval Interval { get; set; }
        public Symbol Symbol { get; set; }
        public BackTestFetcher(ApplicationActiveSessionLogger logger, SessionBase session)
        {
            Logger = logger;
            Session = session;
            Interval = session.Interval;
            Symbol = session.Symbol;
            Session.ServerTimeReceived += Session_ServerTimeReceived;
            Session.Sleeping += Session_Sleeping;
            Session.Started += Session_Started;
            Session.Stoped += Session_Stoped;
            session.Finished += Session_Finished;
            session.Fetching += Session_Fetching;
            session.DataReceived += Session_DataReceived;
        }

        public event EventHandler<MarketDataReceiveEventArgs> CandleReceived;
        public event EventHandler<TimeEventArgs> ServerTimeReceived;

        public virtual void Start(int waitBefore)
        {
            try
            {
                Session.StartAsync(waitBefore);
            }
            catch
            {
                Thread.Sleep(5000);
                Start(0);
            }
        }
        public void Next()
        {
            var s = Session as BackTestSession;
            if(s != null)
            {
                s.Next();
            }
        }

        private void Session_Stoped(object sender, EventArgs e)
        {
            Logger.LogReceived(sender, "Session Stopped!!!", null);
        }

        private void Session_Started(object sender, EventArgs e)
        {

            Logger.LogReceived(sender, "Session Started!!!", null);
        }

        private void Session_Sleeping(object sender, TimeEventArgs e)
        {
            Logger.LogReceived(sender, string.Format("Session Sleeping To {0} later", e.Time.ToString("hh:mm:ss")), null);
        }

        private void Session_ServerTimeReceived(object sender, TimeEventArgs e)
        {
            Logger.LogReceived(sender, string.Format("Session Get Server Time : {0}", e.Time.ToString("yyyy/MM/dd hh:mm:ss")), null);
            ServerTimeReceived?.Invoke(this, e);
        }
        private void Session_DataReceived(object sender, MarketDataReceiveEventArgs e)
        {
            Logger.LogReceived(sender, "Candle Data Received!!!", e.Candles.ToArray());
            CandleReceived?.Invoke(this, e);
        }

        private void Session_Fetching(object sender, EventArgs e)
        {
            Logger.LogReceived(sender, "Session Start Fetching...", null);
        }

        private void Session_Finished(object sender, EventArgs e)
        {
            Logger.LogReceived(sender, "Session Work Finished!!!", null);
        }

        public virtual void Stop()
        {
            Session.Stop();
        }

        public async Task<List<Candlestick>> Pop(DateTime start, DateTime end)
        {
            List<Kline> klines = KlineDataProvider.GetInterval(Symbol.ID, Interval.ToString(), start, end);
            List<Candlestick> candles = new List<Candlestick>();
            foreach (Kline k in klines)
                candles.Add(k.CopyTo());
            return await Session.FillGapAsync(start, end, candles);
        }

        public List<Candlestick> Pop(int count, DateTime end)
        {
            List<Kline> klines = KlineDataProvider.GetInterval(Symbol.ID, Interval.ToString(), count);
            List<Candlestick> candles = new List<Candlestick>();
            foreach (Kline k in klines)
                candles.Add(k.CopyTo());
            // candles = await Session.FillGapAsync(end.AddDays(-20), end, candles);
            return candles.GetRange(candles.Count - count, count);
        }
    }
}

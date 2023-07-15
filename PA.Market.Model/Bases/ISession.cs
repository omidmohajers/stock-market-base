using Binance.Shared.Models;
using PA.StockMarket.Data;
using System;
using System.Collections.Generic;

namespace PA.MarketApi.Bases
{
    public interface ISession
    {
        event EventHandler Started;
        event EventHandler Fetching;
        event EventHandler Stoped;
        event EventHandler Finished;
        event EventHandler<MarketDataReceiveEventArgs> DataReceived;
        event EventHandler<TimeEventArgs> Sleeping;
        event EventHandler<TimeEventArgs> ServerTimeReceived;
        Interval Interval { get; set; }
        DateTime ServerTime { get; }
        Symbol Symbol { get; set; }
        PA.StockMarket.Data.Market Market { get; }
        Candlestick LastLoadedCandle { get; }
        List<Candlestick> Candles { get; }
        void StartAsync(int waitBefore);
        void Stop();
    }
}

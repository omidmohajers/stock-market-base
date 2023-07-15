using Binance.Shared.Models;
using PA.MarketApi.Bases;
using PA.StockMarket.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PA.MarketFetcher.Server
{
    public interface IFetcher
    {
        event EventHandler<MarketDataReceiveEventArgs> CandleReceived;
        event EventHandler<TimeEventArgs> ServerTimeReceived;
        SessionBase Session { get; set; }
        Interval Interval { get; set; }
        Symbol Symbol { get; set; }
        void Start(int waitBefore);
        void Stop();
        Task<List<Candlestick>> Pop(DateTime start, DateTime end);
        List<Candlestick> Pop(int count, DateTime end);
    }
}

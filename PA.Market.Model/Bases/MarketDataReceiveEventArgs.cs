using Binance.Shared.Models;
using System;
using System.Collections.Generic;

namespace PA.MarketApi.Bases
{
    public class MarketDataReceiveEventArgs : EventArgs
    {
        public List<Candlestick> Candles { get; private set; }

        public MarketDataReceiveEventArgs(List<Candlestick> candles)
        {
            Candles = candles;
        }
    }
}
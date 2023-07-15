using Binance.Shared.Models;
using PA.MarketApi.Bases;
using PA.MarketApi.Binance.fapi;
using PA.MarketFetcher.Server;
using PA.StockMarket.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PA.MarketFetcher
{
    public static class FetcherManager
    {
        public static List<IFetcher> Fetchers { get; private set; }
        public static ApplicationActiveSessionLogger Logger { get; set; }
        public static IFetcher AssignFetcher(Symbol symbol, Interval interval)
        {
            IFetcher fetcher = Fetchers.FirstOrDefault(f => f.Interval == interval && f.Symbol.ID == symbol.ID);
            if (fetcher == null)
            {
                SessionBase session = CreateSessionFromSymbol(symbol, interval);
                fetcher = new Fetcher(Logger, session);
                fetcher.Start(60000);
                Fetchers.Add(fetcher);
            }
            return fetcher;
        }

        public static void Init()
        {
            Fetchers = new List<IFetcher>();
            Logger = new ApplicationActiveSessionLogger();
        }
        public static void Add(SessionBase session, int delay)
        {
            Fetcher fetcher = new Fetcher(Logger, session);
            Fetchers.Add(fetcher);
            fetcher.Start(delay);
        }

        private static SessionBase CreateSessionFromSymbol(Symbol symbol, Interval interval)
        {
            switch (symbol.MarketID)
            {
                default:
                    return new FBinanceSession(interval) { Symbol = symbol };
            }
        }
        public static async Task<DateTime> GetServerTimeAsync(long marketID)
        {
            DateTime d = DateTime.UtcNow;
            IFetcher f = Fetchers.FirstOrDefault(o => o.Session.Symbol.MarketID == marketID);
            if (f != null)
            {
                d = await f.Session.GetServerTimeAsync();
            }
            return d;

        }
    }
}

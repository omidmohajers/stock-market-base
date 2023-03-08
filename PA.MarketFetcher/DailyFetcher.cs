using Binance.Shared.Models;
using PA.MarketApi.Bases;
using PA.StockMarket.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA.MarketFetcher.Server
{
    class DailyFetcher : Fetcher
    {
        public DailyFetcher(ApplicationActiveSessionLogger logger, SessionBase session) : base(logger, session)
        {
        }
    }
}

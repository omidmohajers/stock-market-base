using Binance.Common;
using Binance.Futures;
using Binance.Shared.Models;
using PA.Market.Model.Bases;
using PA.MarketApi.Bases;
using PA.StockMarket.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PA.MarketApi.Binance.fapi
{
    public class FBinanceSession : SessionBase
    {
        private FMarket market = null;
        private bool firstTimeFetch;

        public FBinanceSession(Interval interval) : base(interval)
        {
            firstTimeFetch = true;
            market = new FMarket();
        }
        public override async Task<List<Candlestick>> FillGapAsync(DateTime start, DateTime end, List<Candlestick> existsKlines)
        {
            if (existsKlines.Count == 0)
            {
                int wait = BinanceWeightChecker.LockIfNessesery();
                if (wait > 0)
                    Thread.Sleep(wait);
                try
                {
                    List<Candlestick> allGap = await market.KlineCandlestick(Symbol.Name, Interval,
                        Helper.DateTimeToUnixTimeStamp(start),
                        Helper.DateTimeToUnixTimeStamp(end), null);
                    BinanceWeightChecker.ProceedWeight(allGap.Count);
                    foreach (Candlestick cs in allGap)
                    {
                        InsertToDB(cs);
                    }
                    return allGap;
                }
                catch (BinanceServerException ex)
                {
                    //ex.StatusCode
                    Thread.Sleep(2000);
                    return await FillGapAsync(start ,end , existsKlines);
                }
                catch (BinanceHttpException ex)
                {
                    //ex.StatusCode
                    Thread.Sleep(2000);
                    return await FillGapAsync(start, end, existsKlines);
                }
                catch (Exception ex)
                {
                    Thread.Sleep(2000);
                    return await FillGapAsync(start, end, existsKlines);
                }
            }
            try
            {
                existsKlines = existsKlines.OrderBy(o => o.OpenTime).ToList();
                Candlestick first = existsKlines[0], last = existsKlines[existsKlines.Count - 1];
                List<Candlestick> bGap = new List<Candlestick>(), aGap = new List<Candlestick>();
                if (start < first.OpenTime)
                {
                    int wait = BinanceWeightChecker.LockIfNessesery();
                    if (wait > 0)
                        Thread.Sleep(wait);
                    bGap = await market.KlineCandlestick(Symbol.Name, Interval,
                        Helper.DateTimeToUnixTimeStamp(start),
                        Helper.DateTimeToUnixTimeStamp(first.OpenTime), null);
                    bGap = bGap.OrderBy(o => o.OpenTime).ToList();
                    BinanceWeightChecker.ProceedWeight(bGap.Count);
                    if (bGap.Count > 0 && bGap.Last().OpenTime == first.OpenTime)
                    {
                        DeleteFromDB(first);
                        existsKlines.Remove(first);
                    }
                    foreach (Candlestick cs in bGap)
                    {
                        InsertToDB(cs);
                    }
                }
                if (end > last.CloseTime)
                {
                    int wait = BinanceWeightChecker.LockIfNessesery();
                    if (wait > 0)
                        Thread.Sleep(wait);
                    aGap = await market.KlineCandlestick(Symbol.Name, Interval,
                        Helper.DateTimeToUnixTimeStamp(last.OpenTime),
                        Helper.DateTimeToUnixTimeStamp(end), null);
                    aGap = aGap.OrderBy(o => o.OpenTime).ToList();
                    BinanceWeightChecker.ProceedWeight(aGap.Count);
                    if (aGap.Count > 0 && aGap.First().OpenTime == last.OpenTime)
                    {
                        DeleteFromDB(last);
                        existsKlines.Remove(last);
                    }
                    foreach (Candlestick cs in aGap)
                    {
                        InsertToDB(cs);
                    }
                }
                bGap.AddRange(existsKlines);
                bGap.AddRange(aGap);
                return bGap;
            }
            catch (BinanceServerException ex)
            {
                //ex.StatusCode
                Thread.Sleep(10000);
                return await FillGapAsync(start, end, existsKlines);
            }
            catch (BinanceHttpException ex)
            {
                //ex.StatusCode
                Thread.Sleep(10000);
                return await FillGapAsync(start, end, existsKlines);
            }
            catch (Exception ex)
            {
                Thread.Sleep(10000);
                return await FillGapAsync(start, end, existsKlines);
            }
        }
        public override async Task<List<Candlestick>> GetCandlesAsync()
        {
            try
            {
                List<Candlestick> data = new List<Candlestick>();
                int wait = BinanceWeightChecker.LockIfNessesery();
                if (wait > 0)
                    Thread.Sleep(wait);
                if (firstTimeFetch)
                {
                    if (LastLoadedCandle != null)
                    {
                        DateTime? start = LastLoadedCandle?.OpenTime;
                        DateTime? end = await GetServerTimeAsync();
                        data = await market.KlineCandlestick(Symbol.Name, Interval,
                            Helper.DateTimeToUnixTimeStamp(start ?? DateTime.UtcNow),
                            Helper.DateTimeToUnixTimeStamp(end ?? DateTime.UtcNow), null);
                    }
                    else
                    {
                        data = await market.KlineCandlestick(Symbol.Name, Interval, null, null, 265);
                    }
                }
                else
                {
                    data = await market.KlineCandlestick(Symbol.Name, Interval, null, null, 2);
                }
                firstTimeFetch = false;
                BinanceWeightChecker.ProceedWeight(data.Count);
                //if (data.Count > 1)
                //    data.RemoveAt(data.Count - 1);
                return data;
            }
            catch (BinanceServerException ex)
            {
                //ex.StatusCode
                Thread.Sleep(10000);
                return await GetCandlesAsync();
            }
            catch (BinanceHttpException ex)
            {
                //ex.StatusCode
                Thread.Sleep(10000);
                return await GetCandlesAsync();
            }
            catch (Exception ex)
            {
                Thread.Sleep(10000);
                return await GetCandlesAsync();
            }
        }
        public override async Task<DateTime> GetServerTimeAsync()
        {
            int wait = BinanceWeightChecker.LockIfNessesery();
            if (wait > 0)
                Thread.Sleep(wait);
            try
            {
                DateTime d = await market.GetServerTime();

                BinanceWeightChecker.ProceedWeight(1);
                base.RaiseServerTimeReceived(d);
                return d;
            }
            catch (BinanceServerException ex)
            {
                //ex.StatusCode
                Thread.Sleep(2000);
                return await GetServerTimeAsync();
            }
            catch (BinanceHttpException ex)
            {
                //ex.StatusCode
                Thread.Sleep(2000);
                return await GetServerTimeAsync();
            }
            catch (Exception ex)
            {
                Thread.Sleep(2000);
                return await GetServerTimeAsync();
            }
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
    }
}

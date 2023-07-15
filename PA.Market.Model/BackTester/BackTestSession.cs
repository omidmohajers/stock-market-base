﻿using Binance.Common;
using Binance.Futures;
using Binance.Shared.Models;
using PA.Market.Model.Bases;
using PA.MarketApi.Bases;
using PA.StockMarket.Data;
using PA.StockMarket.Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace PA.Market.Model.BackTester
{
    public class BackTestSession : SessionBase
    {
        public DateTime ProcessTime { get; set; } = DateTime.UtcNow;
        public BackTestClock Clock { get; }
        private FMarket market = null;
        public BackTestSession(Interval interval,DateTime startTime,BackTestClock clock) : base(interval)
        {
            ProcessTime = startTime;
            Clock = clock; 
            market = new FMarket(); 
        }

        private void Clock_Tick(object sender, EventArgs e)
        {
            Next();
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
                catch (BinanceServerException)
                {
                    //ex.StatusCode
                    Thread.Sleep(2000);
                    return await FillGapAsync(start, end, existsKlines);
                }
                catch (BinanceHttpException)
                {
                    //ex.StatusCode
                    Thread.Sleep(2000);
                    return await FillGapAsync(start, end, existsKlines);
                }
                catch (Exception)
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
                DateTime end = Clock.Today;
               var klines = KlineDataProvider.GetInterval(Symbol.ID, Interval.ToString(), ProcessTime, end);
               
                if (klines.Count > 0)
                {
                    foreach(var kline in klines)
                    {
                        data.Add(kline.CopyTo());
                    }
                }
                data = await FillGapAsync(ProcessTime, end, data);
                if (data[data.Count - 1].CloseTime > ProcessTime)
                    ProcessTime = data[data.Count - 1].CloseTime.AddSeconds(1);
                return data;
            }
            catch
            {
                Thread.Sleep(10000);
                return await GetCandlesAsync();
            }
        }
        public override async Task<DateTime> GetServerTimeAsync()
        {
            try
            {
                DateTime d = DateTime.UtcNow;
                base.RaiseServerTimeReceived(d);
                return d;
            }
            catch
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
            Clock.Tick += Clock_Tick;
        }

        private void RepeatTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Next();
        }

        protected override async void DoWorkAsync()
        {
            if (Delay > 0)
            {
                Thread.Sleep(Delay);
            }
            Delay = 0;
            RaiseStarted();
            RaiseFetching();
            List<Candlestick> candles = new List<Candlestick>();
            try
            {
                candles = await GetCandlesAsync();
            }
            catch
            {
                Thread.Sleep(10 * 60 * 1000);
                DoWorkAsync();
            }
            candles = candles.OrderByDescending(x => x.OpenTime).ToList();
            if (candles.Count > 0)
            {
                RaiseDataReceived(candles);

            }
            if (processor.ThreadState == ThreadState.Aborted)
                return;
        }
        public void Next()
        {
            DoWorkAsync();
        }
        protected override void AddNewCandles(List<Candlestick> data)
        {
            base.AddNewCandles(data);
        }
        public override void Stop()
        {
            base.Stop();
        }
    }
}

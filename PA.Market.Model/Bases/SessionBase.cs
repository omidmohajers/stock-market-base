using Binance.Common;
using Binance.Shared.Models;
using PA.StockMarket.Data;
using PA.StockMarket.Data.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PA.MarketApi.Bases
{
    public class SessionBase : ISession
    {
        protected Thread processor;
        public Interval Interval { get; set; }
        public int Delay { get; protected set; }
        public DateTime ServerTime { get; protected set; }
        public Symbol Symbol { get; set; }
        public PA.StockMarket.Data.Market Market { get; private set; }
        public Candlestick LastLoadedCandle { get; protected set; }
        public List<Candlestick> Candles { get; protected set; }
        public string Name
        {
            get
            {
                return processor?.Name ?? string.Empty;
            }
        }

        public event EventHandler Started = null;
        public event EventHandler Stoped = null;
        public event EventHandler<MarketDataReceiveEventArgs> DataReceived = null;
        public event EventHandler Finished = null;
        public event EventHandler<TimeEventArgs> Sleeping = null;
        public event EventHandler<TimeEventArgs> ServerTimeReceived = null;
        public event EventHandler Fetching = null;

        public SessionBase(Interval interval)
        {
            Interval = interval;
            Candles = new List<Candlestick>();
        }
        public virtual async void StartAsync(int waitBefore)
        {
            Delay = waitBefore;
            ServerTime = await GetServerTimeAsync();
            processor = new Thread(new ThreadStart(DoWorkAsync));
            processor.Name = string.Format("{0}@kline_{1}", Symbol.Name, Interval.ToString());
            if (processor.ThreadState == ThreadState.Aborted)
                return;
            if (processor.ThreadState == ThreadState.Aborted)
                return;
            processor.Start();
        }
        public virtual async Task<DateTime> GetServerTimeAsync()
        {
            return await Task.Run(() => { return DateTime.UtcNow; });
        }

        public void RaiseServerTimeReceived(DateTime d)
        {
            ServerTimeReceived?.Invoke(this, new TimeEventArgs(d));
        }

        protected virtual async void DoWorkAsync()
        {
            if(Delay > 0)
            {
                Thread.Sleep(Delay);
            }
            Delay = 0;
            Started?.Invoke(this, EventArgs.Empty);
            LoadLastFromDB();
            while (!(processor.ThreadState == ThreadState.Aborted))
            {
                Fetching?.Invoke(this, EventArgs.Empty);
                List<Candlestick> candles = new List<Candlestick>();
                try
                {
                    candles = await GetCandlesAsync();
                }
                catch (BinanceHttpException)
                {
                    Thread.Sleep(10 * 60 * 1000);
                    DoWorkAsync();
                }
                catch (TaskCanceledException)
                {
                    Thread.Sleep(10 * 60 * 1000);
                    DoWorkAsync();
                }
                catch
                {
                    Finished?.Invoke(this, EventArgs.Empty);
                }
                candles = candles.OrderByDescending(x => x.OpenTime).ToList();
                if (candles.Count > 0)
                {
                    AddNewCandles(candles);
                    DataReceived?.Invoke(this, new MarketDataReceiveEventArgs(candles));
                }
                if (processor.ThreadState == ThreadState.Aborted)
                    return;
                double t = 0;
                try
                {
                    t = Convert.ToInt32(Helper.GetSleepTime(Interval, await GetServerTimeAsync()));

                }
                catch
                {
                    t = 10 * 60 * 1000;
                }
                TimeSpan time = TimeSpan.FromMilliseconds(t);
                Sleeping?.Invoke(this, new TimeEventArgs(new DateTime(1970, 1, 1) + time));
                Thread.Sleep(time);
            }
            Finished?.Invoke(this, EventArgs.Empty);
        }

        internal void RaiseFetching()
        {
            Fetching?.Invoke(this, EventArgs.Empty);
        }

        internal void RaiseStarted()
        {
            Started?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void AddNewCandles(List<Candlestick> data)
        {
            data = data.OrderBy(x => x.OpenTime).ToList();
            if (Candles.Count > 0 && data[0].OpenTime == Candles[0].OpenTime)
            {
                DeleteFromDB(Candles[0]);
                Candles[0] = data[0];
            }
            else
            {
                data[0].Interval = Interval.ToString();
                Candles.Insert(0, data[0]);
            }
            RecalculateATR(data[0]);
            InsertToDB(data[0]);
            LastLoadedCandle = data[0];
            data.RemoveAt(0);
            foreach (Candlestick cndl in data)
            {
                Candles.Insert(0, cndl);
                RecalculateATR(cndl);
                InsertToDB(cndl);
            }
            if (data.Count > 0)
                LastLoadedCandle = data.Last();
        }

        protected void DeleteFromDB(Candlestick candlestick)
        {
            KlineDataProvider.DeleteDuplicateCandles(Symbol.ID, Interval.ToString(), candlestick.OpenTime);
        }

        protected void RecalculateATR(Candlestick cndl)
        {
            lock (this)
            {
                if (Candles.Count > 14)
                {
                    Candlestick[] rsiArray = Candles.OrderBy(x => x.OpenTime).ToArray();
                    double[] value = new RSICalculator().CalculateRSIValuesV2(rsiArray, 14);
                    cndl.RSI = value[Candles.Count - 1];
                    value = ATRCalculator.Calculate(14, rsiArray);
                    cndl.ATR = value[Candles.Count - 1];
                    double[] ema = EMACalculator.Calculate(rsiArray, 14);
                    cndl.EMA = ema[Candles.Count - 1];
                    double[] rma = RMACalculator.Calculate(rsiArray, 14);
                    cndl.RMA = rma[Candles.Count - 1];
                    double[] sma = SMACalculator.Calculate(rsiArray, 14);
                    cndl.SMA = sma[Candles.Count - 1];
                }
            }
            //if (Candles.Count > 14)
            //{
            //    Candlestick[] rsiArray = Candles.OrderBy(x => x.OpenTime).ToArray();
            //    double[] rsi = new RSICalculator().CalculateRSIValuesV2(rsiArray, 14);
            //    double[] atr = ATRCalculator.Calculate(14, rsiArray);
            //    for (int j = 0; j < Candles.Count - 14; j++)
            //    {
            //        Candles[j + 1].RSI = rsi[Candles.Count - 2 - j];
            //        Candles[j + 1].ATR = atr[Candles.Count - 2 - j];
            //    }
            //    double[] ema = EMACalculator.Calculate(rsiArray, 14);
            //    int k = Candles.Count - 1;
            //    for (int i = 0; i < ema.Length; i++)
            //    {
            //        Candles[k--].EMA = ema[i];
            //    }
            //    double[] rma = RMACalculator.Calculate(rsiArray, 14);
            //    k = Candles.Count - 1;
            //    for (int i = 0; i < rma.Length; i++)
            //    {
            //        Candles[k--].RMA = rma[i];
            //    }
            //    double[] sma = SMACalculator.Calculate(rsiArray, 14);
            //    k = Candles.Count - 1;
            //    for (int i = 0; i < sma.Length; i++)
            //    {
            //        Candles[k--].SMA = sma[i];
            //    }
            //}
        }

        protected void InsertToDB(Candlestick cndl)
        {
            Kline k = new Kline();
            k.CopyData(cndl);
            k.Interval = Interval.ToString();
            k.SymbolID = Symbol.ID;
            KlineDataProvider.Insert(k);
        }

        public virtual async Task<List<Candlestick>> FillGapAsync(DateTime start,DateTime end, List<Candlestick> existsKlines)
        {
            return await Task.Run(() => { 
                return new List<Candlestick>();
            });
        }

        public virtual void LoadLastFromDB()
        {
            List<Kline> data = KlineDataProvider.GetLast(Symbol.ID, Interval.ToString());
            data = data.OrderBy(o => o.OpenUTCTime).ToList();
            if (data.Count == 0)
                return;
            foreach (Kline k in data)
            {
                Candlestick cs = k.CopyTo();
                cs.SymbolName = Symbol.Name;
                Candles.Insert(0, cs);
            }
            LastLoadedCandle = Candles[0];
        }

        public virtual async Task<List<Candlestick>> GetCandlesAsync()
        {
            return await Task.Run(() => { return new List<Candlestick>(); });
        }

        public virtual void Stop()
        {
            processor.Abort();
            Stoped?.Invoke(this, EventArgs.Empty);
        }
    }
}

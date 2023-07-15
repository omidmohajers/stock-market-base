using PA.MarketApi.Bases;
using System;
using System.Collections.Generic;

namespace PA.MarketFetcher.Server
{
    public class ApplicationActiveSessionLogger
    {
        public event EventHandler<LogData> LogInserted = null;
        public ApplicationActiveSessionLogger()
        {
            Logs = new List<LogData>();
        }
        public List<LogData> Logs { get; private set; }
        public void LogReceived(object sender, string msg, object[] data)
        {
            LogData ld = new LogData()
            {
                Time = DateTime.Now,
                Message = msg
            };
            if (sender is SessionBase)
            {
                SessionBase session = sender as SessionBase;
                ld.SenderOption = string.Format("{0}:{1} ({2})", session.Name, session.Symbol?.Name, session.Interval.ToString());
            }
            Logs.Add(ld);
            LogInserted?.Invoke(this, ld);
        }
    }
}

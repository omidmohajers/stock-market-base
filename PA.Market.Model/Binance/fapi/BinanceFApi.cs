using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PA.Trading.UAPI.Binance
{
    public class BinanceFApi : MarketBaseApi<Candlestick>
    {
        public BinanceFApi() : base()
        {

        }
        public override string TestConnectivityAsync()
        {
                var client = new RestClient("https://binance.com/");
                string requestString = "/fapi/v1/ping";
                var request = new RestRequest(requestString);
                var response = client.Post(request);
                var content = response.Content;
                return content;
        }
        public override DateTime GetServerTime()
        {
            var client = new RestClient("https://binance.com/");
            string requestString = "/fapi/v1/time";
            var request = new RestRequest(requestString);
            var response = client.Post(request);
            var content = response.Content;
            return Helper.UnixTimeStampToDateTime(Helper.ConvertStringToLong(content));
        }
        public override string ExchangeInfo()
        {
            var client = new RestClient("https://binance.com/");
            string requestString = "/fapi/v1/exchangeInfo";
            var request = new RestRequest(requestString);
            var response = client.Post(request);
            var content = response.Content;
            return content;
        }
        public override List<Candlestick> RequestAsync()
        {
                List<Candlestick> list = new List<Candlestick>();
                try
                {


                    var client = new RestClient("https://binance.com/");
                    // "https://fapi.binance.com/fapi/v1/klines?symbol=BTCUSDT&interval=5m&limit=3";
                    string requestString = "fapi/";
                    if (CadleCount > 0)
                    {
                        requestString = string.Format("fapi/{0}/klines?symbol={1}&interval={2}&limit={3}", Version, Symbol, Interval, CadleCount);
                    }
                    else
                    {
                        requestString = string.Format("fapi/{0}/klines?symbol={1}&interval={2}&startTime={3}&endTime={4}", Version, Symbol, Interval, Helper.DateTimeToUnixTimeStamp(StartDate), Helper.DateTimeToUnixTimeStamp(EndDate));
                    }
                    var request = new RestRequest(requestString);
                    var response = client.Post(request);
                    var content = response.Content;
                    string[] data = content.Split(new string[] { "],[" }, StringSplitOptions.RemoveEmptyEntries);
                    list = ConvertStringToSymbolPrice(data);

                }
                catch (Exception ex)
                {

                }
                return list;
        }

        public override List<Candlestick> ConvertStringToSymbolPrice(string[] cArray)
        {
            List<Candlestick> list = new List<Candlestick>();
            foreach(string s in cArray)
            {
                string[] fields = s.Split(',');
                Candlestick uc = new Candlestick();
                uc.OpenTime = Helper.GetJsonTime(fields[0]);
                uc.OpenPrice = Helper.ConvertStringToDecimal(fields[1]);
                uc.HighPrice = Helper.ConvertStringToDecimal(fields[2]);
                uc.LowPrice = Helper.ConvertStringToDecimal(fields[3]);
                uc.ClosePrice = Helper.ConvertStringToDecimal(fields[4]);
                uc.Volume = Helper.ConvertStringToDecimal(fields[5]);
                uc.CloseTime = Helper.GetJsonTime(fields[6]);
                uc.NumberOfTrades = Helper.ConvertStringToLong(fields[7]);
                list.Add(uc);
            }
            return list;
        }
    }
}

using Binance.Shared.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PA.Trading.UAPI
{
    public class TotalMarket 
    {
        public TotalMarket()
        {
            BaseUrl = "http://localhost/market/";
        }
        public string BaseUrl { get; set; }
        public async Task<List<Candlestick>> KlineCandlestick(string username,string password, long symbol,Interval interval,DateTime? start,DateTime? end,long count)
        {
            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri(BaseUrl);
            //    //HTTP GET
            //    var responseTask = client.GetAsync("kline");
            //    responseTask.Wait();

            //    var result = responseTask.Result;
            //    if (result.IsSuccessStatusCode)
            //    {

            //        var readTask = result.Content.ReadAsStreamAsync<List<Candlestick>();
            //        readTask.Wait();

            //        var students = readTask.Result;

            //        foreach (var student in students)
            //        {
            //            Console.WriteLine(student.Name);
            //        }
            //    }
            //}
            //Console.ReadLine();


            return await Task.Run(() =>
            {
                List<Candlestick> list = new List<Candlestick>();

                try
                {
                    var client = new RestClient(BaseUrl);
                    string requestString = "api/";
                    if (count == 0)
                        requestString = string.Format("api/Kline/{0}/{1}/{2}/{3}/{4}", symbol, interval.ToString(),
                                            Helper.DateTimeToUnixTimeStamp(start.Value),
                                            Helper.DateTimeToUnixTimeStamp(end.Value), 0);
                    else
                    {
                        requestString = string.Format("api/kline/get?symbol={0}&interval={1}&count={2}", symbol, interval.ToString(), count);
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
            });
        }

        public async Task<DateTime> GetServerTime()
        {
            return await Task.Run(() =>
            {
                DateTime date = DateTime.UtcNow;

                try
                {
                    var client = new RestClient(BaseUrl);
                    string requestString = "time/";
                    var request = new RestRequest(requestString);
                    var response = client.Post(request);
                    var content = response.Content;
                    long d = Helper.ConvertStringToLong(content);
                    date = Helper.UnixTimeStampToDateTime(d);
                }
                catch (Exception ex)
                {

                }

                return date;
            });
        }

        public List<Candlestick> ConvertStringToSymbolPrice(string[] cArray)
        {
            List<Candlestick> list = new List<Candlestick>();
            foreach (string s in cArray)
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

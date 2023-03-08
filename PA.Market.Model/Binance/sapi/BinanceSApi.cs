using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PA.Trading.UAPI.Binance.sapi
{
    public class BinanceSApi : RequestAsync<USymbolPrice>
    {
        public override List<USymbolPrice> RequestAsync()
        {
            List<USymbolPrice> list = new List<USymbolPrice>();

            try
            {
                var client = new RestClient("https://binance.com/");
                // "https://fapi.binance.com/fapi/v1/klines?symbol=BTCUSDT&interval=5m&limit=3";
                string requestString = "api/";
                if (string.IsNullOrWhiteSpace(Symbol))
                {
                    requestString = string.Format("api/v3/ticker/price", Version, Symbol, Interval.ToString().Substring(1), CadleCount);
                }
                else
                {
                    requestString = string.Format("api/v3/ticker/price?symbol={0}", Symbol);
                }
                // client.Authenticator = new HttpBasicAuthenticator(username, password);
                var request = new RestRequest(requestString);
                var response = client.Post(request);
                //while (!response.IsSuccessful)
                //{
                //    response = client.Post(request);
                //}
                var content = response.Content; // Raw content as string
                string[] data = content.Split(new string[] { "},{" }, StringSplitOptions.RemoveEmptyEntries);

                list = ConvertStringToSymbolPrice(data);
                //   var response2 = client.Post<Person>(request);
                //  var name = response2.Data.Name;
            }
            catch (Exception ex)
            {

            }

            return list;
        }

        public override List<USymbolPrice> ConvertStringToSymbolPrice(string[] cArray)
        {
            List<USymbolPrice> list = new List<USymbolPrice>();
            List<string[]> records = new List<string[]>();
            cArray[0] = cArray[0].Substring(2);
            foreach (string s in cArray)
            {
                string[] fields = s.Split(',');
                fields[0] = fields[0].Substring(10,fields[0].Length - 11);
              //  fields[1] = Helper.ConvertStringToDecimal(fields[1]).ToString();
                records.Add(fields);
            }

            var usdts = records.Where(x => x[0].EndsWith("USDT")).ToArray();
            string[] market = File.ReadAllLines("Spot.txt");
            for (int i = 0; i < market.Length; i++)
                market[i] = market[i].Trim();
            foreach (string[] pair in usdts)
            {
                USymbolPrice us = new USymbolPrice();
                us.Symbol = pair[0].Substring(0, pair[0].IndexOf("USDT"));
                if (!market.Contains(us.Symbol))
                    continue;
                us.USDTPrice = Helper.ConvertStringToDecimal(pair[1]);
                if (us.Symbol == "BTC")
                {
                    Helper.BtcUsdt = us.USDTPrice;
                }
                if (us.Symbol == "ETH")
                {
                    Helper.EthUsdt = us.USDTPrice;
                }
                if (us.Symbol == "BNB")
                {
                    Helper.BnbUsdt = us.USDTPrice;
                }
                if (us.Symbol == "BUSD")
                {
                    Helper.BusdUsdt = us.USDTPrice;
                }
                list.Add(us);
            }



            var btcs = records.Where(x => x[0].EndsWith("BTC")).ToArray();
            foreach (string[] pair in btcs)
            {
                string symbol = pair[0].Substring(0, pair[0].Length - 3);
                if (symbol == "REN")
                {

                }
                USymbolPrice us = list.FirstOrDefault(x => x.Symbol == symbol);
                if (us != null)
                    us.BTCPrice = Helper.ConvertStringToDecimal(pair[1]);
            }
            //    var bnbs = records.Where(x => x[0].EndsWith("BNB")).ToArray();
            //foreach (string[] pair in bnbs)
            //{
            //    string symbol = pair[0].Substring(0, pair[0].IndexOf("BNB"));
            //    USymbolPrice us = list.FirstOrDefault(x => x.Symbol == symbol);
            //    if (us != null)
            //        us.BNBPrice = Helper.ConvertStringToDecimal(pair[1]);
            //}
            //var eths = records.Where(x => x[0].EndsWith("ETH")).ToArray();
            //foreach (string[] pair in eths)
            //{
            //    string symbol = pair[0].Substring(0, pair[0].IndexOf("ETH"));
            //    USymbolPrice us = list.FirstOrDefault(x => x.Symbol == symbol);
            //    if (us != null)
            //        us.ETHPrice = Helper.ConvertStringToDecimal(pair[1]);
            //}

            return list;
        }
    }
}

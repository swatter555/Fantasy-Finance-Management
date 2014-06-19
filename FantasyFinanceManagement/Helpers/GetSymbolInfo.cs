using System;
using System.Collections.Generic;
using System.IO;
using System.Net;


namespace FantasyFinanceManagement.Helpers
{
    public class StockSymbolData
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public decimal Bid { get; set; }
        public decimal Ask { get; set; }
        public decimal Open { get; set; }
        public decimal PreviousClose { get; set; }
        public decimal Last { get; set; }

        public StockSymbolData(string symbol)
        {
            // Convert paramater to upper case
            symbol = symbol.ToUpper();

            // Construct symbol retrieval url
            string Url = "http://finance.yahoo.com/d/quotes.csv?s=" + symbol + "&f=snbaopl1";

            // Get stock data
            string symbolData = RetrieveSymbolData(Url);

            // Remove double qoutes
            symbolData = symbolData.Replace("\"", "");

            // Break data into columns
            string[] cols = symbolData.Split(',');

            // Fill in stock data
            Symbol = cols[0];
            Name = cols[1];

            // Temporary hold ref value
            decimal dec;

            // Attempt to parse decimal strings
            if (Decimal.TryParse(cols[2], out dec))
            {
                Bid = dec;
            }
            else
            {
                Bid = 0;
            }

            if (Decimal.TryParse(cols[3], out dec))
            {
                Ask = dec;
            }
            else
            {
                Ask = 0;
            }

            if (Decimal.TryParse(cols[4], out dec))
            {
                Open = dec;
            }
            else
            {
                Open = 0;
            }

            if (Decimal.TryParse(cols[5], out dec))
            {
                PreviousClose = dec;
            }
            else
            {
                PreviousClose = 0;
            }

            if (Decimal.TryParse(cols[6], out dec))
            {
                Last = dec;
            }
            else
            {
                Last = 0;
            }
        }

        public string RetrieveSymbolData(string Url)
        {
            string csvData;

            try
            {
                using(WebClient webClient = new WebClient())
                {
                    csvData = webClient.DownloadString(Url);
                }
            }
            catch (Exception e)
            {
                csvData = e.ToString();
            }

            return csvData;
        }
    }
}
using System;
using System.Collections.Generic;

namespace HTMLAPScraper
{
    public class HAPStock
    {
        private System.DateTime _timeScraped;
        private string _stockSymbol;
        private string _lastPrice;
        private string _change;
        private string _changePercent;


        public System.DateTime TimeScraped { get => _timeScraped; set => _timeScraped = value; }
        public string StockSymbol { get => _stockSymbol; set => _stockSymbol = value; }
        public string LastPrice { get => _lastPrice  ; set => _lastPrice = value; }
        public string Change { get => _change; set => _change = value; }

        public string ChangePercent { get => _changePercent; set => _changePercent = value; }

        public HAPStock()
        {
        }

        public HAPStock(System.DateTime timeScraped, string stockSymbol, string lastPrice, string change, string changePercent)
        {
            this.TimeScraped = timeScraped;
            this.StockSymbol = stockSymbol;
            this.LastPrice = lastPrice;
            this.Change = change;
            this.ChangePercent = changePercent;
        }
    }
}


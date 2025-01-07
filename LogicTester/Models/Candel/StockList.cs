using LogicTester.Models.Candel;

namespace StockLogger.Models.Candel
{
    public class StockList
    {
        public static List<Stock> GetStocks()
        {
            return new List<Stock>
            {
                new Stock { Ticker = "INFY-EQ", Exchange = "NSE", Name = "Infosys", Id = 1, SymbolToken = "1594" },
                new Stock { Ticker = "RELIANCE-EQ", Exchange = "NSE", Name = "Reliance Industries", Id = 2, SymbolToken = "2885" },
                new Stock { Ticker = "TCS-EQ", Exchange = "NSE", Name = "Tata Consultancy Services", Id = 3, SymbolToken = "11536" },
                new Stock { Ticker = "HDFCBANK-EQ", Exchange = "NSE", Name = "HDFC Bank", Id = 4, SymbolToken = "1333" },
                new Stock { Ticker = "ICICIBANK-EQ", Exchange = "NSE", Name = "ICICI Bank", Id = 5, SymbolToken = "4963" },
                new Stock { Ticker = "HINDUNILVR-EQ", Exchange = "NSE", Name = "Hindustan Unilever", Id = 6, SymbolToken = "1394" },
                new Stock { Ticker = "ITC-EQ", Exchange = "NSE", Name = "ITC Limited", Id = 7, SymbolToken = "1660" },
                new Stock { Ticker = "KOTAKBANK-EQ", Exchange = "NSE", Name = "Kotak Mahindra Bank", Id = 8, SymbolToken = "1922" },
                new Stock { Ticker = "LT-EQ", Exchange = "NSE", Name = "Larsen & Toubro", Id = 9, SymbolToken = "11483" },
                new Stock { Ticker = "SBIN-EQ", Exchange = "NSE", Name = "State Bank of India", Id = 10, SymbolToken = "3045" },
                new Stock { Ticker = "COALINDIA-EQ", Exchange = "NSE", Name = "Coal India", Id = 11, SymbolToken = "20374" },
                new Stock { Ticker = "TITAN-EQ", Exchange = "NSE", Name = "Titan Company", Id = 12, SymbolToken = "3506" },
                new Stock { Ticker = "BHARTIARTL-EQ", Exchange = "NSE", Name = "Bharti Airtel", Id = 13, SymbolToken = "10604" },
                new Stock { Ticker = "HCLTECH-EQ", Exchange = "NSE", Name = "HCL Technologies", Id = 14, SymbolToken = "7229" },
                new Stock { Ticker = "ASIANPAINT-EQ", Exchange = "NSE", Name = "Asian Paints", Id = 15, SymbolToken = "236" },
                new Stock { Ticker = "DMART-EQ", Exchange = "NSE", Name = "Avenue Supermarts", Id = 16, SymbolToken = "19913" },
                new Stock { Ticker = "MARUTI-EQ", Exchange = "NSE", Name = "Maruti Suzuki India", Id = 17, SymbolToken = "10999" },
                new Stock { Ticker = "SUNPHARMA-EQ", Exchange = "NSE", Name = "Sun Pharmaceutical Industries", Id = 18, SymbolToken = "3351" },
                new Stock { Ticker = "NTPC-EQ", Exchange = "NSE", Name = "NTPC Limited", Id = 19, SymbolToken = "11630" },
                new Stock { Ticker = "BAJFINANCE-EQ", Exchange = "NSE", Name = "Bajaj Finance", Id = 20, SymbolToken = "317" },
                new Stock { Ticker = "POWERGRID-EQ", Exchange = "NSE", Name = "Power Grid Corporation", Id = 21, SymbolToken = "14977" },
                new Stock { Ticker = "ULTRACEMCO-EQ", Exchange = "NSE", Name = "UltraTech Cement", Id = 22, SymbolToken = "11532" },
                new Stock { Ticker = "WIPRO-EQ", Exchange = "NSE", Name = "Wipro", Id = 23, SymbolToken = "3787" },
                new Stock { Ticker = "TECHM-EQ", Exchange = "NSE", Name = "Tech Mahindra", Id = 24, SymbolToken = "13538" },
                new Stock { Ticker = "BAJAJFINSV-EQ", Exchange = "NSE", Name = "Bajaj Finserv", Id = 25, SymbolToken = "16675" },
                new Stock { Ticker = "ONGC-EQ", Exchange = "NSE", Name = "Oil and Natural Gas Corporation", Id = 26, SymbolToken = "2475" },
                new Stock { Ticker = "HDFCLIFE-EQ", Exchange = "NSE", Name = "HDFC Life Insurance", Id = 27, SymbolToken = "467" },
                new Stock { Ticker = "SBILIFE-EQ", Exchange = "NSE", Name = "SBI Life Insurance", Id = 28, SymbolToken = "21808" },
                new Stock { Ticker = "M%26M-EQ", Exchange = "NSE", Name = "Mahindra & Mahindra", Id = 29, SymbolToken = "2031" },
                new Stock { Ticker = "DIVISLAB-EQ", Exchange = "NSE", Name = "Divi's Laboratories", Id = 30, SymbolToken = "10940" },
                new Stock { Ticker = "JSWSTEEL-EQ", Exchange = "NSE", Name = "JSW Steel", Id = 31, SymbolToken = "11723" },
                new Stock { Ticker = "ADANIENT-EQ", Exchange = "NSE", Name = "Adani Enterprises", Id = 32, SymbolToken = "25" },
                new Stock { Ticker = "BPCL-EQ", Exchange = "NSE", Name = "Bharat Petroleum Corporation", Id = 33, SymbolToken = "526" },
                new Stock { Ticker = "INDUSINDBK-EQ", Exchange = "NSE", Name = "IndusInd Bank", Id = 34, SymbolToken = "5258" },
                new Stock { Ticker = "CIPLA-EQ", Exchange = "NSE", Name = "Cipla", Id = 35, SymbolToken = "694" },
                new Stock { Ticker = "DRREDDY-EQ", Exchange = "NSE", Name = "Dr. Reddy's Laboratories", Id = 36, SymbolToken = "881" },
                new Stock { Ticker = "ADANIPORTS-EQ", Exchange = "NSE", Name = "Adani Ports and SEZ", Id = 37, SymbolToken = "15083" },
                new Stock { Ticker = "GRASIM-EQ", Exchange = "NSE", Name = "Grasim Industries", Id = 38, SymbolToken = "1232" },
                new Stock { Ticker = "HEROMOTOCO-EQ", Exchange = "NSE", Name = "Hero MotoCorp", Id = 39, SymbolToken = "1348" },
                new Stock { Ticker = "EICHERMOT-EQ", Exchange = "NSE", Name = "Eicher Motors", Id = 40, SymbolToken = "910" },
                new Stock { Ticker = "AXISBANK-EQ", Exchange = "NSE", Name = "Axis Bank", Id = 41, SymbolToken = "5900" },
                new Stock { Ticker = "TATAMOTORS-EQ", Exchange = "NSE", Name = "Tata Motors", Id = 42, SymbolToken = "3456" },
                new Stock { Ticker = "SHREECEM-EQ", Exchange = "NSE", Name = "Shree Cement", Id = 43, SymbolToken = "3103" },
                new Stock { Ticker = "APOLLOHOSP-EQ", Exchange = "NSE", Name = "Apollo Hospitals", Id = 44, SymbolToken = "157" },
                new Stock { Ticker = "BRITANNIA-EQ", Exchange = "NSE", Name = "Britannia Industries", Id = 45, SymbolToken = "547" },
                new Stock { Ticker = "UPL-EQ", Exchange = "NSE", Name = "UPL Limited", Id = 46, SymbolToken = "11287" },
                new Stock { Ticker = "PIDILITIND-EQ", Exchange = "NSE", Name = "Pidilite Industries", Id = 47, SymbolToken = "2664" },
                new Stock { Ticker = "VEDL-EQ", Exchange = "NSE", Name = "Vedanta", Id = 48, SymbolToken = "3063" },
                new Stock { Ticker = "BAJAJ-AUTO-EQ", Exchange = "NSE", Name = "Bajaj Auto", Id = 49, SymbolToken = "16669" },
                new Stock { Ticker = "NESTLEIND-EQ", Exchange = "NSE", Name = "Nestlé India", Id = 50, SymbolToken = "17963" }

            };
        }
    }

}

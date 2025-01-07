using LogicTester.Models.Candel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StockLogger.Models.Candel;
using System.Text;

namespace LogicTester.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AngelCandelController : ControllerBase
    {
        private readonly StockLoggerDbContext _context;
        private readonly List<(string ticker, string exchange, string name, long id, string symboltoken)> _stocks;

        public AngelCandelController(StockLoggerDbContext context)
        {
            _context = context;

            // Transform the list of Stock into a list of tuples
            _stocks = StockList.GetStocks()
                .Select(stock => (
                    ticker: stock.Ticker,
                    exchange: stock.Exchange,
                    name: stock.Name,
                    id: stock.Id,
                    symboltoken: stock.SymbolToken))
                .ToList();
        }

        // Fetches the public IP from ipify API
        private static async Task<string> GetPublicIPAsync()
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync("https://api.ipify.org?format=json");
                    response.EnsureSuccessStatusCode();
                    string content = await response.Content.ReadAsStringAsync();
                    dynamic ipData = JsonConvert.DeserializeObject(content);
                    return ipData.ip;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error fetching public IP: " + ex.Message);
                    return string.Empty;
                }
            }
        }

        // Model for stock request data
        public class StockRequest
        {
            public string SymbolToken { get; set; }
            public string AuthorizationToken { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }



        // POST https://localhost:7067/api/AngelCandel/getCandleData
        [HttpPost("getCandleData")]
        public async Task<IActionResult> GetCandleData([FromBody] StockRequest stockRequest)
        {
            // Extract only the date part from StartDate
            var startDateOnly = stockRequest.StartDate.Date;
            var EndDateOnly = stockRequest.EndDate.Date;

            var matchingStock = _stocks.FirstOrDefault(s => s.symboltoken == stockRequest.SymbolToken);

            // Create start date with time 9:15 AM
            var startDateWithTime900 = startDateOnly.AddHours(9).AddMinutes(15);

            // Create start date with time 3:30 PM
            var startDateWithTime330 = EndDateOnly.AddHours(15).AddMinutes(30);

            var data = new
            {
                exchange = "NSE",
                symboltoken = stockRequest.SymbolToken,
                interval = "ONE_MINUTE",
                fromdate = startDateWithTime900.ToString("yyyy-MM-dd HH:mm"),
                todate = startDateWithTime330.ToString("yyyy-MM-dd HH:mm")
            };

            var jsonData = JsonConvert.SerializeObject(data);
            var client = new HttpClient();

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://apiconnect.angelone.in/rest/secure/angelbroking/historical/v1/getCandleData")
            {
                Content = new StringContent(jsonData, Encoding.UTF8, "application/json")
            };

            // Set the headers
            requestMessage.Headers.Add("Accept", "application/json");
            requestMessage.Headers.Add("X-SourceID", "WEB");
            requestMessage.Headers.Add("X-ClientLocalIP", "192.168.56.177");  // Your local IP from ipconfig
            requestMessage.Headers.Add("X-ClientPublicIP", await GetPublicIPAsync());  // Fetching the public IP dynamically
            requestMessage.Headers.Add("X-MACAddress", "XX-XX-XX-XX-XX-XX"); // Replace with your actual MAC address
            requestMessage.Headers.Add("X-UserType", "USER");
            requestMessage.Headers.Add("Authorization", "Bearer " + stockRequest.AuthorizationToken);
            requestMessage.Headers.Add("X-PrivateKey", "DcsJlRJp"); // Your actual API Key

            try
            {
                // Send request to get historical data
                HttpResponseMessage response = await client.SendAsync(requestMessage);
                response.EnsureSuccessStatusCode();
                string responseContent = await response.Content.ReadAsStringAsync();
                dynamic candleData = JsonConvert.DeserializeObject(responseContent);
                var rawCandelData = candleData.data;

                List<Candel> ModifiedCandelDataList = new List<Candel>();

                if (rawCandelData == null)
                {
                    return null;
                }

                foreach (var rawCandel in rawCandelData)
                {
                    Candel newCandel = new Candel
                    {
                        OpenTime = DateTime.Parse(rawCandel[0].ToString()),
                        CloseTime = DateTime.Parse(rawCandel[0].ToString()).AddMinutes(1),

                        StartPrice = Convert.ToDecimal(rawCandel[1]),
                        HighestPrice = Convert.ToDecimal(rawCandel[2]),
                        LowestPrice = Convert.ToDecimal(rawCandel[3]),
                        EndPrice = Convert.ToDecimal(rawCandel[4]),

                        Ticker = matchingStock.ticker,
                        TickerId = matchingStock.id,
                        Exchange = matchingStock.exchange,

                        Volume = Convert.ToDecimal(rawCandel[5]),
                    };
                    newCandel.SetBullBearStatus();
                    newCandel.SetPriceChange();

                    ModifiedCandelDataList.Add(newCandel);
                }

                return Ok(ModifiedCandelDataList);  // Return the fetched historical candle data
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Error fetching candle data: " + ex.Message });
            }
        }



    }
}

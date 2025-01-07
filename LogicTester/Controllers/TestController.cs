using LogicTester.Models.Candel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OtpNet;
using StockLogger.Models.Candel;
using System.Text;

namespace LogicTester.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly StockLoggerDbContext _context;
        private readonly List<(string ticker, string exchange, string name, long id, string symboltoken)> _stocks;

        public TestController(StockLoggerDbContext context)
        {
            _context = context;

            // Fetch stocks from StockList and transform them into the required tuple format
            _stocks = StockList.GetStocks()
                .Select(stock => (stock.Ticker, stock.Exchange, stock.Name, stock.Id, stock.SymbolToken))
                .ToList();
        }

        [HttpGet]
        public IActionResult GetStocks()
        {
            var stocks = StockList.GetStocks();
            return Ok(stocks);
        }

        public class TestRequestModel
        {
            public string Symboltoken { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }

        public class BulkTestRequestModel
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }

        //POST https://localhost:7067/api/Test/BulkTestMasterAPI
        [HttpPost("BulkTestMasterAPI")]
        public async Task<IActionResult> BulkTestMasterAPI([FromBody] BulkTestRequestModel request)
        {
            string authtoken = await GetAuthorizationTokenAsync();

            List<List<List<Candel>>> MainCorrectPredictionList = new List<List<List<Candel>>>();
            List<List<List<Candel>>> MainWrongPredictionList = new List<List<List<Candel>>>();


            decimal MainProfit = 0;
            decimal MainLoss = 0;


            foreach (var stock in _stocks)
            {
                List<Candel> MasterList = new List<Candel>();

                List<Candel> DrafonFlyDojiCandels = new List<Candel>();

                List<List<Candel>> AnalyzerList = new List<List<Candel>>();

                List<List<Candel>> CorrectPredictionList = new List<List<Candel>>();
                List<List<Candel>> WrongPredictionList = new List<List<Candel>>();

                List<Candel> CandelData = new List<Candel>();

                // Variables for profit and loss tracking
                decimal TotalProfit = 0;
                decimal NetLoss = 0;

                var token = authtoken;

                var requestBody = new
                {
                    SymbolToken = stock.symboltoken,
                    AuthorizationToken = token,
                    StartDate = request.StartDate.ToString("o"), // ISO string format
                    EndDate = request.EndDate.ToString("o") // ISO string format
                };


                var client = new HttpClient();
                var jsonRequestBody = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");

                try
                {
                    var response = await client.PostAsync("https://localhost:7067/api/AngelCandel/getCandleData", content);

                    if (response.IsSuccessStatusCode)
                    {

                        var responseData = await response.Content.ReadAsStringAsync();
                        CandelData = JsonConvert.DeserializeObject<List<Candel>>(responseData);

                        List<Candel> dragonFlyDojiCandles = IdentifyDragonflyDojiCandles(CandelData);

                        MasterList = CandelData;
                        DrafonFlyDojiCandels = dragonFlyDojiCandles;

                        // Iterating through each Dragonfly Doji Candle
                        foreach (Candel dojiCandle in dragonFlyDojiCandles)
                        {
                            // Get the index of the Dragonfly Doji candle in the full list
                            int dojiIndex = CandelData.IndexOf(dojiCandle);

                            // List to store the next 5 candles
                            List<Candel> nextFiveCandles = new List<Candel>();

                            nextFiveCandles.Add(dojiCandle);

                            // Check if there are at least 5 more candles after the identified Dragonfly Doji candle
                            for (int i = dojiIndex + 1; i < dojiIndex + 6 && i < CandelData.Count; i++)
                            {
                                nextFiveCandles.Add(CandelData[i]);
                            }

                            // Add the list of next 5 candles to the AnalyzerList
                            AnalyzerList.Add(nextFiveCandles);
                        }

                        foreach (List<Candel> detectedCandelList in AnalyzerList)
                        {
                            Candel firstCandel = detectedCandelList[0];
                            Candel lastCandel = detectedCandelList[5];

                            bool isCorrectPrediction = detectedCandelList.Any(c => c.EndPrice > firstCandel.EndPrice);

                            if (isCorrectPrediction)
                            {
                                Candel candelThatSatisfiesCondition = detectedCandelList.FirstOrDefault(c => c.EndPrice > firstCandel.EndPrice);

                                CorrectPredictionList.Add(detectedCandelList);
                                MainCorrectPredictionList.Add(CorrectPredictionList);

                                TotalProfit = TotalProfit + (candelThatSatisfiesCondition.EndPrice - firstCandel.EndPrice);
                                MainProfit = MainProfit + (candelThatSatisfiesCondition.EndPrice - firstCandel.EndPrice);
                            }
                            else
                            {
                                foreach (var detectedCandel in detectedCandelList)
                                {
                                    // Find the candel meeting the criteria
                                    var matchingCandel = CandelData
                                        .Where(c => c.EndPrice > detectedCandel.EndPrice && c.OpenTime > detectedCandel.OpenTime)
                                        .FirstOrDefault();

                                    if (matchingCandel != null)
                                    {
                                        CorrectPredictionList.Add(detectedCandelList);

                                        MainCorrectPredictionList.Add(CorrectPredictionList);

                                        TotalProfit = TotalProfit + (matchingCandel.EndPrice - detectedCandel.EndPrice);
                                        MainProfit = MainProfit + (matchingCandel.EndPrice - detectedCandel.EndPrice);
                                    }
                                    else
                                    {
                                        WrongPredictionList.Add(detectedCandelList);
                                        MainWrongPredictionList.Add(WrongPredictionList);
                                        NetLoss = NetLoss + (firstCandel.EndPrice - detectedCandel.EndPrice);
                                        MainLoss = MainLoss + (firstCandel.EndPrice - detectedCandel.EndPrice);
                                    }
                                }

                            }
                        }

                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode}");
                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }

                
                


            }

            return Ok(new
            {
                MainProfit,
                MainLoss,
                MainCorrectPredictionList,
                MainWrongPredictionList
            });

        }

        //POST https://localhost:7067/api/Test/TestMasterAPI
        [HttpPost("TestMasterAPI")]
        public async Task<IActionResult> TestMasterAPI([FromBody] TestRequestModel request)
        {
            string authtoken = await GetAuthorizationTokenAsync();

            List<Candel> MasterList = new List<Candel>();

            List<Candel> DrafonFlyDojiCandels = new List<Candel>();

            List<List<Candel>> AnalyzerList = new List<List<Candel>>();

            List<List<Candel>> CorrectPredictionList = new List<List<Candel>>();
            List<List<Candel>> WrongPredictionList = new List<List<Candel>>();

            // Variables for profit and loss tracking
            decimal TotalProfit = 0;
            decimal NetLoss = 0;

            List<Candel> CandelData = new List<Candel>();

                var token = authtoken;

                var requestBody = new
                {
                    SymbolToken = request.Symboltoken,
                    AuthorizationToken = token,
                    StartDate = request.StartDate.ToString("o"), // ISO string format
                    EndDate = request.EndDate.ToString("o") // ISO string format
                };

                var client = new HttpClient();
                var jsonRequestBody = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");

                try
                {
                    var response = await client.PostAsync("https://localhost:7067/api/AngelCandel/getCandleData", content);

                if (response.IsSuccessStatusCode)
                {

                    var responseData = await response.Content.ReadAsStringAsync();
                    CandelData = JsonConvert.DeserializeObject<List<Candel>>(responseData);

                    List<Candel> dragonFlyDojiCandles = IdentifyDragonflyDojiCandles(CandelData);

                    MasterList = CandelData;
                    DrafonFlyDojiCandels = dragonFlyDojiCandles;

                    // Iterating through each Dragonfly Doji Candle
                    foreach (Candel dojiCandle in dragonFlyDojiCandles)
                    {
                        // Get the index of the Dragonfly Doji candle in the full list
                        int dojiIndex = CandelData.IndexOf(dojiCandle);

                        // List to store the next 5 candles
                        List<Candel> nextFiveCandles = new List<Candel>();

                        nextFiveCandles.Add(dojiCandle);

                        // Check if there are at least 5 more candles after the identified Dragonfly Doji candle
                        for (int i = dojiIndex + 1; i < dojiIndex + 6 && i < CandelData.Count; i++)
                        {
                            nextFiveCandles.Add(CandelData[i]);
                        }

                        // Add the list of next 5 candles to the AnalyzerList
                        AnalyzerList.Add(nextFiveCandles);
                    }

                    foreach (List<Candel> detectedCandelList in AnalyzerList)
                    {
                        Candel firstCandel = detectedCandelList[0];
                        Candel lastCandel = detectedCandelList[5];

                        bool isCorrectPrediction = detectedCandelList.Any(c => c.EndPrice > firstCandel.EndPrice);

                        if (isCorrectPrediction)
                        {
                            Candel candelThatSatisfiesCondition = detectedCandelList.FirstOrDefault(c => c.EndPrice > firstCandel.EndPrice);

                            CorrectPredictionList.Add(detectedCandelList);
                             
                            TotalProfit = TotalProfit + (candelThatSatisfiesCondition.EndPrice - firstCandel.EndPrice); 
                        }
                        else
                        {
                            WrongPredictionList.Add(detectedCandelList);
                            NetLoss = NetLoss + (firstCandel.EndPrice - lastCandel.EndPrice);
                        }
                    }

                }
                else
                {
                          Console.WriteLine($"Error: {response.StatusCode}");
                      }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }

            return Ok(new
            {
                //AnalyzerList = AnalyzerList,                //AnalyzerList = AnalyzerList,
                CorrectPredictionList = CorrectPredictionList.Count,
                WrongPredictionList = WrongPredictionList.Count,
                TotalProfit = TotalProfit,
                NetLoss = NetLoss
            });

        }


        // Function to identify Dragonfly Doji candles
        private List<Candel> IdentifyDragonflyDojiCandles(List<Candel> CandelData)
        {
            List<Candel> dragonFlyDojiCandles = new List<Candel>();

            foreach (var c in CandelData)
            {
                Candel recentCandel = c;

                // Check if it is a Doji with a small body
                bool isDoji = Math.Abs(recentCandel.StartPrice - recentCandel.EndPrice) < (recentCandel.HighestPrice - recentCandel.LowestPrice) * 0.1m;

                // Check for a long lower shadow (shadow size relative to the body)
                bool longLowerShadow = (recentCandel.StartPrice - recentCandel.LowestPrice) > 3 * (recentCandel.EndPrice - recentCandel.StartPrice);

                // The body of the candle should be small and at the top of the range
                bool smallBodyAtTop = Math.Abs(recentCandel.StartPrice - recentCandel.EndPrice) < (recentCandel.HighestPrice - recentCandel.LowestPrice) * 0.3m;

                //// Preceding candle's trend should be bullish (for confirming upward momentum)
                //bool precedingBullishTrend = CandelData.Where(x => x.CloseTime < recentCandel.OpenTime)
                //                                       .OrderByDescending(x => x.CloseTime)
                //                                       .Take(3)
                //                                       .All(x => x.EndPrice > x.StartPrice); // At least the last 3 candles should be bullish

                //// Check for higher volume
                //bool higherVolume = recentCandel.Volume > CandelData.Average(x => x.Volume);

                ////bool higherVolume = recentCandel.Volume > CandelData.TakeLast(10).Max(x => x.Volume) * 0.75m; // Volume above 75% of the max in last 10 candles


                //// The next candle should also be bullish for confirmation
                //bool nextCandleBullish = CandelData.Where(x => x.OpenTime > recentCandel.CloseTime)
                //                                   .OrderBy(x => x.OpenTime)
                //                                   .FirstOrDefault()?.EndPrice > recentCandel.EndPrice;

                //Candel verificationCandel = CandelData.Where(x => x.OpenTime > recentCandel.CloseTime)
                //                                   .OrderBy(x => x.OpenTime)
                //                                   .FirstOrDefault();

                // If all conditions match, then it's a Dragonfly Doji with high probability of upward movement
                if (isDoji && longLowerShadow && smallBodyAtTop 
                    //&& precedingBullishTrend && higherVolume && nextCandleBullish
                    )
                {
                    dragonFlyDojiCandles.Add(recentCandel);
                }
            }

            return dragonFlyDojiCandles;
        }






        // Helper method to fetch the JWT token
        private async Task<string> GetAuthorizationTokenAsync()
        {
            string authorizationToken = string.Empty;

            // Fetch public IP using ipify API
            string publicIp = await GetPublicIPAsync();

            // Setup login credentials and generate TOTP
            var loginData = new
            {
                clientcode = "AAAF282130",  // Your actual client code
                password = "6366",          // Your actual pin
                totp = GenerateTOTP("3IGPCM52A2WTQCH7FW2RYOCYIY") // Generate TOTP from secret key
            };

            var loginJsonData = JsonConvert.SerializeObject(loginData);
            var loginClient = new HttpClient();
            var loginRequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://apiconnect.angelone.in/rest/auth/angelbroking/user/v1/loginByPassword")
            {
                Content = new StringContent(loginJsonData, Encoding.UTF8, "application/json")
            };

            // Set headers for login request
            loginRequestMessage.Headers.Add("Accept", "application/json");
            loginRequestMessage.Headers.Add("X-UserType", "USER");
            loginRequestMessage.Headers.Add("X-SourceID", "WEB");
            loginRequestMessage.Headers.Add("X-ClientLocalIP", "192.168.56.177");  // Your local IP from ipconfig
            loginRequestMessage.Headers.Add("X-ClientPublicIP", publicIp);
            loginRequestMessage.Headers.Add("X-MACAddress", "XX-XX-XX-XX-XX-XX"); // Replace with your MAC address
            loginRequestMessage.Headers.Add("X-PrivateKey", "DcsJlRJp");          // Your actual API Key

            try
            {
                // Send login request and fetch login token
                HttpResponseMessage loginResponse = await loginClient.SendAsync(loginRequestMessage);
                loginResponse.EnsureSuccessStatusCode();  // Throws an exception if not successful
                string loginResponseContent = await loginResponse.Content.ReadAsStringAsync();
                dynamic loginResponseJson = JsonConvert.DeserializeObject(loginResponseContent);

                // Check login status
                if (loginResponseJson.status == true)
                {
                    authorizationToken = loginResponseJson.data.jwtToken;  // Assuming the token is present here
                }
                else
                {
                    throw new Exception("Failed to authenticate: " + loginResponseJson.message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during login: {ex.Message}");
                throw;
            }

            return authorizationToken;
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

        // Generate TOTP based on the secret key
        private static string GenerateTOTP(string secretKey)
        {
            var otp = new Totp(Base32Encoding.ToBytes(secretKey));
            return otp.ComputeTotp(); // Generates the TOTP value
        }




    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using Newtonsoft.Json;
using StockAPI_Testx;

class Program
{
    static async System.Threading.Tasks.Task Main(string[] args)
    {
        string apiKey = ConfigurationManager.AppSettings["ApiKey"];
        string date = DateTime.Now.AddHours(-24).ToString("yyyy-MM-dd");  // The free API version only delivers end of day data. A 24h delay is required.
        string ticker = GetTickerSymbolFromUser();

        string url = $"https://api.polygon.io/v1/open-close/{ticker}/{date}?adjusted=true&apiKey={apiKey}";

        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();
            Stock stock = JsonConvert.DeserializeObject<Stock>(json);
            DisplayRequestedParameters(stock);
        }
        else
        {
            Console.WriteLine();
            Console.Write(response.ReasonPhrase);
            Console.ReadLine();
        }
    }


    private static string GetTickerSymbolFromUser()
    {
        string ticker;
        Console.WriteLine("Enter a stock ticker to find out its recent price. There is a 24h delay. " + "\n");
        Console.Write("Please enter a ticker symbol: ");

        do
        {
            ticker = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(ticker))
            {
                Console.Write("Ticker musn't be empty. Please enter a valid stock ticker: ");
            }
        }
        while (string.IsNullOrWhiteSpace(ticker));

        return ticker.ToUpper();
    }

    private static List<decimal> GetRequestedParametersFromUser(Stock stock)
    {
        Console.WriteLine("Enter all requested parameters. Press Enter to continue and display the data." + "\n");

        List<decimal> parameters = new List<decimal>();

        Console.WriteLine("(1) Open Price");
        Console.WriteLine("(2) Close Price");
        Console.WriteLine("(3) Lowest Price");
        Console.WriteLine("(4) Highest Price");
        Console.WriteLine("(5) Pre Market Price");
        Console.WriteLine("(6) After Hours Price" + "\n");

        bool wantsToContinue = false;

        do
        {
            ConsoleKeyInfo userInput = Console.ReadKey(true);

            switch (userInput.Key)
            {
                case ConsoleKey.D1:
                    parameters.Add(stock.Open);
                    break;
                case ConsoleKey.D2:
                    parameters.Add(stock.Close);
                    break;
                case ConsoleKey.D3:
                    parameters.Add(stock.Low);
                    break;
                case ConsoleKey.D4:
                    parameters.Add(stock.High);
                    break;
                case ConsoleKey.D5:
                    parameters.Add(stock.PreMarket);
                    break;
                case ConsoleKey.D6:
                    parameters.Add(stock.AfterHours);
                    break;
                case ConsoleKey.Enter:
                    wantsToContinue = true;
                    break;
                default:
                    break;
            }
        }
        while (!wantsToContinue);

        return parameters;
    }
    private static void DisplayRequestedParameters(Stock stock)
    {
        List<decimal> requestedParamters = GetRequestedParametersFromUser(stock);

        for (int i = 0; i < requestedParamters.Count; i++)
        {
            Console.WriteLine(requestedParamters[i].ToString());
        }

        Console.ReadLine();
    }
}



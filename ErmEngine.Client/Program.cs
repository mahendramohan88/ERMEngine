using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ErmEngine.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the ERM Engine.");
            Console.WriteLine("Enter the path to the CSV file you want to analyse:");

            var path = Console.ReadLine();
            Console.WriteLine("Processing...");
                                   
            var result = GetAnomalies(path).Result;

            var formattedResult = result.Replace(@"\n", Environment.NewLine).Replace(@"\t", "\t");

            Console.WriteLine("\n\nFile Name\t\t\t\tDate/Time\t\tValue\tMedian");
            Console.Write(formattedResult);

            var exit = Console.ReadLine();
        }

        static async Task<string> GetAnomalies(string path)
        {
            var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
            var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
            var client = httpClientFactory.CreateClient();

            client.BaseAddress = new Uri("https://localhost:44382");
            
            string jsonInput = JsonConvert.SerializeObject(path);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var stringContent = new StringContent(jsonInput);
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            var response = await client.PostAsync("/api/GetSummary", stringContent);
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}

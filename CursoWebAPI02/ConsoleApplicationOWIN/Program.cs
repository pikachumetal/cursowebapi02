using Microsoft.Owin.Hosting;
using System;
using System.Net.Http;

namespace ConsoleApplicationOWIN
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseAddress = "http://localhost:9000/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                // Create HttpCient and make a request to api/values 
                var client = new HttpClient();
                var response = client.GetAsync(baseAddress + "api/v3/persons").Result;
                Console.WriteLine(response);
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);

                Console.WriteLine("Press any keyto close the API...");
                Console.ReadLine();
            }
        }
    }
}

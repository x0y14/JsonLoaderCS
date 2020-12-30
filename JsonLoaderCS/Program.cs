using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using StringNumConverter;
using JsonLoaderCS;

namespace JsonLoaderCS
{
    internal static class Program
    {
        private static readonly HttpClient client = new HttpClient();
        private static async Task<string> GetJson()
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try	
            {
                HttpResponseMessage response = await client.GetAsync("https://httpbin.org/json");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                Console.WriteLine(responseBody);
                return responseBody;
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");	
                Console.WriteLine("Message :{0} ",e.Message);
            }
            return "";
        }
        public static void Main(string[] args)
        {
            var s = GetJson().Result;
            
            var dict = new JsonLoaderCS(s);
            var map = dict.Load();
            dict.CheckData(dict.Loaded);
            
            Console.WriteLine(map["slideshow"]["slides"][1]["items"][0]);
            Console.WriteLine(dict.Get("slideshow/slides.1/items.0"));
            // Why <em>WonderWidgets</em> are great

        }
    }
}

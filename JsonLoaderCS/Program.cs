using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using JsonLoader;

namespace JsonLoaderCS
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var dict = new JsonLoader.Loader();
            var map1 = dict.LoadStringAsJson("{ \"title\": \"test\", \"items\": [ 9999, \"hello\", {\"list\": [ 123 ] } ] }");
            Console.WriteLine(
                $"{dict.Get("title")} : {dict.Get("title") == map1["title"]}, " +
                $"{dict.Get("items.0")} : {dict.Get("items.0") == map1["items"][0]}, " +
                $"{dict.Get("items.2/list.0")} : {dict.Get("items.2/list.0") == map1["items"][2]["list"][0]}"
            );

            var map2 = dict.LoadWithPath("/Users/x0y14/dev/csharp/JsonLoaderCS/JsonLoaderCS/test.json");
            Console.WriteLine(
                $"{dict.Get("title")} : {dict.Get("title") == map2["title"]}, " +
                $"{dict.Get("items.0")} : {dict.Get("items.0") == map2["items"][0]}, " +
                $"{dict.Get("items.2/list.0")} : {dict.Get("items.2/list.0") == map2["items"][2]["list"][0]}"
            );
        }
    }
}

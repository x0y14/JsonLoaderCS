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
            var dict = new JsonLoader.Loader(
                "{ \"title\": \"test\", \"items\": [ 9999, \"hello\", {\"list\": [ 123 ] } ] }");
            var map = dict.Load();
            Console.WriteLine(
                $"{dict.Get("title")} : {dict.Get("title") == map["title"]}, " +
                $"{dict.Get("items.0")} : {dict.Get("items.0") == map["items"][0]}, " +
                $"{dict.Get("items.2/list.0")} : {dict.Get("items.2/list.0") == map["items"][2]["list"][0]}"
            );
        }
    }
}

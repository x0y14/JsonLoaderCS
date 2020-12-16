using System;
using System.Collections.Generic;
using System.ComponentModel;
using StringNumConverter;
using JsonParser;

namespace JsonLoaderCS
{
    static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello, StringNumConverter v0.1!");
            // _ = new StringNumConverter.Converter("111").Calc();
            var d1 = new JsonParser.JsonParser("{\"key\":12344}").Parse();
            var d2 = new JsonParser.JsonParser("{\"key\": \"value\"}").Parse();
            var d3 = new JsonParser.JsonParser("{\"key\": {\"next_key\": 404} }").Parse();
            var d4 = new JsonParser.JsonParser("{\"k1\": \"v1\", \"k2\": \"v2\"}").Parse();
            var d5 = new JsonParser.JsonParser("{ 0: \"hello\"}").Parse();
            var d6 = new JsonParser.JsonParser("{ 0: [ {\"key\": true}, 88, [0,0,0,0], \"hello\" ]}").Parse();
            Console.WriteLine("[end]");
        }
    }
}

using System;
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
            _ = new StringNumConverter.Converter("111").Calc();
            _ = new JsonParser.JsonParser("{\"key\":12344}").Parse();
            _ = new JsonParser.JsonParser("{\"key\": \"value\"}").Parse();
            Console.WriteLine("done");

        }
    }
}

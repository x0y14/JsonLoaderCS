using System;
using StringNumConverter;

namespace JsonLoader
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello, StringNumConverter v0.1!");
            var con = new Converter("111");
            con.Main();
        }
    }
}

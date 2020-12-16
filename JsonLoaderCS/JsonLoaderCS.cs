using System;
using System.Collections.Generic;
using JsonParser;

namespace JsonLoaderCS
{
    public class JsonLoaderCS
    {
        public string JsonData;
        public readonly double version = 0.9;
        public Dictionary<string, dynamic> Loaded;

        public JsonLoaderCS(string json)
        {
            JsonData = json;
        }

        public Dictionary<string, dynamic> Load()
        {
            Loaded = new JsonParser.JsonParser(JsonData).Parse();
            return Loaded;
        }

        public void CheckData(int n, Dictionary<string, dynamic> JsonObj)
        {
            var nest = n;
            foreach (var j in JsonObj)
            {
                Console.WriteLine($"{new String(' ', nest)}[Dict(KEY): {j.Key}]");
                if (j.Value is Dictionary<string, dynamic>)
                {
                    CheckData(nest+=2, j.Value);
                }
                else if (j.Value is List<dynamic>)
                {
                    CheckList(nest+=2, j.Value);
                }
                
                else
                {
                    if (j.Value is null)
                    {
                        Console.WriteLine($"{new String(' ', nest + 2)}| {j.Key}: null(null)");
                    }
                    else
                    {
                        Console.WriteLine($"{new String(' ', nest + 2)}| {j.Key}: {j.Value}({j.Value.GetType()})");
                    }
                }
            }
        }

        public void CheckList(int n, List<dynamic> items)
        {
            var nest = n;
            // Console.WriteLine("hello, list");
            Console.WriteLine($@"{new String(' ', nest)}[List]");
            foreach (var i in items)
            {
                if (i is List<dynamic>)
                {
                    CheckList(n, i);
                }
                else if (i is Dictionary<string, dynamic>)
                {
                    CheckData(n, i);
                }
                else
                {
                    if (i is null)
                    {
                        Console.WriteLine($"{new String(' ', nest)}| null(null)");
                    }
                    else
                    {
                        Console.WriteLine($"{new String(' ', nest)}| {i}({i.GetType()})");
                    }
                }
            }
        }

    }
}
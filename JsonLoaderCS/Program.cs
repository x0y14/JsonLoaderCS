using System;
using System.Collections.Generic;
using System.ComponentModel;
using StringNumConverter;
using JsonLoaderCS;

namespace JsonLoaderCS
{
    static class Program
    {
        public static void Main(string[] args)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(@"/Users/x0y14/Projects/JsonLoaderCS/JsonLoaderCS/testlite.json");
            string s = sr.ReadToEnd();
            sr.Close();
            var dict = new JsonLoaderCS(s);
            // var dict = new JsonLoaderCS("");
            dict.Load();
            dict.CheckData(0, dict.Loaded);
        }
    }
}

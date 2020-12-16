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
            var dict = new JsonLoaderCS("{ \"dict\": [   {\"key\": true}, 100, \"50\", -88, 0.5, -1.04, [0,0,0,0], \"hello\", {12:34}, null ]}");
            dict.Load();
            dict.CheckData(0, dict.Loaded);
        }
    }
}

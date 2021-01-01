using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using JsonParser;

namespace JsonLoaderCS
{
    public class JsonLoaderCS
    {
        private readonly string _jsonData;
        public readonly double Version = 0.9;
        public Dictionary<string, dynamic> Loaded;

        public JsonLoaderCS(string json)
        {
            _jsonData = json;
        }

        public Dictionary<string, dynamic> Load()
        {
            try
            {
                Loaded = new JsonParser.JsonParser(_jsonData).Parse();
            }
            catch (Errors.NotFoundException e)
            {
                Console.WriteLine(e);
            }


            return Loaded;
        }

        public void CheckData(Dictionary<string, dynamic> jsonObj, int n = 0)
        {
            var nest = n;
            foreach (var j in jsonObj)
            {
                Console.WriteLine($"{new String(' ', nest)}[Dict(KEY): {j.Key}]");
                if (j.Value is Dictionary<string, dynamic>)
                {
                    CheckData(j.Value, nest+=2);
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
            nest += 2;
            foreach (var i in items)
            {
                if (i is List<dynamic>)
                {
                    CheckList(nest, i);
                }
                else if (i is Dictionary<string, dynamic>)
                {
                    CheckData(i, nest);
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

            nest -= 2;
        }
        
        public dynamic Get(string path)
        {
            // {"args": { "title": "the world", "items": ["a", "b", { "obj": "str" }], "0": "-1" } }
                // "args/title" => "the world"
                // "args/items.1" => "b"
                // "args/items.2/obj" => "str"
                // "args/0" => "-1"
                if (Loaded == null)
                {
                    throw new NullReferenceException("Json has not loaded.");
                }


                if (path.Substring(path.Length - 1, 1) == "/")
                {
                    path = path.Substring(0, path.Length - 1);
                }

                if (path == "/" || path == "")
                {
                    throw new Exception("give me some paths");
                }

                string[] pathSplit = path.Split("/");

                var mapPos = Loaded;

                var subPaths = new List<int>();
                var subPathsNest = 0;
                var original_path = "";
                dynamic result = 0; 
                
                // path(non including sub-path), now sub-nest, subs
                // ex: ( "args", 0, [ 0, 1, 1, 0, 1, 2 ]

                foreach (var p in pathSplit)
                {
                    // Console.WriteLine(p);
                    if (p.Contains("."))
                    {
                        foreach (string sp in p.Split("."))
                        {
                            if (subPathsNest != 0)
                            {
                                if (int.TryParse(sp, out var i))
                                {
                                    subPaths.Add(i);
                                }
                                else
                                {
                                    throw new Exception("List Pos is Number only.");
                                }
                            }
                            else
                            {
                                original_path = sp;
                            }
                            
                            subPathsNest++;
                        }
                    }
                    
                    // Console.WriteLine($"[MapPos]: {mapPos[p]}");

                    if (subPaths.Any())
                    {
                        var reff = mapPos[original_path];
                        foreach (var sp in subPaths)
                        {
                            // reff.Count
                            if (sp < 0)
                            {
                                var sp_ = reff.Count + sp;
                                reff = reff[sp_];
                            }
                            else
                            {
                                reff = reff[sp];
                            }
                        }

                        // mapPos = reff;
                        if (reff is not Dictionary<string, dynamic>)
                        {
                            // Console.WriteLine($"reff : {reff}");
                            return reff;
                            result = reff;
                        }
                        // else
                        // {
                        mapPos = reff;
                        // }
                    }
                    
                    else

                    {
                        if (mapPos[p] is not Dictionary<string, dynamic>)
                        {
                            return mapPos[p];
                        }

                        mapPos = mapPos[p];
                    }


                    if (mapPos is not Dictionary<string, dynamic>)
                    {
                        return mapPos;
                    }

                    // 初期化
                    subPaths = new List<int>();
                    subPathsNest = 0;
                    original_path = "";
                }
                
                

                return mapPos;
        }

    }
}
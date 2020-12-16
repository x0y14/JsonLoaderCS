using System;
using System.Collections.Generic;
using StringNumConverter;
using static JsonLoaderCS.Errors;

namespace JsonParser
{
    public class JsonParser
    {
        private int Pos { get; set; }
        private string Original { get; set; }

        public JsonParser(string data)
        {
            Pos = 0;
            Original = data;
        }

        private bool Is_Eof()
        {
            if (Original.Length <= Pos)
            {
                return true;
            }

            return false;
        }
        
        private string GetChar() {
            return Original.Substring(Pos, 1);
        }
        
        private string Get1BeforeChar() {
            return Original.Substring(Pos-1, 1);
        }
        
        private string Get2BeforeChar() {
            // Consumeした後に実行されるので、~ ~ ~ ~ \\ \" ?
            // ?から数えて二個前がスラッシュになる。ゆえにPos-2
            return Original.Substring(Pos-2, 1);
        }

        private string ConsumeChar() {
            var c = GetChar();
            Pos++;
            return c;
        }

        // private string GetNextChar()
        // {
        //     return Original.Substring(Pos+1, 1);
        // }

        private void ConsumeWhiteSpace()
        {
            while (Is_Eof() == false)
            {
                if (String.IsNullOrWhiteSpace(GetChar()))
                {
                    ConsumeChar();
                }
                else
                {
                    return;
                }
            }
        }
        
        private string Goto(string goal)
        // 真上に行く。
        // 超えたほうがいいのかな。
        {
            if (goal.Length != 1) { throw new InvalidParamaterExeception("Goal's Length must be 1"); }
            var result = "";
            while (Is_Eof() == false)
            {
                var c = ConsumeChar();
                // result += c;
                
                if (c == goal)
                {
                    if (Get2BeforeChar() != "\\")
                    {
                        return result;
                    }
                    // Console.WriteLine($@"found slash item: \{c}");
                    // 汚いから描き直したいね。
                }

                result += c;
            }

            throw new NotFoundException("Goal was not found on Original.");
        }

        private string ConsumeWhile(List<string> targets)
        {
            var result = "";
            while (Is_Eof() == false)
            {
                // Console.WriteLine($@"[ConsumeWhile] Scan: {GetChar()}");
                if (targets.Contains(GetChar()))
                {
                    return result;
                }

                result += ConsumeChar();
            }

            throw new NotFoundException("targets was not found.");
        }
        

        // private (string, string) AnalyzeValue()
        private dynamic AnalyzeValue()

        {
            var EndValue = new List<string> {",", " ", "}", "]"};
            ConsumeWhiteSpace();
            
            // int
            if ("1234567890-".Contains(GetChar()))
            {
                var data = ConsumeWhile(EndValue);
                return new StringNumConverter.Converter(data).Calc();
            }
            
            // string
            else if (GetChar() == "\"")
            {
                ConsumeChar();
                var data = Goto("\"");
                return data.ToString();
            }
            
            // bool
            else if ("tfn".Contains(GetChar()))
            {
                var data = ConsumeWhile(EndValue);
                // Console.WriteLine($@": Found Value: (Bool) {data}");
                switch (data)
                {
                    case "true": return true;
                    case "false": return false;
                    case "null": return null;
                }

                throw new InvalidOperationException("json-keyword only.");
            }
            
            else if (GetChar() == "{")
            {
                var data = Parse();
                return data;
            }
            
            else if (GetChar() == "[")
            {
                var vals = new List<dynamic>();
                ConsumeChar();
                while (Is_Eof() == false)
                {
                    // Console.WriteLine(GetChar());
                    ConsumeWhiteSpace();
                    var v = AnalyzeValue();
                    vals.Add(v);
                    ConsumeWhiteSpace();
                    
                    // Console.WriteLine(GetChar());
                    
                    if (",}".Contains(GetChar()))
                    {
                        ConsumeChar();
                        continue;
                    }
                    
                    if (GetChar() == "]")
                    {
                        ConsumeChar();
                        return vals;
                    }
                    else
                    {
                        throw new NotFoundException($"Unpack Object: {GetChar()}");
                    }

                }
            }
            // return "Unknown";
            throw new NotFoundException($@"Unpack Object: {GetChar()}");
        }
        

        public Dictionary<string, dynamic> Parse()
        {
            var result = new Dictionary<string, dynamic>();
            
            while (Is_Eof() == false)
            {
                if (GetChar() == "{") { ConsumeChar(); }
                //Console.WriteLine("Found {");
                
                ConsumeWhiteSpace();

                if (GetChar() == "}")
                {
                    //Console.WriteLine("Found } (e1)");
                    return result;
                }

                var key = "";
                
                if (GetChar() == "\"")
                {
                    ConsumeChar();
                    key = Goto("\"");
                    //Console.WriteLine(key);
                    ConsumeWhiteSpace();
                }
                
                else if ("1234567890".Contains(GetChar()))
                {
                    var EndValue = new List<string> {" ", ":"};
                    // { 123 : "value" }
                    key = ConsumeWhile(EndValue);
                }

                if (GetChar() == ":") { ConsumeChar(); }
                var value = AnalyzeValue();
                if (key == "") { throw new InvalidParamaterExeception("Key is Empty."); }
                result[key] = value;
                // ConsumeWhiteSpace();
                // Console.WriteLine($@"[{key}] {Original.Length} in {Pos}");
                if (GetChar() == ",")
                {
                    // one more this loop.
                    ConsumeChar();
                    continue;
                }
                if (GetChar() == "}")
                {
                    //Console.WriteLine("Found } (e2)");
                    ConsumeChar();
                    return result;
                }
                
                // if (Get1BeforeChar() == "}")
                // {
                //     return result;
                // }
                //Console.WriteLine("[warn] maybe found ',' , one more looping.");
            }
            //Console.WriteLine("[pls check code.]");
            return result;
        }
        
    }
}
using System;
using System.Collections.Generic;
using JsonLoaderCS;
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
        
        private (string, string, string) GetNears()
        {
            var str1 = "";
            try { str1 = Original.Substring(Pos-5, 4); }
            catch (Exception e) { }

            var str2 = "";
            str2 = GetChar();

            var str3 = "";
            try { str3 = Original.Substring(Pos, 5); }
            catch (Exception e) { }

            return (str1, str2, str3);
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
        
        private void ConsumeWhiteSpace()
        {
            while (Is_Eof() == false)
            {
                if (String.IsNullOrWhiteSpace(GetChar()))
                {
                    ConsumeChar();
                }
                else if (GetChar() == "\n")
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
            if (goal.Length != 1) { throw new InvalidParamaterException("Goal's Length must be 1"); }
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
        
        private dynamic AnalyzeValue()

        {
            var EndValue = new List<string> {",", " ", "}", "]", "\n", "\t"};
            ConsumeWhiteSpace();
            
            // int
            if ("1234567890-".Contains(GetChar()))
            {
                ConsumeWhiteSpace();
                var data = ConsumeWhile(EndValue);
                try
                {
                    return new StringNumConverter.Converter(data).Calc();
                }
                catch (InvalidParamaterException e)
                {
                    var e3 = Errors.ErrorMessageMaker(e.Message,
                        "JsonParser", "AnalyzeValue", GetNears(),
                        Original.Length, Pos);
                    throw new InvalidOperationException(e3);
                }
            }
            
            // string
            else if (GetChar() == "\"")
            {
                ConsumeChar();
                var data = Goto("\"");
                ConsumeWhiteSpace();
                return data.ToString();
            }
            
            // bool
            else if ("tfn".Contains(GetChar()))
            {
                var data = ConsumeWhile(EndValue);
                switch (data)
                {
                    case "true": return true;
                    case "false": return false;
                    case "null": return null;
                }
                
                var e = Errors.ErrorMessageMaker("json-keyword only.",
                    "JsonParser", "AnalyzeValue",GetNears(),
                    Original.Length, Pos);

                throw new InvalidOperationException(e);
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
                    ConsumeWhiteSpace();
                    var v = AnalyzeValue();
                    vals.Add(v);
                    ConsumeWhiteSpace();
                    // 安定しこう。
                    // if (",}".Contains(GetChar()))
                    // {
                    //     Console.WriteLine(GetNears());
                    //     ConsumeChar();
                    //     continue;
                    // }
                    // challenge
                    if (",".Contains(GetChar()))
                    {
                        ConsumeChar();
                        continue;
                    }
                    if ("}".Contains(GetChar()))
                    {
                        ConsumeChar();
                        if (",".Contains(GetChar()))
                        {
                            ConsumeChar();
                            return new Dictionary<string, dynamic>();
                        }
                        continue;
                    }

                    if (GetChar() == "]")
                    {
                        ConsumeChar();
                        return vals;
                    }
                    else
                    {
                        var e = Errors.ErrorMessageMaker("Unpack Object",
                            "JsonParser", "AnalyzeValue",GetNears(),
                            Original.Length, Pos);
                        throw new NotFoundException(e);
                    }

                }
            }

            if (GetChar() == "]")
            {
                Console.WriteLine("[warn] empty list");
                return new List<dynamic>();
            }

            var e2 = Errors.ErrorMessageMaker("Unpack Object",
                "JsonParser", "AnalyzeValue",GetNears(),
                Original.Length, Pos);
            throw new NotFoundException(e2);
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
                    return result;
                }

                var key = "";
                
                if (GetChar() == "\"")
                {
                    ConsumeChar();
                    key = Goto("\"");
                    ConsumeWhiteSpace();
                }
                
                else if ("1234567890".Contains(GetChar()))
                {
                    var EndValue = new List<string> {" ", ":"};
                    key = ConsumeWhile(EndValue);
                }

                if (GetChar() == ":") { ConsumeChar(); }
                var value = AnalyzeValue();

                // Console.WriteLine(value);
                if (key == "")
                {
                    var e = Errors.ErrorMessageMaker("Key is Empty.",
                        "JsonParser", "Parse",GetNears(),
                        Original.Length, Pos);
                    throw new InvalidParamaterException(e);
                }
                result[key] = value;
                if (GetChar() == ",")
                {
                    // one more this loop.
                    ConsumeChar();
                    continue;
                }
                if (GetChar() == "}")
                {
                    ConsumeChar();
                    return result;
                }
            }
            return result;
        }
        
    }
}
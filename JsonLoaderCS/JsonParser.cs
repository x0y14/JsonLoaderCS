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
        
        private string GetBeforeChar() {
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
                    if (GetBeforeChar() != "\\")
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
                if (targets.Contains(GetChar()))
                {
                    return result;
                }

                result += ConsumeChar();
            }

            throw new NotFoundException("targets was not found.");
        }

        private (string, string) AnalyzeValue()
        {
            List<string> EndValue = new List<string>();
            EndValue.Add(",");
            EndValue.Add(" ");
            EndValue.Add("}");
            EndValue.Add("]");

            ConsumeWhiteSpace();
            
            // int
            if ("1234567890-".Contains(GetChar()))
            {
                var data = ConsumeWhile(EndValue);
                return ("int", data);
            }
            
            // string
            else if (GetChar() == "\"")
            {
                ConsumeChar();
                var data = Goto("\"");
                ConsumeChar();
                return ("string", data);
            }
            
            // bool
            else if ("tf".Contains(GetChar()))
            {
                var data = ConsumeWhile(EndValue);
                switch (data)
                {
                    case "true": return ("bool", "true");
                    case "false": return ("bool", "false");
                }
            }

            return ("s", "s");
        }

        public string Parse()
        {
            // System.Console.WriteLine($@"Get({Pos}): {GetChar()}");
            while (Is_Eof() == false)
            {
                if (GetChar() == "{") { ConsumeChar(); }
                
                ConsumeWhiteSpace();

                if (GetChar() == "\"")
                {
                    ConsumeChar();
                    var key = Goto("\"");
                    // ConsumeChar();
                    // Console.WriteLine(key);
                    // Console.WriteLine(GetChar());
                    // return "";
                    ConsumeWhiteSpace();
                }

                if (GetChar() == ":") { ConsumeChar(); }
                
                // Console.WriteLine($@"Just value analyze: {GetChar()}");
                var value = AnalyzeValue();
                // Console.WriteLine($@"Value: {value}");
                
                return "";
            }
            
            return "";
        }
        
    }
}
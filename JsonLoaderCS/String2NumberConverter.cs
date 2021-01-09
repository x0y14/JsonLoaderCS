using System;

namespace JsonLoader
{
    public class String2NumberConverter
    {
        private string Original { get; }
        private int Pos { get; set; }
        private static string Target { get; set; }
        private bool Minus { get; }
        private bool Float { get; }
        private int DecimalPoint { get; }

        public String2NumberConverter(string data)
        {
            Original = data;
            Pos = 0;
            Target = data;
            
            // マイナスの有無を確認します。
            //見つけた場合、削除します。
            if (Target.Contains("-"))
            {
                // マイナスを含む場合、それは文字列の先頭になければなりません。
                var dp = Target.IndexOf("-", StringComparison.Ordinal);
                
                if (dp != 0) { throw new Exception($"[{Original}] => Minus must be on the head of string."); }
                if (CountOf(Target, "-") != 1) { throw new Exception($"[{Original}] => Minus must not exist more than one."); }

                Target = Target.Replace("-", "");
                Minus = true;
            }
            else
            {
                Minus = false;
            }
            
            // 小数点を見つけたら、削除します。
            // Decimal_Pointは、小数点がどこに存在するかを示します。
            // ex) 8.1 -> 1, 10 -> 2
            if (Target.Contains("."))
            {
                var dp = Target.IndexOf(".", StringComparison.Ordinal);
                
                if (dp == 0)
                {
                    throw new Exception($"[{Original}] => Decimal_Point must not be on the head of string.");
                }

                if (CountOf(Target, ".") != 1)
                {
                    throw new Exception($"[{Original}] => Decimal_Point must not exist more than one.");
                }
                
                Target = Target.Replace(".", "");
                DecimalPoint = dp;
                Float = true;
            }
            else
            {
                DecimalPoint = Target.Length;
                Float = false;
            }
        }

        private static int CountOf(string s, string c)
        {
            return s.Length - s.Replace(c, "").Length;
        }

        
        // 確認用
        private void ShowState()
        {
            System.Console.WriteLine($@"Inputed: {Original}");
            System.Console.WriteLine($@"Edited: {Target}");
            System.Console.WriteLine($@"Pos: {Pos}");
            System.Console.WriteLine($@"Minus: {Minus}");
            System.Console.WriteLine($@"Float: {Float}");
            System.Console.WriteLine($@"Decimal_Point: {DecimalPoint}");
            System.Console.WriteLine("------");
        }

        private bool Is_Eof()
        {
            if (Target.Length <= Pos)
            {
                return true;
            }

            return false;
        }
        
        private string GetChar() {
            return Target.Substring(Pos, 1);
        }
        
        private string ConsumeChar() {
            var c = GetChar();
            Pos++;
            return c;
        }

        private int Exchange(string n)
        {
            if ("1234567890".Contains(n))
            {
                switch (n)
                {
                    case "1": return 1;
                    case "2": return 2;
                    case "3": return 3;
                    case "4": return 4;
                    case "5": return 5;
                    case "6": return 6;
                    case "7": return 7;
                    case "8": return 8;
                    case "9": return 9;
                    case "0": return 0;
                }
            }
            throw new Exception($"{n} >> Exchange Param:n must be in 1234567890");
        }
        
        public decimal Calc()
        {
            decimal result = 0;
            while (Is_Eof() == false)
            {
                var c = ConsumeChar();
                var numNum = Exchange(c);
                if (numNum != 0)
                { 
                    var ruijo = System.Math.Pow(10, DecimalPoint - Pos);
                    var r = System.Convert.ToDecimal(numNum) * System.Convert.ToDecimal(ruijo);
                    result += r;
                }
            }

            if (Minus)
            {
                result *= -1;
            }
            return result;
        }
    }
}
using System;

namespace StringNumConverter
{
    public class InvalidSyntaxException : Exception
    {
        public InvalidSyntaxException(String message) : base(message)
        { }

        public InvalidSyntaxException(String message, Exception inner) : base(message, inner) { }
    }


    public class Converter
    {
        private int Pos { get; set; }
        private static string Target { get; set; }
        private bool Minus { get; set; }
        private bool Float { get; set; }
        private int Dicimal_Point { get; set; }

        public Converter(string data)
        {
            Pos = 0;
            Target = data;
            // マイナスが含まれていたら削除する。
            if (Target.Contains("-"))
            {
                var dp = Target.IndexOf("-");
                if (dp != 0) { throw new InvalidSyntaxException("Minus must be on the head of string."); }

                Target = Target.Replace("-", "");
                Minus = true;
            }
            else
            {
                Minus = false;
            }
            // 小数点あるのかあったら消す、Dicimal_Pointに保存。
            // これは小数点が置かれる位置が示される。
            // 8.1 -> 1
            // 10 -> 2
            if (Target.Contains("."))
            {
                var dp = Target.IndexOf(".");
                if (dp == 0) { throw new InvalidSyntaxException("Decimal_Point must not be on the head of string."); }

                Dicimal_Point = dp;
                Float = true;
            }
            else
            {
                Dicimal_Point = Target.Length;
                Float = false;
            }

        }

        public void Main()
        {
            ShowChars();
        }

        private void ShowChars()
        {
            System.Console.WriteLine($@"Inputed: {Target}");
            System.Console.WriteLine($@"Pos: {Pos}");
            System.Console.WriteLine($@"Minus: {Minus}");
            System.Console.WriteLine($@"Float: {Float}");
            System.Console.WriteLine($@"Dicimal_Point: {Dicimal_Point}");
            System.Console.WriteLine("------");

        }
    }
}
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using String2NumberConverter;
using static JsonLoaderCS.Errors;

namespace JsonLoaderCSTest
{
    [TestClass]
    public class StringNumConverterTest
    {
        [TestMethod]
        public void ConvertTest()
        {
            var normal = new Converter("102");

            var minus_man = new Converter("-201");

            var float_man = new Converter("89.9091");

            var minus_float = new Converter("-3.5");
            return;
        }
        
        [TestMethod]
        public void ConvertCorrect()
        {
            var c1 = new Converter("123").Calc();
            Assert.AreEqual(Convert.ToDecimal(123), c1);
            
            var c2 = new Converter("3.1415").Calc();
            Assert.AreEqual(Convert.ToDecimal(3.1415), c2);
            
            var c3 = new Converter("-12").Calc();
            Assert.AreEqual(Convert.ToDecimal(-12), c3);
            
            var c4 = new Converter("-1089892389.2").Calc();       
            Assert.AreEqual(Convert.ToDecimal(-1089892389.2), c4); 
        }
        
        [TestMethod]
        public void StressTest10K()
        {
            var errors = new List<dynamic>();
            var randomer = new System.Random();
            
            for (var i = 0; i < 10000; i++)
            {
                var n1 = randomer.Next(-10000000, 1000000000);
                var n2 = System.Convert.ToDecimal(randomer.NextDouble());
                var n3 = System.Convert.ToDecimal(randomer.NextDouble() * -1);

                var str1 = new Converter($"{n1}").Calc();
                var str2 = new Converter($"{n2}").Calc();
                var str3 = new Converter($"{n3}").Calc();
                Assert.AreEqual(n1, str1);
                Assert.AreEqual(n2, str2);
                Assert.AreEqual(n3, str3);
            }
            Console.WriteLine($"errors:{errors.Count}");
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JsonLoader;
    

namespace JsonLoaderCSTest
{
    [TestClass]
    public class StringNumConverterTest
    {
        [TestMethod]
        public void ConvertTest()
        {
            var normal = new JsonLoader.String2NumberConverter("102");

            var minus_man = new JsonLoader.String2NumberConverter("-201");

            var float_man = new JsonLoader.String2NumberConverter("89.9091");

            var minus_float = new JsonLoader.String2NumberConverter("-3.5");
            return;
        }
        
        [TestMethod]
        public void ConvertCorrect()
        {
            var c1 = new JsonLoader.String2NumberConverter("123").Calc();
            Assert.AreEqual(Convert.ToDecimal(123), c1);
            
            var c2 = new JsonLoader.String2NumberConverter("3.1415").Calc();
            Assert.AreEqual(Convert.ToDecimal(3.1415), c2);
            
            var c3 = new JsonLoader.String2NumberConverter("-12").Calc();
            Assert.AreEqual(Convert.ToDecimal(-12), c3);
            
            var c4 = new JsonLoader.String2NumberConverter("-1089892389.2").Calc();       
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

                var str1 = new JsonLoader.String2NumberConverter($"{n1}").Calc();
                var str2 = new JsonLoader.String2NumberConverter($"{n2}").Calc();
                var str3 = new JsonLoader.String2NumberConverter($"{n3}").Calc();
                Assert.AreEqual(n1, str1);
                Assert.AreEqual(n2, str2);
                Assert.AreEqual(n3, str3);
            }
            Console.WriteLine($"errors:{errors.Count}");
        }
    }
}

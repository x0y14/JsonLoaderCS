using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringNumConverter;

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

            var float_man = new Converter("8.1");

            var minus_float = new Converter("-3.5");
        }

        [TestMethod]
        public void ConvertErrorMinus() {
            try
            {
                var err_minus = new Converter("599-8");
            }
            catch (InvalidSyntaxException)
            {
                System.Console.WriteLine("InvalidSyntaxException( minus )が正常に発生しました。");
                return;
            }
            catch (Exception e)
            {
                Assert.Fail(
                    string.Format($@"Unexpected exception of type {e.GetType()} caught: {e.Message}")
                    );
            }
            Assert.Fail("InvalidSyntaxException( minus )が発生しませんでした。");
        }

        [TestMethod]
        public void ConvertErrorFloat() {
            try
            {
                var err_float = new Converter(".5998");
            }
            catch (InvalidSyntaxException)
            {
                System.Console.WriteLine("InvalidSyntaxException( float )が正常に発生しました。");
                return;
            }
            catch (Exception e)
            {
                Assert.Fail(
                    string.Format($@"Unexpected exception of type {e.GetType()} caught: {e.Message}")
                    );
            }
            Assert.Fail("InvalidSyntaxException( float )が発生しませんでした。");
        }
    }
}

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

            var float_man = new Converter("89.9091");

            var minus_float = new Converter("-3.5");
            return;
        }

        [TestMethod]
        public void ConvertMinus_Invalid_Pos() {
            try
            {
                var err_pos_minus = new Converter("599-8");
            }
            catch (InvalidSyntaxException)
            {
                System.Console.WriteLine("InvalidSyntaxException( ConvertMinus_Invalid_Pos )が正常に発生しました。");
                return;
            }
            catch (Exception e)
            {
                Assert.Fail(
                    string.Format($@"Unexpected exception of type {e.GetType()} caught: {e.Message}")
                    );
            }
            Assert.Fail("InvalidSyntaxException( ConvertMinus_Invalid_Pos )が発生しませんでした。");
        }

        [TestMethod]
        public void ConvertFloat_Invalid_Pos() {
            try
            {
                var err_pos_float = new Converter(".5998");
            }
            catch (InvalidSyntaxException)
            {
                System.Console.WriteLine("InvalidSyntaxException( ConvertFloat_Invalid_Pos )が正常に発生しました。");
                return;
            }
            catch (Exception e)
            {
                Assert.Fail(
                    string.Format($@"Unexpected exception of type {e.GetType()} caught: {e.Message}")
                    );
            }
            Assert.Fail("InvalidSyntaxException( ConvertFloat_Invalid_Pos )が発生しませんでした。");
        }

        [TestMethod]
        public void ConvertMinus_Many_Exist() {
            try
            {
                var err_exist_minus = new Converter("-59-98");
            }
            catch (InvalidSyntaxException)
            {
                System.Console.WriteLine("InvalidSyntaxException( ConvertMinus_Many_Exist )が正常に発生しました。");
                return;
            }
            catch (Exception e)
            {
                Assert.Fail(
                    string.Format($@"Unexpected exception of type {e.GetType()} caught: {e.Message}")
                    );
            }
            Assert.Fail("InvalidSyntaxException( ConvertMinus_Many_Exist )が発生しませんでした。");
        }

        [TestMethod]
        public void ConvertFloat_Many_Exist()
        {
            try
            {
                var err_exist_float = new Converter("59..98");
            }
            catch (InvalidSyntaxException)
            {
                System.Console.WriteLine("InvalidSyntaxException( ConvertFloat_Many_Exist )が正常に発生しました。");
                return;
            }
            catch (Exception e)
            {
                Assert.Fail(
                    string.Format($@"Unexpected exception of type {e.GetType()} caught: {e.Message}")
                    );
            }
            Assert.Fail("InvalidSyntaxException( ConvertFloat_Many_Exist )が発生しませんでした。");
        }
    }
}

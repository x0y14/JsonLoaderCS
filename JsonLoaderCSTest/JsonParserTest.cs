using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JsonParser;
using static JsonLoaderCS.Errors;


namespace JsonLoaderCSTest
{
    [TestClass]
    public class JsonParserTest
    {
        [TestMethod]
        public void ParserTest()
        {
            var keyint = new JsonParser.JsonParser("{\"key\":12344}").Parse();
            // Assert.AreEqual(("int", "12344"), keyint);
            // Console.WriteLine(keyint);
            
            var keystring = new JsonParser.JsonParser("{\"key\": \"value\"}").Parse();
            // Assert.AreEqual(("string", "value"), keyint);

            var keystringhasdoublequote = new JsonParser.JsonParser("{\"key\": \"va\\\"lue\"}").Parse();
            // Assert.AreEqual(("string", "va\"lue"), keyint);

            var keybool = new JsonParser.JsonParser("{\"key\": true}").Parse();
            // Assert.AreEqual(("bool", "true"), keyint);

        }
    }
}
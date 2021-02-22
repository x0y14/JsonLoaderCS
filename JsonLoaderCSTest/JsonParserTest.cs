using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JsonLoader;
using static JsonLoader.Errors;


namespace JsonLoaderCSTest
{
    [TestClass]
    public class JsonParserTest
    {
        [TestMethod]
        public void ParserTest()
        {
            var keyint = new JsonLoader.Loader().LoadStringAsJson("{\"key\":12344}");
            // Assert.AreEqual(("int", "12344"), keyint);
            // Console.WriteLine(keyint);
            
            var keystring = new JsonLoader.Loader().LoadStringAsJson("{\"key\": \"value\"}");
            // Assert.AreEqual(("string", "value"), keyint);

            var keystringhasdoublequote = new JsonLoader.Loader().LoadStringAsJson("{\"key\": \"va\\\"lue\"}");
            // Assert.AreEqual(("string", "va\"lue"), keyint);

            var keybool = new JsonLoader.Loader().LoadStringAsJson("{\"key\": true}");
            // Assert.AreEqual(("bool", "true"), keyint);

        }
    }
}
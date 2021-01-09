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
            var keyint = new JsonLoader.Loader("{\"key\":12344}").Load();
            // Assert.AreEqual(("int", "12344"), keyint);
            // Console.WriteLine(keyint);
            
            var keystring = new JsonLoader.Loader("{\"key\": \"value\"}").Load();
            // Assert.AreEqual(("string", "value"), keyint);

            var keystringhasdoublequote = new JsonLoader.Loader("{\"key\": \"va\\\"lue\"}").Load();
            // Assert.AreEqual(("string", "va\"lue"), keyint);

            var keybool = new JsonLoader.Loader("{\"key\": true}").Load();
            // Assert.AreEqual(("bool", "true"), keyint);

        }
    }
}
using System.ComponentModel;
using DinWebsite.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DinWebsite.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Properties prop = new Properties("C:/din_properties/properties");
            prop.Set("giphy", "https://api.giphy.com/v1/gifs/random?api_key=ifwqjnAoK7j6nZvb1k7QO48qS651g0cL&tag=trending humor&rating=G");
            prop.Save();
        }
    }
}

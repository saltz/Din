using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;
using Models.AD;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            if (AccountManagment.ChangePassword("test", "qwerty12345"))
            {
                Tuple<bool, AdObject> result = LoginSystem.Login("test", "qwerty12345");

                
            }
        }
    }
}

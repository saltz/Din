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
            if (LoginSystem.Login("chloe", "chloe123").Item1)
            {
                if (AccountManagment.ChangePassword("chloe", "password123", "password123"))
                {
                    if (LoginSystem.Login("chloe", "password123").Item1)
                    {
                        if (LoginSystem.Login("chloe", "chloe123").Item1)
                        {
                            
                        }
                    }
                }
            }

        }
    }
}

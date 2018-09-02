using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Din.Tests
{
    
    public class HashTest
    {
        [Fact]
        public void HashTestSimpleString()
        {
            const string plainText = "test";
            var hashed = BCrypt.Net.BCrypt.HashPassword(plainText);
            Assert.NotEqual(plainText, hashed);
            Assert.True(BCrypt.Net.BCrypt.Verify(plainText, hashed));
        }
    }
}

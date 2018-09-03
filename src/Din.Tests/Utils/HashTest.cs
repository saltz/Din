using System;
using System.Collections.Generic;
using Xunit;

namespace Din.Tests.Utils
{
    
    public class HashTest
    {
        [Fact]
        public void HashTestSimpleString()
        {
            const string plainText = "test";

            var hashed = BCrypt.Net.BCrypt.HashPassword(plainText);

            Assert.NotNull(hashed);
            Assert.NotEmpty(hashed);
            Assert.True(BCrypt.Net.BCrypt.Verify(plainText, hashed));
        }
    }
}

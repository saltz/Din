using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NETCore.Encrypt;

namespace Din.Config
{
    public static class CustomConfigProviderExtensions
    {
        public static IConfigurationBuilder AddEncryptedProvider(this IConfigurationBuilder builder)
        {
            return builder.Add(new CustomConfigProvider());
        }
    }

    public class CustomConfigProvider : ConfigurationProvider, IConfigurationSource
    {
        public override void Load()
        {
            Data = UnencryptMyConfiguration();
        }

        private IDictionary<string, string> UnencryptMyConfiguration()
        {
            IDictionary<string, string> unEncryptedCollection = new Dictionary<string, string>();
            JObject rawJObject;

            using (var sr =  new StreamReader("appsettings-encrypted.json"))
            {
                rawJObject = JsonConvert.DeserializeObject<JObject>(EncryptProvider.AESDecrypt(sr.ReadToEnd(),
                    Environment.GetEnvironmentVariable("AES_KEY"), Environment.GetEnvironmentVariable("AES_IV")));
            }

            foreach (var property in rawJObject.Properties())
            {
                foreach (var childProperty in property.Value)
                {
                    unEncryptedCollection.Add(property.Name + ":" + (childProperty as JProperty)?.Name, (childProperty as JProperty)?.Value.ToString());
                }
            }
               
            return unEncryptedCollection;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new CustomConfigProvider();
        }
    }
}

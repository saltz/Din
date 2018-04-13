using MediaBrowser.Common.Extensions;
using UAParser;

namespace Din.Logic.BrowserDetection
{
    public static class BrowserDetector
    {
        public static string Detect(string input)
        {
            var uaParser = Parser.GetDefault();
            ClientInfo client = uaParser.Parse(input);
            if (client.UserAgent.Family.Contains("irefox"))
            {
                return "firefox";
            }

            if (client.UserAgent.Family.Contains("hrome"))
            {
                return "chrome";
            }

            if (client.UserAgent.Family.Contains("afari"))
            {
                return "safari";
            }

            if (client.UserAgent.Family.Contains("nternet"))
            {
                return "internet-explore";
            }      
            return client.UserAgent.Family.Contains("dge") ? "edge" : "globe";
        }
    }
}


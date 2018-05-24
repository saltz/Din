using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Din.ExternalModels.Utils
{
    public class HttpRequestHelper
    {
        private readonly HttpWebRequest _request;
        private HttpWebResponse _response;
        private string _result;

        public HttpRequestHelper(string url, bool cookieContainer)
        {
            _request = (HttpWebRequest)WebRequest.Create(url);
            if (!cookieContainer) return;
            var cookies = new CookieContainer();
            _request.CookieContainer = cookies;
        }

        public HttpRequestHelper(string url, string token)
        {
            _request = (HttpWebRequest) WebRequest.Create(url);
            _request.Headers.Add("Authorization", "Bearer " + token);
        }

        public async Task<string> PerformGetRequestAsync()
        {
            _request.Method = "GET";
            _response = (HttpWebResponse) await _request.GetResponseAsync();
            using (var sr = new StreamReader(_response.GetResponseStream() ?? throw new InvalidOperationException()))
                _result = sr.ReadToEnd();
            return _result;
        }

        public async Task<Tuple<int, string>> PerformPostRequestAsync(string payload)
        {
            _request.Method = "POST";
            using (var sw = new StreamWriter(_request.GetRequestStream()))
                sw.Write(payload);
            try
            {
                _response = (HttpWebResponse) await _request.GetResponseAsync();
                using(var sr = new StreamReader(_response.GetResponseStream() ?? throw new InvalidOperationException()))
                    _result = sr.ReadToEnd();
                return new Tuple<int, string>((int) _response.StatusCode, _result);
            }
            catch (WebException)
            {
                return null;
            }
        }


        public void SetDecompressionMethods(IEnumerable<DecompressionMethods> methods)
        {
            foreach (var method in methods)
            {
                if (_request.AutomaticDecompression != DecompressionMethods.None)
                    _request.AutomaticDecompression = _request.AutomaticDecompression | method;
                else
                    _request.AutomaticDecompression = method;
            }
        }
    }
}

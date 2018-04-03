using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.IO;
using System.Net;
using System.Text;

namespace Din.Logic
{
    public class HttpRequestHelper
    {
        private readonly HttpWebRequest _request;
        private HttpWebResponse _response;
        private string _result;

        public HttpRequestHelper(string url)
        {
            _request = (HttpWebRequest) WebRequest.Create(url);   
        }

        public string PerformGetRequest()
        {
            _request.Method = "GET";
            _response = (HttpWebResponse) _request.GetResponse();
            using (var sr = new StreamReader(_response.GetResponseStream() ?? throw new InvalidOperationException()))
            {
                _result = sr.ReadToEnd();
            }
            return _result;
        }

        public int PerformPostRequest(string payload, string contentType)
        {
            _request.Method = "POST";
            using (var sw = new StreamWriter(_request.GetRequestStream()))
            {
                sw.Write(payload);
            }
            try
            {
                _response = (HttpWebResponse) _request.GetResponse();
                return (int)_response.StatusCode;
            }
            catch (WebException)
            {
                return (int) HttpStatusCode.BadRequest;
            }
        }
    }
}

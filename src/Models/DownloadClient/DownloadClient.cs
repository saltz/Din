using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Models.Exceptions;
using Newtonsoft.Json;

namespace Models.DownloadClient
{
    public class DownloadClient
    {
        private readonly string _url;
        private readonly CookieContainer _cookies;
        private HttpWebRequest _request;
        private readonly string _pwd;
        private bool _authenticated;

        public DownloadClient()
        {
            _url = File.ReadLines("C:/din_properties/api_downloadclient").First();
            _pwd = File.ReadLines("C:/din_properties/api_downloadclient").ElementAt(1);
            _cookies = new CookieContainer();
            _authenticated = false;
        }

        public bool Authenticate()
        {
            var payload = new DownloadClientRequestObject1("auth.login", new List<string> { _pwd }, 1);

            _request = (HttpWebRequest)WebRequest.Create(_url);
            _request.ContentType = "application/json";
            _request.Method = "POST";
            _request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            _request.CookieContainer = _cookies;
            using (var streamWriter = new StreamWriter(_request.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(payload);
                streamWriter.Write(json);
            }
            try
            {
                string webResult;
                var httpResponse = (HttpWebResponse)_request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    webResult = streamReader.ReadToEnd();
                }
                var jsonResponse = JsonConvert.DeserializeObject<Dictionary<string, object>>(webResult);
                if ((bool)jsonResponse["result"])
                    _authenticated = true;
                return (bool)jsonResponse["result"];
            }
            catch
            {
                throw new DownloadClientException("Error occured while authenticating");
            }
        }

        public List<DownloadClientItem> GetAllItems()
        {
            if (!_authenticated) throw new DownloadClientException("Not Authenticated");
            var payload = new DownloadClientRequestObject1("webapi.get_torrents", new List<string>(), 1);

            _request = (HttpWebRequest)WebRequest.Create(_url);
            _request.ContentType = "application/json";
            _request.Method = "POST";
            _request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            _request.CookieContainer = _cookies;

            using (var streamWriter = new StreamWriter(_request.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(payload);
                streamWriter.Write(json);
            }
            try
            {
                string webResult;
                var httpResponse = (HttpWebResponse)_request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    webResult = streamReader.ReadToEnd();
                }
                var response = JsonConvert.DeserializeObject<DownloadClientResponseObject>(webResult);
                return new List<DownloadClientItem>(response.Result.Items);
            }
            catch
            {
                throw new DownloadClientException("Failed to get all items");
            }
        }

        public int GetItemEta(string itemHash)
        {
            if (!_authenticated) throw new DownloadClientException("Not Authenticated");
            var payload = new DownloadClientRequestObject2("webapi.get_torrents", new List<List<string>>
            {
                new List<string>
                {
                    itemHash
                },
                new List<string>
                {
                    "eta"
                }
            }, 1);

            _request = (HttpWebRequest)WebRequest.Create(_url);
            _request.ContentType = "application/json";
            _request.Method = "POST";
            _request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            _request.CookieContainer = _cookies;

            using (var streamWriter = new StreamWriter(_request.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(payload);
                streamWriter.Write(json);
            }
            try
            {
                string webResult;
                var httpResponse = (HttpWebResponse)_request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    webResult = streamReader.ReadToEnd();
                }
                var response = JsonConvert.DeserializeObject<DownloadClientResponseObject>(webResult);
                return response.Result.Items[0].Eta;
            }
            catch
            {
                throw new DownloadClientException("Failed to get all items");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using DinWebsite.ExternalModels.Exceptions;
using Newtonsoft.Json;

namespace DinWebsite.ExternalModels.DownloadClient
{
    public class DownloadClient
    {
        private readonly CookieContainer _cookies;
        private readonly string _pwd;
        private readonly string _url;
        private bool _authenticated;
        private HttpWebRequest _request;

        public DownloadClient(string url, string pwd)
        {
            _url = url;
            _pwd = pwd;
            _cookies = new CookieContainer();
            _authenticated = false;
        }

        public bool Authenticate()
        {
            var payload = new DownloadClientRequestObject1("auth.login", new List<string> {_pwd}, 1);

            _request = (HttpWebRequest) WebRequest.Create(_url);
            _request.ContentType = "application/json";
            _request.Method = "POST";
            _request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            _request.CookieContainer = _cookies;
            using (var streamWriter = new StreamWriter(_request.GetRequestStream()))
            {
                var json = JsonConvert.SerializeObject(payload);
                streamWriter.Write(json);
            }
            try
            {
                string webResult;
                var httpResponse = (HttpWebResponse) _request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream() ?? throw new InvalidOperationException()))
                {
                    webResult = streamReader.ReadToEnd();
                }
                var jsonResponse = JsonConvert.DeserializeObject<Dictionary<string, object>>(webResult);
                if ((bool) jsonResponse["result"])
                    _authenticated = true;
                return (bool) jsonResponse["result"];
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

            _request = (HttpWebRequest) WebRequest.Create(_url);
            _request.ContentType = "application/json";
            _request.Method = "POST";
            _request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            _request.CookieContainer = _cookies;

            using (var streamWriter = new StreamWriter(_request.GetRequestStream()))
            {
                var json = JsonConvert.SerializeObject(payload);
                streamWriter.Write(json);
            }
            try
            {
                string webResult;
                var httpResponse = (HttpWebResponse) _request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream() ?? throw new InvalidOperationException()))
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

        public DownloadClientItem GetItemStatus(string itemHash)
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
                    "eta",
                    "files",
                    "file_progress"
                }
            }, 1);

            _request = (HttpWebRequest) WebRequest.Create(_url);
            _request.ContentType = "application/json";
            _request.Method = "POST";
            _request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            _request.CookieContainer = _cookies;

            using (var streamWriter = new StreamWriter(_request.GetRequestStream()))
            {
                var json = JsonConvert.SerializeObject(payload);
                streamWriter.Write(json);
            }
            try
            {
                string webResult;
                var httpResponse = (HttpWebResponse) _request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream() ?? throw new InvalidOperationException()))
                {
                    webResult = streamReader.ReadToEnd();
                }
                return JsonConvert.DeserializeObject<DownloadClientResponseObject>(webResult).Result.Items[0];
            }
            catch
            {
                throw new DownloadClientException("Failed to get item status");
            }
        }
    }
}
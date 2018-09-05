using System.Threading.Tasks;
using Din.ExternalModels.GeneratedMedia;
using Din.ExternalModels.Utils;

namespace Din.Service.Utils
{
    //TODO this should be a client
    public class MediaGeneration
    {
        private readonly string _url;

        public async Task<string> GenerateBackgroundAsync()
        {
            var httpRequest = new HttpRequestHelper(_url, false);
            return await httpRequest.PerformGetRequestAsync();
        }


        public async Task<string> GetRandomGifAsync(GiphyQuery query)
        {
            HttpRequestHelper httpRequest;
            switch (query)
            {
                case GiphyQuery.PageNotFound:
                    httpRequest = new HttpRequestHelper(_url, false);
                    break;
                case GiphyQuery.Forbidden:
                    httpRequest = new HttpRequestHelper(_url, false);
                    break;
                case GiphyQuery.Logout:
                    httpRequest = new HttpRequestHelper(_url, false);
                    break;
                case GiphyQuery.ServerError:
                    httpRequest = new HttpRequestHelper(_url, false);
                    break;
                case GiphyQuery.Random:
                    httpRequest = new HttpRequestHelper(_url, false);
                    break;
                default:
                    httpRequest = new HttpRequestHelper(_url, false);
                    break;
            }
            var response = await httpRequest.PerformGetRequestAsync();
            return response;
        }

    }
}
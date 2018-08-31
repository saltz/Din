using System.Threading.Tasks;
using Din.ExternalModels.GeneratedMedia;
using Din.ExternalModels.Utils;
using Din.Service.Concrete;

namespace Din.Service.Utils
{
    public static class MediaGeneration
    {
        public static async Task<string> GenerateBackgroundAsync()
        {
            var httpRequest = new HttpRequestHelper(MainService.PropertyFile.Get("unsplash"), false);
            return await httpRequest.PerformGetRequestAsync();
        }


        public static async Task<string> GetRandomGifAsync(GiphyQuery query)
        {
            HttpRequestHelper httpRequest;
            switch (query)
            {
                case GiphyQuery.PageNotFound:
                    httpRequest = new HttpRequestHelper(MainService.PropertyFile.Get("giphyPageNotFound"), false);
                    break;
                case GiphyQuery.Forbidden:
                    httpRequest = new HttpRequestHelper(MainService.PropertyFile.Get("giphyForbidden"), false);
                    break;
                case GiphyQuery.Logout:
                    httpRequest = new HttpRequestHelper(MainService.PropertyFile.Get("giphyLogout"), false);
                    break;
                case GiphyQuery.ServerError:
                    httpRequest = new HttpRequestHelper(MainService.PropertyFile.Get("giphyServerError"), false);
                    break;
                case GiphyQuery.Random:
                    httpRequest = new HttpRequestHelper(MainService.PropertyFile.Get("giphyRandom"), false);
                    break;
                default:
                    httpRequest = new HttpRequestHelper(MainService.PropertyFile.Get("giphyRandom"), false);
                    break;
            }
            var response = await httpRequest.PerformGetRequestAsync();
            return response;
        }

    }
}
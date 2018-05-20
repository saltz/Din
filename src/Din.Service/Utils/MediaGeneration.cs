using System.Threading.Tasks;
using Din.ExternalModels.GeneratedMedia;
using Din.ExternalModels.Utils;
using Din.Service.Classes;

namespace Din.Service.Utils
{
    public static class MediaGeneration
    {
        public static async Task<string> GenerateBackgroundAsync()
        {
            var httpRequest = new HttpRequestHelper(MainService.PropertyFile.get("unsplash"), false);
            return await httpRequest.PerformGetRequestAsync();
        }


        public static async Task<string> GetRandomGifAsync(GiphyQuery query)
        {
            HttpRequestHelper httpRequest;
            switch (query)
            {
                case GiphyQuery.PageNotFound:
                    httpRequest = new HttpRequestHelper(MainService.PropertyFile.get("giphyPageNotFound"), false);
                    break;
                case GiphyQuery.Forbidden:
                    httpRequest = new HttpRequestHelper(MainService.PropertyFile.get("giphyForbidden"), false);
                    break;
                case GiphyQuery.Logout:
                    httpRequest = new HttpRequestHelper(MainService.PropertyFile.get("giphyLogout"), false);
                    break;
                case GiphyQuery.ServerError:
                    httpRequest = new HttpRequestHelper(MainService.PropertyFile.get("giphyServerError"), false);
                    break;
                case GiphyQuery.Random:
                    httpRequest = new HttpRequestHelper(MainService.PropertyFile.get("giphyRandom"), false);
                    break;
                default:
                    httpRequest = new HttpRequestHelper(MainService.PropertyFile.get("giphyRandom"), false);
                    break;
            }
            var response = await httpRequest.PerformGetRequestAsync();
            return response;
        }

    }
}
using Din.ExternalModels.GeneratedMedia;
using Din.ExternalModels.ViewModels;
using Din.Service.Interfaces;
using Din.Service.Utils;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Din.Service.Concrete
{
    public class StatusCodeService : IStatusCodeService
    {
        public async Task<StatusCodeModel> GenerateDataToDisplayAsync(int statusCode)
        {
            var model = new StatusCodeModel
            {
                StatisCode = statusCode
            };
            switch (statusCode)
            {
                case 400:
                    model.StatusMessage = "Te Fck did you do";
                    model.Gif = JsonConvert.DeserializeObject<GiphyContainer>(await MediaGeneration.GetRandomGifAsync(GiphyQuery.Random));
                    break;
                case 401:
                    model.StatusMessage = "You're not supposed to do that";
                    model.Gif = JsonConvert.DeserializeObject<GiphyContainer>(await MediaGeneration.GetRandomGifAsync(GiphyQuery.Forbidden));
                    break;
                case 403:
                    model.StatusMessage = "No No No";
                    model.Gif = JsonConvert.DeserializeObject<GiphyContainer>(await MediaGeneration.GetRandomGifAsync(GiphyQuery.Forbidden));
                    break;
                case 404:
                    model.StatusMessage = "It's gone";
                    model.Gif = JsonConvert.DeserializeObject<GiphyContainer>(await MediaGeneration.GetRandomGifAsync(GiphyQuery.PageNotFound));
                    break;
                case 408:
                    model.StatusMessage = "The server timed out waiting for the request";
                    model.Gif = JsonConvert.DeserializeObject<GiphyContainer>(await MediaGeneration.GetRandomGifAsync(GiphyQuery.ServerError));
                    break;
                case 500:
                    model.StatusMessage = "Hmmm seems like I fucked up";
                    model.Gif = JsonConvert.DeserializeObject<GiphyContainer>(await MediaGeneration.GetRandomGifAsync(GiphyQuery.ServerError));
                    break;
                default:
                    model.StatusMessage = "Hmmm seems like I fucked up";
                    model.Gif = JsonConvert.DeserializeObject<GiphyContainer>(await MediaGeneration.GetRandomGifAsync(GiphyQuery.Random));
                    break;
            }
            return model;
        }
    }
}

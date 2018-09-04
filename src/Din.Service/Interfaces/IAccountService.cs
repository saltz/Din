using System.Threading.Tasks;
using Din.ExternalModels.ViewModels;

namespace Din.Service.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAccountService
    {
        Task<AccountViewModel> GetAccountDataAsync(int id, string useragent);
        Task<ResultViewModel> UploadAccountImageAsync(int id, string name, byte[] data);
        Task GetMovieCalendarAsync();
        Task GetTvShowCalendarAsync();
    }
}

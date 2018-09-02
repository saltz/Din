using Din.ExternalModels.ViewModels;
using System.Threading.Tasks;

namespace Din.Service.Interfaces
{
    public interface IStatusCodeService
    {
        Task<StatusCodeViewModel> GenerateDataToDisplayAsync(int statusCode);
    }
}

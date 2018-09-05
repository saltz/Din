using System.Threading.Tasks;

namespace Din.Service.Services.Interfaces
{
    public interface IStatusCodeService
    {
        /// <summary>
        /// Generates ViewModel corresponding to the status code.
        /// </summary>
        /// <param name="statusCode">Http status code.</param>
        /// <returns>ViewModel</returns>
        Task<StatusCodeViewModel> GenerateDataToDisplayAsync(int statusCode);
    }
}

using System.Threading.Tasks;
using Din.Service.Dto;
using Din.Service.Dto.Account;

namespace Din.Service.Services.Interfaces
{
    /// <summary>
    /// Service for corresponding account controller.
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Gets the account data associated with the session.
        /// </summary>
        /// <param name="id">Account id, stored in the session.</param>
        /// <returns>ViewModel containing the account data.</returns>
        Task<DataDto> GetAccountDataAsync(int id);
        /// <summary>
        /// Upload the supplied image file by the user.
        /// </summary>
        /// <param name="id">Account id, stored in the session.</param>
        /// <param name="name">Supplied file name.</param>
        /// <param name="data">Byte array made from supplied user file.</param>
        /// <returns>Default result ViewModel</returns>
        Task<ResultDto> UploadAccountImageAsync(int id, string name, byte[] data);
    }
}

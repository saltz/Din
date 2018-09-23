using System.Threading.Tasks;
using Din.Service.Dto;
using Din.Service.Dto.Account;
using Din.Service.Dto.Context;

namespace Din.Service.Services.Interfaces
{
    /// <summary>
    /// Service for corresponding account controller
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Gets the account data associated with the session
        /// </summary>
        /// <param name="id">Account id, stored in the session</param>
        /// <returns>ViewModel containing the account data</returns>
        Task<DataDto> GetAccountDataAsync(int id);
        /// <summary>
        /// Upload the supplied image file by the user
        /// </summary>
        /// <param name="id">Account id, stored in the session</param>
        /// <param name="name">Supplied file name</param>
        /// <param name="data">Byte array made from supplied user file</param>
        /// <returns>Default result ViewModel</returns>
        Task<ResultDto> UploadAccountImageAsync(int id, string name, byte[] data);
        /// <summary>
        /// Update the user information submitted by user
        /// </summary>
        /// <param name="user">Model containing the information data</param>
        /// <returns>Default result ViewModel</returns>
        Task<ResultDto> UpdatePersonalInformation(int id, UserDto user);
        /// <summary>
        /// Update the account information submitted by the user
        /// </summary>
        /// <param name="id">Account id, stored in the session</param>
        /// <param name="username">username of the account</param>
        /// <param name="hash">hash of the new password</param>
        /// <returns></returns>
        Task<ResultDto> UpdateAccountInformation(int id, string username, string hash);
    }
}

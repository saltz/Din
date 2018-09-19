using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Din.Data;
using Din.Data.Entities;
using Din.Service.Dto;
using Din.Service.Dto.Account;
using Din.Service.Dto.Context;
using Din.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Din.Service.Services.Concrete
{
    /// <inheritdoc cref="IAccountService" />
    public class AccountService : BaseService, IAccountService
    {
        private readonly DinContext _context;
        private readonly IMapper _mapper;

        public AccountService(DinContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DataDto> GetAccountDataAsync(int id)
        {
            return new DataDto
            {
                User = _mapper.Map<UserDto>(await _context.User.FirstAsync(u => u.Account.ID.Equals(id))),
                Account = _mapper.Map<AccountDto>(await _context.Account.FirstAsync(a => a.ID.Equals(id))),
                AddedContent = _mapper.Map<IEnumerable<AddedContentDto>>((await _context.AddedContent.Where(ac => ac.Account.ID.Equals(id)).ToListAsync()).AsEnumerable())
            };
        }

        public async Task<ResultDto> UploadAccountImageAsync(int id, string name, byte[] data)
        {
            //TODO
            var account = await _context.Account.FirstAsync(a => a.ID.Equals(id));
            account.Image = new AccountImageEntity
            {
                Data = data,
                Name = name
            };

            await _context.SaveChangesAsync();
            return null;
        }

        public async Task<ResultDto> UpdatePersonalInformation(int id, UserDto user)
        {
            try
            {
                var userEntity = await _context.User.FirstAsync(u => u.Account.ID.Equals(id));
                _context.Attach(userEntity);

                userEntity.FirstName = user.FirstName;
                userEntity.LastName = user.LastName;

                await _context.SaveChangesAsync();

                return GenerateResultDto("Update successful", "Your user information has been updated.",
                    ResultDtoStatus.Successful);
            }
            catch
            {
                return GenerateResultDto("Update unsuccessful", "Something went wrong 😵 Try again later!",
                    ResultDtoStatus.Unsuccessful);
            }
        }

        public async Task<ResultDto> UpdateAccountInformation(int id, string username, string hash)
        {
            try
            {
                var accountEntity = await _context.Account.FirstAsync(a => a.ID.Equals(id));
                _context.Attach(accountEntity);

                accountEntity.Username = username;
                accountEntity.Hash = hash;

                await _context.SaveChangesAsync();

                return GenerateResultDto("Update successful", "Your account information has been updated.",
                    ResultDtoStatus.Successful);
            }
            catch
            {
                return GenerateResultDto("Update unsuccessful", "Something went wrong 😵 Try again later!",
                    ResultDtoStatus.Unsuccessful);
            }
        }
    }
}

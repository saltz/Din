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
    /// <inheritdoc />
    public class AccountService : IAccountService
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

            return new ResultDto
            {
                Title = "Profile picture updated",
                TitleColor = "#00d77c",
                Message = "Your profile picture is successfully uploaded"
            };
        }
    }
}

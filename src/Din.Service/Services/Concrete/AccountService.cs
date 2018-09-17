using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Din.Data;
using Din.Data.Entities;
using Din.Service.DTO;
using Din.Service.DTO.Account;
using Din.Service.DTO.Context;
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

        public async Task<DataDTO> GetAccountDataAsync(int id)
        {
            return new DataDTO
            {
                User = _mapper.Map<UserDTO>(await _context.User.FirstAsync(u => u.Account.ID.Equals(id))),
                Account = _mapper.Map<AccountDTO>(await _context.Account.FirstAsync(a => a.ID.Equals(id))),
                AddedContent = _mapper.Map<IEnumerable<AddedContentDTO>>((await _context.AddedContent.Where(ac => ac.Account.ID.Equals(id)).ToListAsync()).AsEnumerable())
            };
        }

        public async Task<ResultDTO> UploadAccountImageAsync(int id, string name, byte[] data)
        {
            //TODO
            var account = await _context.Account.FirstAsync(a => a.ID.Equals(id));
            account.Image = new AccountImageEntity
            {
                Data = data,
                Name = name
            };

            await _context.SaveChangesAsync();

            return new ResultDTO
            {
                Title = "Profile picture updated",
                TitleColor = "#00d77c",
                Message = "Your profile picture is successfully uploaded"
            };
        }
    }
}

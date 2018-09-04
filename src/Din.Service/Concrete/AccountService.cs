using System;
using System.Linq;
using System.Threading.Tasks;
using Din.Data;
using Din.ExternalModels.Entities;
using Din.ExternalModels.ViewModels;
using Din.Service.Interfaces;
using Din.Service.Systems;
using Microsoft.EntityFrameworkCore;
using UAParser;

namespace Din.Service.Concrete
{
    /// <inheritdoc />
    public class AccountService : IAccountService
    {
        private readonly DinContext _context;

        public AccountService(DinContext context)
        {
            _context = context;
        }

        public async Task<AccountViewModel> GetAccountDataAsync(int id, string useragent)
        {
            return new AccountViewModel
            {
                User = await _context.User.Include(u => u.Account.AddedContent).Include(u => u.Account.Image).FirstAsync(u => u.ID.Equals(id)),
                Client = Parser.GetDefault().Parse(useragent),
                AddedContent = await _context.AddedContent.Where(a => a.Account.User.ID.Equals(id)).ToListAsync()
            };
        }

        public async Task<ResultViewModel> UploadAccountImageAsync(int id, string name, byte[] data)
        {
            var account = await _context.Account.FirstAsync(a => a.User.ID.Equals(id));
            account.Image = new AccountImage
            {
                Data = data,
                Name = name
            };
            await _context.SaveChangesAsync();
            return new ResultViewModel
            {
                Title = "Profile picture updated",
                TitleColor = "#00d77c",
                Message = "Your profile picture is succesfully uploaded"
            };
        }

        public async Task GetMovieCalendarAsync()
        {
            var mediaSystem = new MediaSystem();
            await mediaSystem.GetMovieCalendarAsync();
        }

        public Task GetTvShowCalendarAsync()
        {
            throw new NotImplementedException();
        }
    }
}

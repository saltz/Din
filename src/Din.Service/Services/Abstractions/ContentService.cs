using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Din.Data;
using Din.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Din.Service.Services.Abstractions
{
    public abstract class ContentService : BaseService
    {
        private readonly DinContext _context;
        protected readonly IMapper Mapper;

        protected ContentService(DinContext context, IMapper mapper)
        {
            _context = context;
            Mapper = mapper;
        }

        protected async Task LogContentAdditionAsync(string title, int accountId, ContentType type, int foreignId)
        {
            var account = await _context.Account.FirstAsync(a => a.ID.Equals(accountId));

            if(account.AddedContent == null)
                account.AddedContent = new List<AddedContentEntity>();

            _context.Attach(account);
            account.AddedContent.Add(new AddedContentEntity
            {
                ForeignId = foreignId,
                Title = title,
                DateAdded = DateTime.Now,
                Status = ContentStatus.Queued,
                Account = account,
                Type = type
            });
                
            await _context.SaveChangesAsync();
        }

        protected string GenerateTitleSlug(string title, DateTime date)
        {
            return $"{title.ToLower().Replace(" ", "-")}-{date.Year.ToString().ToLower()}";
        }     

        //private async Task<List<AddedContent>> PerformUpdateAsync(Account account)
        //{
        //    var content = await _context.AddedContent.Where(ac => ac.Account.Equals(account) && !ac.Status.Equals(ContentStatus.Downloaded)).ToListAsync();
        //    _mediaSystem = new MediaSystem.MediaSystem(_propertyFile.get("mediaSystem"));
        //    _downloadSystem = new DownloadSystem.DownloadSystem(_propertyFile.get("downloadSystemUrl"), _propertyFile.get("downloadSystemPwd"));
        //    content = await _mediaSystem.CheckIfItemIsCompletedAsync(content);
        //    var downloadClientItems = await _downloadSystem.GetAllItemsAsync();
        //    foreach (var item in content)
        //    foreach (var dItem in downloadClientItems)
        //    {
        //        var titles = FixNames(item.Title, dItem.Name);
        //        if (!(titles[0].CalculateSimilarity(titles[1]) > 0.4)) continue;
        //        var responseItem = await _downloadSystem.GetItemStatusAsync(dItem.Hash);
        //        item.Eta = responseItem.Eta;
        //        item.Percentage = (responseItem.FileProgress.Sum()) / responseItem.Files.Count;
        //        break;
        //    }

        //    _context.Attach(account);
        //    account.AddedContent.AddRange(content);
        //    await _context.SaveChangesAsync();
        //    return account.AddedContent;
        //}

        //private string[] FixNames(string title1, string title2)
        //{
        //    title1 = title1.Replace(" ", ".");
        //    if (title2.Contains("1080p"))
        //        title2 = title2.Substring(0, title2.IndexOf("1080p", StringComparison.Ordinal));

        //    if (title2.Contains("720p"))
        //        title2 = title2.Substring(0, title2.IndexOf("720p", StringComparison.Ordinal));

        //    title1 = Regex.Replace(title1, @"[\d-]", string.Empty);
        //    title2 = Regex.Replace(title2, @"[\d-]", string.Empty);
        //    return new[] { title1, title2 };
        //}
    }
}
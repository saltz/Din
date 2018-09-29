using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Din.Data;
using Din.Data.Entities;
using Din.Service.Clients.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Din.Service.BackgroundServices.Concrete
{
    public class copy
    {
        private readonly DinContext _context;
        private readonly IMovieClient _movieClient;
        private readonly ITvShowClient _tvShowClient;
        private readonly IDownloadClient _downloadClient;
        private List<AddedContentEntity> _content;

        public copy(DinContext context, IMovieClient movieClient, ITvShowClient tvShowClient, IDownloadClient downloadClient)
        {
            _context = context;
            _movieClient = movieClient;
            _tvShowClient = tvShowClient;
            _downloadClient = downloadClient;
        }

        public async Task UpdateAddedContentStatus( )
        {
            _content = await _context.AddedContent.ToListAsync();
            CheckIfDone();
            CheckIfDownloading();
        }

    
        private void CheckIfDone()
        {
            var movieCollection = _movieClient.GetCurrentMoviesAsync().Result;
            //var tvShowCollection = _tvShowClient.GetCurrentTvShowsAsync().Result;

            foreach (var c in _content.Where(uac => uac.Type.Equals(ContentType.Movie)))
            {
                if (movieCollection.First(i => i.Id.Equals(c.ForeignId)).Downloaded)
                {
                    c.Status = ContentStatus.Done;
                }
            }

            //TODO check if tvshow content items are done
        }

        private void CheckIfDownloading()
        {
            var downloadCollection = _downloadClient.GetAllItemsAsync().Result;


        }
    }
}

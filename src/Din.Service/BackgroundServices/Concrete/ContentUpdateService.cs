using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Din.Data;
using Din.Data.Entities;
using Din.Service.BackgroundServices.Abstractions;
using Din.Service.Clients.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Din.Service.BackgroundServices.Concrete
{
    public class ContentUpdateService : ScheduledProcessor
    {
        protected override string Schedule => "*/5 * * * *";

        public ContentUpdateService(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
        }

        public override async Task ProcessInScope(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<DinContext>();
            var movieClient = serviceProvider.GetService<IMovieClient>();
            var tvShowClient = serviceProvider.GetService<ITvShowClient>();

            var content = await context.AddedContent.ToListAsync();
            content.RemoveAll(c => c.Status.Equals(ContentStatus.Done));

            await UpdateStatusMovieObjects(content, movieClient);
            await UpdateStatusTvShowObjects(content, tvShowClient);

            await context.SaveChangesAsync();
        }

        private async Task UpdateStatusMovieObjects(List<AddedContentEntity> content, IMovieClient movieClient)
        {
            content = content.Where(c => c.Type.Equals(ContentType.Movie)).ToList();

            var movieCollection = (await movieClient.GetCurrentMoviesAsync()).ToList();

            foreach (var c in content)
            {
                if (movieCollection.First(i => i.Id.Equals(c.ForeignId)).Downloaded)
                {
                    c.Status = ContentStatus.Done;
                    continue;
                }

                if (DateTime.Now >= c.DateAdded.AddDays(3))
                {
                    c.Status = ContentStatus.NotAvailable;
                }

                //TODO check download system
            }
        }

        private async Task UpdateStatusTvShowObjects(List<AddedContentEntity> content, ITvShowClient tvShowClient)
        {
            content = content.Where(c => c.Type.Equals(ContentType.TvShow)).ToList();

            var tvShowCollection = (await tvShowClient.GetCurrentTvShowsAsync()).ToList();

            foreach (var c in content)
            {
                var show = tvShowCollection.First(i => i.Id.Equals(c.ForeignId));
                show.Seasons.Remove(show.Seasons.First(s => s.SeasonsNumber.Equals(0)));

                var seasonCheck = 0;

                foreach (var s in show.Seasons)
                {
                    if (s.Statistics.EpisodeCount.Equals(s.Statistics.TotalEpisodeCount))
                    {
                        seasonCheck++;
                        continue;
                    }

                    break;
                }

                if (seasonCheck.Equals(show.Seasons.Count))
                {
                    c.Status = ContentStatus.Done;
                    continue;
                }

                if (DateTime.Now >= c.DateAdded.AddDays(3))
                {
                    c.Status = ContentStatus.NotAvailable;
                }

                //TODO check download system
            }
        }
    }
}
using System;
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
        protected override string Schedule => "*/1 * * * *";

        public ContentUpdateService(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
        }

        public override async Task ProcessInScope(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<DinContext>();
            var movieClient = serviceProvider.GetService<IMovieClient>();
            var tvShowClient = serviceProvider.GetService<ITvShowClient>();

            await UpdateStatusMovieObjects(context, movieClient);
            await UpdateStatusTvShowObjects(context, tvShowClient);
        }

        private async Task UpdateStatusMovieObjects(DinContext context, IMovieClient movieClient)
        {
            var content = await context.AddedContent.Where(ac => ac.Type.Equals(ContentType.Movie)).ToListAsync();
            var movieCollection = (await movieClient.GetCurrentMoviesAsync()).ToList();

            foreach (var c in content)
            {
                if (movieCollection.First(i => i.Id.Equals(c.ForeignId)).Downloaded)
                {
                    c.Status = ContentStatus.Done;
                    break;
                }

                if (DateTime.Now >= c.DateAdded.AddDays(3))
                {
                    c.Status = ContentStatus.NotAvailable;
                    break;
                }

                //if in download system : downloading

            }
        }

        private async Task UpdateStatusTvShowObjects(DinContext context, ITvShowClient tvShowClient)
        {
            var content = await context.AddedContent.Where(ac => ac.Type.Equals(ContentType.TvShow)).ToListAsync();
            var tvShowCollection = (await tvShowClient.GetCurrentTvShowsAsync()).ToList();

            foreach (var c in content)
            {

            }
        }
    }
}

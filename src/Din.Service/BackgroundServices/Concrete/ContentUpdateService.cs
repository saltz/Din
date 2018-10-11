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
using Microsoft.Extensions.Logging;

namespace Din.Service.BackgroundServices.Concrete
{
    public class ContentUpdateService : ScheduledProcessor
    {
        protected override string Schedule => "*/5 * * * *";
        private ILogger<ContentUpdateService> _logger;

        public ContentUpdateService(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
        }

        public override async Task ProcessInScope(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetService<ILogger<ContentUpdateService>>();

            _logger.LogInformation("Starting content update cycle");

            var context = serviceProvider.GetService<DinContext>();
            var movieClient = serviceProvider.GetService<IMovieClient>();
            var tvShowClient = serviceProvider.GetService<ITvShowClient>();

            await UpdateStatusMovieObjects(context, movieClient);
            await UpdateStatusTvShowObjects(context, tvShowClient);

            await context.SaveChangesAsync();

            _logger.LogInformation("End of content update cycle");
        }

        private async Task UpdateStatusMovieObjects(DinContext context, IMovieClient movieClient)
        {
            try
            {
                var content = await context.AddedContent.Where(c =>
                    c.Type.Equals(ContentType.Movie) && !c.Status.Equals(ContentStatus.Done)).ToListAsync();
                var queue = (await movieClient.GetQueue()).ToList();
                var now = DateTime.Now;

                _logger.LogInformation($"Updating: {content.Count} movies");

                foreach (var c in content)
                {
                    var movie = await movieClient.GetMovieByIdAsync(c.SystemId);
                    var item = queue.Find(q => q.Movie.SystemId.Equals(c.SystemId));


                    if (movie.Downloaded)
                    {
                        c.Status = ContentStatus.Done;
                        continue;
                    }

                    if (item != null)
                    {
                        c.Status = ContentStatus.Downloading;
                        c.Percentage = Math.Round(1 - (item.SizeLeft / item.Size), 2);
                        c.Eta = (int) item.TimeLeft.TotalSeconds;
                    }

                    if (now >= c.DateAdded.AddDays(2) && c.Percentage > 0.0)
                    {
                        c.Status = ContentStatus.Stuck;
                        continue;
                    }

                    if (now >= c.DateAdded.AddDays(3))
                    {
                        c.Status = ContentStatus.NotAvailable;
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Updating movies encounterd error: {e.Message}");
            }
        }

        private async Task UpdateStatusTvShowObjects(DinContext context, ITvShowClient tvShowClient)
        {
            try
            {
                var content = await context.AddedContent
                    .Where(c => c.Type.Equals(ContentType.TvShow) && !c.Status.Equals(ContentStatus.Done))
                    .ToListAsync();
                var now = DateTime.Now;

                _logger.LogInformation($"Updating: {content.Count} tvshows");

                foreach (var c in content)
                {
                    var show = await tvShowClient.GetTvShowByIdAsync(c.SystemId);
                    var pilotSeason = show.Seasons.FirstOrDefault(s => s.SeasonsNumber.Equals(0));

                    if (pilotSeason != null)
                    {
                        show.Seasons.Remove(pilotSeason);
                    }

                    var seasonsDone = 0;
                    var seasonPercentage = new List<double>();

                    foreach (var s in show.Seasons)
                    {
                        if (s.Statistics.EpisodeCount.Equals(s.Statistics.TotalEpisodeCount))
                        {
                            seasonsDone++;
                            continue;
                        }

                        seasonPercentage.Add(Convert.ToDouble(s.Statistics.EpisodeCount) /
                                             (Convert.ToDouble(s.Statistics.TotalEpisodeCount) / 100));
                    }

                    if (seasonsDone.Equals(show.Seasons.Count))
                    {
                        c.Status = ContentStatus.Done;
                        continue;
                    }

                    var showPercentage = Math.Round((seasonPercentage.Sum() / seasonPercentage.Count) / 100, 2);

                    if (showPercentage > 0.0)
                    {
                        c.Status = ContentStatus.Downloading;
                        c.Percentage = showPercentage;
                    }

                    if (now >= c.DateAdded.AddDays(2) && showPercentage > 0.0)
                    {
                        c.Status = ContentStatus.Stuck;
                        c.Percentage = showPercentage;
                        continue;
                    }

                    if (now >= c.DateAdded.AddDays(5))
                    {
                        c.Status = ContentStatus.NotAvailable;
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Updating tvshows encounterd error: {e.Message}");
            }
        }
    }
}
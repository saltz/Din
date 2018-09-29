
/*

        public async Task<List<MediaSystemMovieCalendarObject>> GetMovieCalendarAsync()
        {
            return JsonConvert.DeserializeObject<List<MediaSystemMovieCalendarObject>>(
                await new HttpRequestHelper(BuildRequestUrl(Selection.MovieSystem, Endpoint.Calendar) + "&start=" +
                                            DateTime.Now.ToString("yyyy-MM-dd") + "&end=" +
                                            DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd"), false)
                    .PerformGetRequestAsync());
        }

        public async Task<List<MediaSystemTvShowCalendarObject>> GetTvShowCalendarAsync()
        {
            return JsonConvert.DeserializeObject<List<MediaSystemTvShowCalendarObject>>(
                await new HttpRequestHelper(BuildRequestUrl(Selection.TvShowSystem, Endpoint.Calendar) + "&start=" +
                                            DateTime.Now.ToString("yyyy-MM-dd") + "&end=" + DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd"), false)
                    .PerformGetRequestAsync());
        }

        //TODO Check this code for optimization
        public async Task<List<AddedContent>> CheckIfItemIsCompletedAsync(List<AddedContent> content)
        {
            var httpRequest = new HttpRequestHelper(_url, false);
            var current =
                JsonConvert.DeserializeObject<List<MediaSystemMovie>>(await httpRequest.PerformGetRequestAsync());
            foreach (var i in content)
            foreach (var m in current)
            {
                if (!i.Title.Equals(m.Title)) continue;
                if (!m.Downloaded) continue;
                i.Status = ContentStatus.Done;
                break; 
            }

            return content;
        }

*/
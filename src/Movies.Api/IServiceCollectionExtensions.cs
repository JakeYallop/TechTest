using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movies.Api.Database;
using Movies.Api.Models;

namespace Movies.Api
{
    public static class IServiceCollectionExtensions
    {
        private sealed class MetadataMap : ClassMap<MovieMetadata>
        {
            public MetadataMap()
            {
                Map(p => p.Id);
                Map(p => p.MovieId);
                Map(p => p.Title);
                Map(p => p.LanguageCode).Name("Language");
                Map(p => p.Duration);
                Map(p => p.ReleaseYear);
            }
        }

        private sealed class StatsMap : ClassMap<RawMovieStats>
        {
            public StatsMap()
            {
                Map(p => p.MovieId).Name("movieId");
                Map(p => p.WatchDuration).Name("watchDurationMs");
            }
        }

        private sealed record RawMovieStats(int MovieId, int WatchDuration)
        {
            private RawMovieStats() : this(default, default)
            {

            }
        }

        public static void AddMoviesDatabase(this IServiceCollection services, string metadataPath, string statsPath)
        {
            using var metadataReader = new StreamReader(metadataPath);
            using var metadataCsv = new CsvReader(metadataReader, CultureInfo.InvariantCulture);
            metadataCsv.Context.RegisterClassMap<MetadataMap>();
            var movieMetadata = metadataCsv.GetRecords<MovieMetadata>().ToList();

            using var statsReader = new StreamReader(statsPath);
            using var statsCsv = new CsvReader(statsReader, CultureInfo.InvariantCulture);
            statsCsv.Context.RegisterClassMap<StatsMap>();
            var rawMovieStats = statsCsv.GetRecords<RawMovieStats>().ToList();
            var movieStats = rawMovieStats.GroupBy(s => s.MovieId)
                .Select(s =>
                {
                    var movieId = s.Key;
                    var averageWatchDurationMs = Math.Floor(s.Average(x => x.WatchDuration));
                    var watches = s.Count();
                    return new MovieStats(movieId, TimeSpan.FromMilliseconds(averageWatchDurationMs), watches);
                });

            services.AddSingleton(new MoviesDatabase(movieMetadata, movieStats));
        }
    }
}

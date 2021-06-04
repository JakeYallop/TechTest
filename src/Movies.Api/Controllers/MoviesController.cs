using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Movies.Api.Database;
using Movies.Api.Models;

namespace Movies.Api.Controllers
{
    [Route("/movies")]
    public class MoviesController : ApiControllerBase
    {
        public MoviesController(MoviesDatabase database)
        {
            Database = database ?? throw new ArgumentNullException(nameof(database));
        }

        public MoviesDatabase Database { get; }

        [HttpGet("stats")]
        public IActionResult GetStats()
        {
            var movies = Database.MoviesMetadata.GroupBy(x => x.MovieId)
                //select the first metadata record for each movie, defaulting to the english title
                .Select(x => x.FirstOrDefault(x => x.LanguageCode == "EN") ?? x.First())
                .Join(Database.MovieStats, m => m.MovieId, m => m.MovieId, (metadata, stats) => new MovieStatsData
                (
                    metadata.MovieId,
                    metadata.Title,
                    (int)stats.AverageWatchDurationS.TotalSeconds,
                    stats.Watches,
                    metadata.ReleaseYear
                )).OrderByDescending(m => m.Watches).ThenByDescending(m => m.ReleaseYear);

            return Ok(movies);
        }
    }
}

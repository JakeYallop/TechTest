using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Movies.Api.Database;

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

        [HttpGet("/stats")]
        public IActionResult GetStats()
        {
            var movies = Database.MoviesMetadata.Join(Database.MovieStats, m => m.Id, m => m.Id, (metadata, stats) => new
            {
                metadata.Id,
                metadata.Title,
                stats.AverageWatchDurationS,
                stats.Watches,
                metadata.ReleaseYear
            }).OrderByDescending(m => m.Watches).ThenByDescending(m => m.ReleaseYear);
            return Ok(movies);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Movies.Api.Database;
using Movies.Api.Models;

namespace Movies.Api.Controllers
{
    [Route("/metadata")]
    public class MetadataController : ApiControllerBase
    {
        public MetadataController(MoviesDatabase database)
        {
            Database = database ?? throw new ArgumentNullException(nameof(database));
        }

        public MoviesDatabase Database { get; }

        private readonly object dbLock = new();
        [HttpPost("")]
        public IActionResult AddMetadata([FromBody] MovieData data)
        {
            //avoid race condition - if 2 requests are made simultaneously, they would recieve the same id.
            var metadataId = 1;
            lock (dbLock)
            {
                if (!Database.MoviesMetadata.IsEmpty)
                {
                    metadataId = Database.MoviesMetadata.Max(x => x.Id) + 1;
                }
                var metadata = new MovieMetadata(metadataId, data.MovieId, data.Title, data.Language, TimeSpan.Parse(data.Duration!), data.ReleaseYear);
                Database.MoviesMetadata.Add(metadata);
            }
            //No content
            return StatusCode(204);
        }

        [HttpGet("{movieId}")]
        public IActionResult GetMetadata(int movieId)
        {
            var nonUniqueLanguageMovies = Database.MoviesMetadata
                .Where(m => m.MovieId == movieId)
                .Where(m => m.IsValid())
                //order by metadata id descending, so that we select the latest piece of metadata if we find duplicates below
                .OrderByDescending(m => m.Id)
                .ThenBy(x => x.LanguageCode);
            //select highest movie id from movies
            var seenMovieIds = new HashSet<(int, string?)>();
            var movies = new List<MovieMetadata>();
            foreach (var movie in nonUniqueLanguageMovies)
            {
                //if we've already added a movie with the given id and language, then skip it
                if (!seenMovieIds.Contains((movie.MovieId, movie.LanguageCode)))
                {
                    seenMovieIds.Add((movie.MovieId, movie.LanguageCode));
                    movies.Add(movie);
                }
            }
            //order by language code as required by brief
            movies = movies.OrderBy(m => m.LanguageCode).ToList();

            if (movies.Count == 0)
            {
                return NotFound();
            }
            return Ok(movies);
        }
    }
}

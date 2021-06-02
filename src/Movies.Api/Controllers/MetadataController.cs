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

        [HttpPost("")]
        public IActionResult AddMetadata()
        {

        }

        [HttpGet("{movieId}")]
        public IActionResult GetMetadata(int movieId)
        {
            var nonUniqueLanguageMovies = Database.MoviesMetadata.Where(m => m.Id == movieId).Where(m => m.IsValid()).OrderByDescending(m => m.Id).ThenBy(x => x.LanguageCode);
            //select highest movie id from movies
            var seenMovieIds = new HashSet<(int, string?)>();
            var movies = new List<MovieMetadata>();
            foreach (var movie in nonUniqueLanguageMovies)
            {
                //if we've already added a movie with the given id and language, then skip it
                if (!seenMovieIds.Contains((movie.Id, movie.LanguageCode)))
                {
                    movies.Add(movie);
                }
            }

            if (movies.Count == 0)
            {
                return NotFound();
            }
            return Ok(movies);
        }
    }
}

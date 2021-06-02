using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movies.Api.Models;

namespace Movies.Api.Database
{
    public class MoviesDatabase
    {
        public List<MovieMetadata> MoviesMetadata { get; }
        public List<MovieStats> MovieStats { get; }
        public MoviesDatabase(IEnumerable<MovieMetadata> movies, IEnumerable<MovieStats> movieMetadata)
        {
            MoviesMetadata = movies.ToList();
            MovieStats = movieMetadata.ToList();
        }
    }
}

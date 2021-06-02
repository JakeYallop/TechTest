using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movies.Api.Models;

namespace Movies.Api.Database
{
    public class MoviesDatabase
    {
        public IEnumerable<Movie> Movies { get; }
        public IEnumerable<MovieMetadata> MovieMetadata { get; }
        public MoviesDatabase(IEnumerable<Movie> movies, IEnumerable<MovieMetadata> movieMetadata)
        {
            Movies = movies;
            MovieMetadata = movieMetadata;
        }
    }
}

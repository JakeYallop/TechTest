using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Api.Database
{
    public class MoviesDatabase
    {
        public ConcurrentBag<MovieMetadata> MoviesMetadata { get; }
        public ConcurrentBag<MovieStats> MovieStats { get; }
        public MoviesDatabase(IEnumerable<MovieMetadata> movieMetadata, IEnumerable<MovieStats> movieStats)
        {
            MoviesMetadata = new ConcurrentBag<MovieMetadata>(movieMetadata);
            MovieStats = new ConcurrentBag<MovieStats>(movieStats);
        }
    }
}

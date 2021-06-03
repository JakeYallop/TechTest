using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Api.Models
{
    public class MovieData
    {
        public int MovieId { get; }
        public string? Title { get; }
        public string? LanguageCode { get; }
        public TimeSpan Duration { get; }
        public string? ReleaseYear { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Api.Database
{
    public record MovieMetadata(int Id, int MovieId, string? Title, string? LanguageCode, TimeSpan Duration, int ReleaseYear)
    {
        private MovieMetadata() : this(default, default, default, default, default, default)
        {

        }
        //for dotnet 5.0+, we could use the MemberNotNull attribute. Not available in net core 3.1
        public bool IsValid()
        {
            return MovieId != default && !string.IsNullOrWhiteSpace(Title) && !string.IsNullOrWhiteSpace(LanguageCode) && Duration != default && ReleaseYear != default;
        }
    }
}

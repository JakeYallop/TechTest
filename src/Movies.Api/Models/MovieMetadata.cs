using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Api.Models
{
    public record MovieMetadata(int Id, string? Title, RegionInfo? LanguageCode, TimeSpan Duration, string? Year)
    {
        //for dotnet 5.0+, we could use the MemberNotNull attribute. Not available in net core 3.1
        public bool IsValid()
        {
            return Id != default && !string.IsNullOrWhiteSpace(Title) && LanguageCode is not null && Duration != default && string.IsNullOrWhiteSpace(Year);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Api.Models
{
    public record MovieStatsData(int MovieId, string? Title, int AverageWatchDurationS, int Watches, int ReleaseYear)
    {
    }
}

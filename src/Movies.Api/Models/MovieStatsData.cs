using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Api.Models
{
    public record MovieStatsData(int Id, string? Title, TimeSpan AverageWatchDurationS, int Watches, int ReleaseYear)
    {
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Api.Models
{
    public record MovieStats(TimeSpan AverageWatchDurationS, int Watches);
}

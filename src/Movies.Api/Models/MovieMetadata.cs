using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Api.Models
{
    public record MovieMetadata(TimeSpan AverageWatchDurationS, int Watches);
}

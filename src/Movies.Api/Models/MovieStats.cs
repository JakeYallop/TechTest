﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Api.Models
{
    public record MovieStats(int MovieId, TimeSpan AverageWatchDurationS, int Watches);
}

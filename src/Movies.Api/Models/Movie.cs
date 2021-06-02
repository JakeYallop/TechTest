using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Api.Models
{
    public record Movie(int Id, string Title, RegionInfo LanguageCode, TimeSpan Duration, string Year);
}

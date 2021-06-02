using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Movies.Api.Database;

namespace Movies.Api.Controllers
{
    [Route("/movies")]
    public class MoviesController : ApiControllerBase
    {
        public MoviesController(MoviesDatabase database)
        {
            Database = database ?? throw new ArgumentNullException(nameof(database));
        }

        public MoviesDatabase Database { get; }

        [HttpGet("/stats")]
        public IActionResult GetStats()
        {

        }
    }
}

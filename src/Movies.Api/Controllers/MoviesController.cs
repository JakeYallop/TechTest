using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Movies.Api.Controllers
{
    [Route("/movies")]
    public class MoviesController : ApiControllerBase
    {
        [HttpGet("/stats")]
        public IActionResult GetStats()
        {

        }
    }
}

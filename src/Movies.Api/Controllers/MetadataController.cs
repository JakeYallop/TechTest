using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Movies.Api.Database;

namespace Movies.Api.Controllers
{
    [Route("/metadata")]
    public class MetadataController : ApiControllerBase
    {
        public MetadataController(MoviesDatabase database)
        {
            Database = database ?? throw new ArgumentNullException(nameof(database));
        }

        public MoviesDatabase Database { get; }

        [HttpPost("")]
        public IActionResult AddMetadata()
        {

        }

        [HttpGet("{metadataId}")]
        public IActionResult GetMetadata(int metadataId)
        {

        }
    }
}

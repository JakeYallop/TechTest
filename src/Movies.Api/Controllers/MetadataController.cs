using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Movies.Api.Controllers
{
    [Route("/metadata")]
    public class MetadataController : ApiControllerBase
    {
        public MetadataController()
        {
            
        }

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Conference.Controllers
{ [Route("api/speakers")]
    [ApiController]
    //[ApiVersion("2.0")]
    public class Speakers2Controller : ControllerBase
    {
        // GET: api/Speakers2
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


    }
}
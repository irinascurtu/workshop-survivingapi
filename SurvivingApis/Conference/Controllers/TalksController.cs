using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Conference.Models;
using Core.Data;
using Core.Domain;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Conference.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TalksController : ControllerBase
    {
        private readonly ITalkRepository _talkRepository;
        private readonly ISpeakerRepository _speakerRepository;
        private readonly IMapper _mapper;

        public TalksController(ITalkRepository talkRepository,
            ISpeakerRepository speakerRepository, IMapper mapper
            )
        {
            _talkRepository = talkRepository;
            _speakerRepository = speakerRepository;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetTalksForSpeaker")]
        public IActionResult GetTalksForSpeaker(int speakerId)
        {

            throw new NotImplementedException();
        }


        //will add the details and everything from ApiBehaviour
        public override ActionResult ValidationProblem(
            [ActionResultObjectValue] ModelStateDictionary modelStateDictionary)
        {
            var options = HttpContext.RequestServices
                .GetRequiredService<IOptions<ApiBehaviorOptions>>();
            return (ActionResult)options.Value.InvalidModelStateResponseFactory(ControllerContext);
        }
    }
}

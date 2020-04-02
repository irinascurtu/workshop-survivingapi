using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Conference.Models;
using Core.Data;
using Core.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Conference.Controllers
{

    [ApiController]
    [Route("api/speakers/{speakerId}/talks")]
    public class SpeakerTalksController : ControllerBase
    {
        private readonly ITalkRepository _talkRepository;
        private readonly ISpeakerRepository _speakerRepository;
        private readonly IMapper _mapper;

        public SpeakerTalksController(ITalkRepository talkRepository,
            ISpeakerRepository speakerRepository, IMapper mapper
            )
        {
            _talkRepository = talkRepository;
            _speakerRepository = speakerRepository;
            _mapper = mapper;
        }


        [HttpGet(Name = "GetTalksForSpeaker")]
        public ActionResult<IEnumerable<TalkDto>> GetTalksForSpeaker(int speakerId)
        {

            if (!_speakerRepository.SpeakerExists(speakerId))
            {
                return NotFound();
            }
            var talksForSpeaker = _talkRepository.GetTalks(speakerId).ToList();
            return Ok(_mapper.Map<IEnumerable<TalkDto>>(talksForSpeaker));
        }

        [HttpGet("{talkId}", Name = "GetTalkForSpeaker")]
        public ActionResult<TalkDto> GetTalksForSpeaker(int speakerId, int talkId)
        {
            if (!_speakerRepository.SpeakerExists(speakerId))
            {
                return NotFound();
            }

            var talkForSpeaker = _talkRepository.GetTalk(speakerId, talkId);

            if (talkForSpeaker == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TalkDto>(talkForSpeaker));
        }

     

        [HttpPost(Name = "CreateTalkForSpeaker")]
        public ActionResult<TalkDto> CreateTalkForSpeaker(
            int speakerId, TalkForModificationDto talk)
        {
            if (!_speakerRepository.SpeakerExists(speakerId))
            {
                return NotFound();
            }

            var talkEntity = _mapper.Map<Talk>(talk);
            _talkRepository.AddTalk(speakerId, talkEntity);
            _talkRepository.Save();

            var talkToReturn = _mapper.Map<TalkDto>(talkEntity);

            return CreatedAtRoute("GetTalkForSpeaker",
                new
                {
                    speakerId = speakerId,
                    talkId = talkToReturn.Id
                },
                talkToReturn);
        }

        [HttpPut("{talkId}")]
        public IActionResult UpdateTalkForSpeaker(int speakerId,
            int talkId,
            TalkUpdateDto talk)
        {
            if (!_speakerRepository.SpeakerExists(speakerId))
            {
                return NotFound();
            }

            var talkForSpeaker = _talkRepository.GetTalk(speakerId, talkId);
            if (talkForSpeaker == null)
            {
                var talkToAdd = _mapper.Map<Talk>(talk);
                talkToAdd.Id = talkId;

                _talkRepository.AddTalk(speakerId, talkToAdd);
                _talkRepository.Save();
                var talkToReturn = _mapper.Map<TalkDto>(talkToAdd);

                return CreatedAtRoute("GetTalkForSpeaker",
                    new { speakerId, talkId = talkToReturn.Id },
                    talkToReturn);
            }

            _mapper.Map(talk, talkForSpeaker);
            _talkRepository.UpdateTalk(talkForSpeaker);
            _talkRepository.Save();
            return NoContent();
        }


        [HttpPatch("{talkId}")]
        public ActionResult PartiallyUpdateTalkForSpeaker(int speakerId,
            int talkId,
            JsonPatchDocument<TalkUpdateDto> patchDocument)
        {
            if (!_speakerRepository.SpeakerExists(speakerId))
            {
                return NotFound();
            }

            var talkForSpeakerFromRepo = _talkRepository.GetTalk(speakerId, talkId);

            if (talkForSpeakerFromRepo == null)
            {
                var talkDto = new TalkUpdateDto();
                patchDocument.ApplyTo(talkDto, ModelState);

                if (!TryValidateModel(talkDto))
                {
                    return ValidationProblem(ModelState);
                }

                var talkToAdd = _mapper.Map<Talk>(talkDto);
                talkToAdd.Id = talkId;

                _talkRepository.AddTalk(speakerId, talkToAdd);
                _talkRepository.Save();

                var talkToReturn = _mapper.Map<TalkDto>(talkToAdd);

                return CreatedAtRoute("GetTalkForSpeaker",
                    new { speakerId, talkId = talkToReturn.Id },
                    talkToReturn);
            }

            var talkToPatch = _mapper.Map<TalkUpdateDto>(talkForSpeakerFromRepo);
            // add validation

            patchDocument.ApplyTo(talkToPatch);
            if (!TryValidateModel(talkToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(talkToPatch, talkForSpeakerFromRepo);

            _talkRepository.UpdateTalk(talkForSpeakerFromRepo);

            _talkRepository.Save();

            return NoContent();
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

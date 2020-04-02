using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Conference.Models;

using Core.Data;
using Core.Data.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using System.Xml.Serialization;
using Conference.Helpers;
using Microsoft.Net.Http.Headers;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace Conference.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SpeakersController : ControllerBase
    {
        private readonly ISpeakerRepository _speakerRepository;

        private readonly IMapper _mapper;

        public SpeakersController(ISpeakerRepository speakerRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _speakerRepository = speakerRepository;
        }


        [HttpOptions]
        public IActionResult GetSpeakersOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST");
            return Ok();
        }

        //[HttpGet(Name = "GetSpeakers")]
        //[HttpHead]
        //public IActionResult GetSpeakers()
        //{
        //    var speakersFromRepo = _speakerRepository.GetSpeakers();
        //    //map from repo to DTO

        //    return Ok(_mapper.Map<IEnumerable<SpeakerDto>>(speakersFromRepo));
        //}

        [HttpGet(Name = "GetSpeakers")]
        [HttpHead]
        public IActionResult GetSpeakers([FromQuery]SpeakerResourceParameters speakersParam)
        {
            var speakersFromRepo = _speakerRepository.GetSpeakers(speakersParam);
            //map from repo to DTO
            var paginationMetadata = new
            {
                totalCount = speakersFromRepo.TotalCount,
                pageSize = speakersFromRepo.PageSize,
                currentPage = speakersFromRepo.CurrentPage,
                totalPages = speakersFromRepo.TotalPages
            };
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
            //response.Headers.Add("X-Paging-PageNo", pageNo.ToString());
            //response.Headers.Add("X-Paging-PageSize", pageSize.ToString());
            //response.Headers.Add("X-Paging-PageCount", pageCount.ToString());
            //response.Headers.Add("X-Paging-TotalRecordCount", total.ToString());
            var links = CreateLinksForSpeakers(speakersParam,
                speakersFromRepo.HasNext,
                speakersFromRepo.HasPrevious);

            var shapedSpeakers = _mapper.Map<IEnumerable<SpeakerDto>>(speakersFromRepo)
                .ShapeData();

            var speakerWithLinks = shapedSpeakers.Select(speaker =>
            {
                var speakerAsDictionary = speaker as IDictionary<string, object>;
                var speakerLinks = CreateLinksForSpeaker((int)speakerAsDictionary["Id"]);
                speakerAsDictionary.Add("links", speakerLinks);
                return speakerAsDictionary;
            });

            var linkedResource = new
            {
                value = speakerWithLinks,
                links
            };

            return Ok(linkedResource);
        }

        [HttpGet("{speakerId}", Name = "GetSpeaker")]
        public IActionResult GetSpeaker(int speakerId)

        {

            var speakerFromRepo = _speakerRepository.GetSpeaker(speakerId);

            if (speakerFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<SpeakerDto>(speakerFromRepo));
        }

        #region baseline
        private IEnumerable<LinkDto> CreateLinksForSpeakers(
            SpeakerResourceParameters speakersParam,
            bool hasNext, bool hasPrevious)
        {
            var links = new List<LinkDto>();

            // self 
            links.Add(
                new LinkDto(CreateSpeakersResourceUri(
                        speakersParam, ResourceUriType.Current)
                    , "self", "GET"));

            if (hasNext)
            {
                links.Add(
                    new LinkDto(CreateSpeakersResourceUri(
                            speakersParam, ResourceUriType.NextPage),
                        "nextPage", "GET"));
            }

            if (hasPrevious)
            {
                links.Add(
                    new LinkDto(CreateSpeakersResourceUri(
                            speakersParam, ResourceUriType.PreviousPage),
                        "previousPage", "GET"));
            }

            return links;
        }

        private string CreateSpeakersResourceUri(
            SpeakerResourceParameters speakersParam,
            ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link("GetSpeakers",
                      new
                      {
                          pageNumber = speakersParam.PageNumber - 1,
                          pageSize = speakersParam.PageSize
                      });
                case ResourceUriType.NextPage:
                    return Url.Link("GetSpeakers",
                      new
                      {
                          pageNumber = speakersParam.PageNumber + 1,
                          pageSize = speakersParam.PageSize
                      });
                case ResourceUriType.Current:
                default:
                    return Url.Link("GetSpeakers",
                    new
                    {
                        pageNumber = speakersParam.PageNumber,
                        pageSize = speakersParam.PageSize
                    });
            }

        }

        private IEnumerable<LinkDto> CreateLinksForSpeaker(int speakerId)
        {
            var links = new List<LinkDto>();

            links.Add(
                new LinkDto(Url.Link("GetSpeaker", new { speakerId }),
                    "self",
                    "GET"));


            links.Add(
                new LinkDto(Url.Link("DeleteSpeaker", new { speakerId }),
                    "delete_speaker",
                    "DELETE"));

            links.Add(
                new LinkDto(Url.Link("CreateTalkForSpeaker", new { speakerId }),
                    "create_talk_for_speaker",
                    "POST"));

            links.Add(
                new LinkDto(Url.Link("GetTalksForSpeaker", new { speakerId }),
                    "talks",
                    "GET"));

            return links;
        }
        #endregion
    }
}

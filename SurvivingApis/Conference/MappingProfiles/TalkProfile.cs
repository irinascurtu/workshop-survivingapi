using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Conference.Models;
using Core.Domain;

namespace Conference.MappingProfiles
{

    public class TalkProfile : Profile
    {
        public TalkProfile()
        {
            CreateMap<Talk, Models.TalkDto>();
          //  CreateMap<Models.TalkForModificationDto, Talk>();
            CreateMap<TalkUpdateDto, Talk>();

            //CreateMap<Models.CourseForCreationDto, Entities.Course>();
         
            CreateMap<Talk, Models.TalkUpdateDto>();
        }
    }
}

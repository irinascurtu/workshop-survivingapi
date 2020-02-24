using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Domain;

namespace Conference.MappingProfiles
{

    public class SpeakerProfile : Profile
    {
        public SpeakerProfile()
        {
            CreateMap<Speaker, Models.SpeakerDto>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            CreateMap<Models.SpeakerForCreation, Speaker>();

            CreateMap<Speaker, Models.SpeakerFullDto>();

        }
    }
}

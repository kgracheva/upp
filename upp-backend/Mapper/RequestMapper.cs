﻿using AutoMapper;
using upp.Dtos.Calendar;
using upp.Dtos.Request;
using upp.Entities;

namespace upp.Mapper
{
    public class RequestMapper : Profile
    {
        public RequestMapper()
        {
            CreateMap<Request, RequestDto>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(x => x.Created));
            CreateMap<RequestDto, Request>()
                .ForMember(dest => dest.Created, opt => opt.Ignore());
        }
    }
}

using AutoMapper;
using bookspace.Api.DTO;
using bookspace.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookspace.Api.Profiles
{
    public class PaginationProfile : Profile
    {
        public PaginationProfile()
        {
            CreateMap<PaginationFilterDto, PaginationFilter>();
            CreateMap(typeof(Pagination<>), typeof(PaginatedResponseDto));
        }
    }
}

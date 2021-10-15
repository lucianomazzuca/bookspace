using AutoMapper;
using bookspace.Api.DTO.Genres;
using bookspace.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookspace.Api.Profiles
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<GenreCreateDto, Genre>();
            CreateMap<Genre, GenreReadDto>();
        }
    }
}

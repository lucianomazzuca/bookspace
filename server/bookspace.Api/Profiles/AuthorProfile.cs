using AutoMapper;
using bookspace.Api.DTO.Author;
using bookspace.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookspace.Api.Profiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<AuthorCreateDto, Author>();
            CreateMap<Author, AuthorReadDto>();
        }
    }
}

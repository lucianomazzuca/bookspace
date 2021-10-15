using AutoMapper;
using bookspace.Api.DTO.User;
using bookspace.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookspace.Api.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRegisterDTO, User>();
            CreateMap<UserLoginDTO, User>();
        }
    }
}

using AutoMapper;
using bookspace.Api.DTO.User;
using bookspace.Api.Entities;
using bookspace.Api.Exceptions;
using bookspace.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookspace.Api.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AuthController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(UserRegisterDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.Password = CryptographyService.GetSha256(userDto.Password);
            user.RoleId = 2;

            try
            {
                await _userService.Insert(user);
                return StatusCode(201);
            } catch (UserAlreadyExistsException e)
            {
                return StatusCode(409, new { e.Message });
            }
        }
    }
}

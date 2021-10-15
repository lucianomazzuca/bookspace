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
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthController(IUserService userService, IMapper mapper, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
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

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserLoginDTO userDto)
        {
            var user = await _userService.GetByEmail(userDto.Email);
            if (user == null)
            {
                return StatusCode(401, new { message = "Email is not registered" });
            }

            var isPasswordValid = _authService.VerifyPassword(user.Password, userDto.Password);
            if (!isPasswordValid)
            {
                return StatusCode(401, new { message = "Invalid credentials" });
            }

            var token = _authService.generateJwtToken(user);
            return Ok(new { token });
        }
    }
}

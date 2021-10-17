using AutoMapper;
using bookspace.Api.DTO.Author;
using bookspace.Api.Entities;
using bookspace.Api.Exceptions;
using bookspace.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookspace.Api.Controllers
{
    [Route("authors")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public AuthorController(IAuthorService authorService, IMapper mapper)
        {
            _authorService = authorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var authors = await _authorService.GetAll();
            var authorsDto = _mapper.Map<IEnumerable<AuthorReadDto>>(authors);
            return Ok(authorsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var author = await _authorService.GetById(id);
            if (author == null)
            {
                return NotFound();
            }

            var authorDto = _mapper.Map<AuthorReadDto>(author);

            return Ok(authorDto);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<ActionResult> Insert(AuthorCreateDto authorDto)
        {
            var author = _mapper.Map<Author>(authorDto);

            await _authorService.Insert(author);
            return StatusCode(201);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(AuthorCreateDto authorDto, int id)
        {
            var author = await _authorService.GetById(id);
            if (author == null)
            {
                return NotFound();
            }

            author = _mapper.Map<Author>(authorDto);
            author.Id = id;
            await _authorService.Update(author);
            return Ok();
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> SoftDelete(int id)
        {
            try
            {
                await _authorService.Delete(id);
                return Ok();
            }
            catch (RecordNotFoundException)
            {
                return NotFound();
            }
        }
    }
}

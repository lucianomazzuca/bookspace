using AutoMapper;
using bookspace.Api.DTO.Genres;
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
    [Route("genres")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;

        public GenreController(IGenreService genreService, IMapper mapper)
        {
            _genreService = genreService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var genres = await _genreService.GetAll();
            var genresDto = _mapper.Map<IEnumerable<GenreReadDto>>(genres);
            return Ok(genresDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var genre = await _genreService.GetById(id);
            if (genre == null)
            {
                return NotFound();
            }

            var genreDto = _mapper.Map<GenreReadDto>(genre);
            return Ok(genreDto);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<ActionResult> Insert(GenreCreateDto genreDto)
        {
            var genre = _mapper.Map<Genre>(genreDto);

            await _genreService.Insert(genre);
            return StatusCode(201);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(GenreCreateDto genreDto, int id)
        {
            var genre = await _genreService.GetById(id);
            if (genre == null)
            {
                return NotFound();
            }

            genre = _mapper.Map<Genre>(genreDto);
            genre.Id = id;
            await _genreService.Update(genre);
            return Ok();
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> SoftDelete(int id)
        {
            try
            {
                await _genreService.Delete(id);
                return Ok();
            }
            catch (RecordNotFoundException)
            {
                return NotFound();
            }
        }
    }
}

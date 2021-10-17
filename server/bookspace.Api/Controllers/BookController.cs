using AutoMapper;
using bookspace.Api.DTO.Book;
using bookspace.Api.Entities;
using bookspace.Api.Exceptions;
using bookspace.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookspace.Api.Controllers
{
    [ApiController]
    [Route("books")]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BookController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var books = await _bookService.GetAll();
            var booksDto = _mapper.Map<IEnumerable<BookReadDto>>(books);
            return Ok(booksDto);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<ActionResult> Insert(BookCreateDto bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);
            var genresIds = bookDto.GenresIds;

            try
            {
                await _bookService.Insert(book, genresIds);
                return StatusCode(201);
            }
            catch (RecordNotFoundException e)
            {
                return StatusCode(400, e.Message);
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(BookCreateDto bookDto, int id)
        {
            var book = _mapper.Map<Book>(bookDto);
            book.Id = id;
            var genresIds = bookDto.GenresIds;

            try
            {
                await _bookService.Update(book, genresIds);
                return StatusCode(200);
            }
            catch (RecordNotFoundException e)
            {
                return StatusCode(400, e.Message);
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> SoftDelete(int id)
        {
            try
            {
                await _bookService.SoftDelete(id);
                return Ok();
            }
            catch (RecordNotFoundException)
            {
                return NotFound();
            }
        }
    }
}

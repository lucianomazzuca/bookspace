using bookspace.Api.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bookspace.Api.DTO.Book
{
    public class BookCreateDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public IFormFile Image { get; set; }
        public int? AuthorId { get; set; }

        public ICollection<Genre> Genres { get; set; }
    }
}

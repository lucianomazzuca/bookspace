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
        [Range(0, 5)]
        public int Rating { get; set; }
        public IFormFile Image { get; set; }
        public int? AuthorId { get; set; }
        public List<int> GenresIds { get; set; } = new List<int>();
    }
}

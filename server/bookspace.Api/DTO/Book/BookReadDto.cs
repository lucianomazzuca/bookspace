
using bookspace.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookspace.Api.DTO.Book
{
    public class BookReadDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public string Image { get; set; }
        public Entities.Author Author { get; set; }
        public ICollection<Genre> Genres { get; set; }
    }
}

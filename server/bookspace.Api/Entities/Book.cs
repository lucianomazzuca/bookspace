using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bookspace.Api.Entities
{
    public class Book : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public string Image { get; set; }
        public int? AuthorId { get; set; }

        public Author Author { get; set; }
        public ICollection<Genre> Genres { get; set; } = new List<Genre>();
    }
}

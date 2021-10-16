using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bookspace.Api.DTO.Author
{
    public class AuthorCreateDto
    {
        [Required]
        public string Name { get; set; }
    }
}

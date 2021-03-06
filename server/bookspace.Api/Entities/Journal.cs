using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bookspace.Api.Entities
{
    public class Journal : BaseEntity
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        public int StatusId { get; set; }

        public Book Book { get; set; }
        public Status Status { get; set; }
    }
}

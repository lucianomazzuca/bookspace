using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookspace.Api.Entities
{
    public class Genre : BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}

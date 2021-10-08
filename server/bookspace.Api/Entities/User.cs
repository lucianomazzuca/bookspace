using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bookspace.Api.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
    }
}

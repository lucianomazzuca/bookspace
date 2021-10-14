using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bookspace.Api.DTO.User
{
    public class UserRegisterDTO
    {
        [Required]
        public string Name { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [StringLength(30, MinimumLength = 8)]
        [Required]
        public string Password { get; set; }
        public IFormFile Image { get; set; }
        public int RoleId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bookspace.Api.Entities
{
    public abstract class BaseEntity
    {
        [Required]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [DefaultValue(false)]
#pragma warning disable IDE1006 // Naming Styles
        public bool isDeleted { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}

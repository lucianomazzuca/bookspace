using bookspace.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookspace.Api.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
               .HasData(
                   new Role { Id = 1, Name = "Administrator"},
                   new Role { Id = 2, Name = "Standard"}
            );

            modelBuilder.Entity<Status>()
               .HasData(
                   new Status { Id = 1, Name = "Reading" },
                   new Status { Id = 2, Name = "Plan to read"},
                   new Status { Id = 2, Name = "Completed" }
            );
        }
    }
}

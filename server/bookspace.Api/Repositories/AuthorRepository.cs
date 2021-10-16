using bookspace.Api.Data;
using bookspace.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookspace.Api.Repositories
{
    public class AuthorRepository : BaseRepository<Author>
    {
        public AuthorRepository(BookspaceContext context) : base(context)
        {
        }
    }
}

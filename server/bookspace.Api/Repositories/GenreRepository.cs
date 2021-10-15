using bookspace.Api.Data;
using bookspace.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookspace.Api.Repositories
{
    public class GenreRepository : BaseRepository<Genre>
    {
        public GenreRepository(BookspaceContext context) : base(context)
        {
        }
    }
}

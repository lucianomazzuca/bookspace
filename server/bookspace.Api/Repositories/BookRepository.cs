using bookspace.Api.Data;
using bookspace.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookspace.Api.Repositories
{
    public class BaseRepository : BaseRepository<Book>
    {
        public BaseRepository(BookspaceContext context) : base(context)
        {
        }

        public override async Task<Book> GetById(int id)
        {
            var item = await _model
                .Where(x => !x.isDeleted)
                .AsNoTracking()
                .Include(x => x.Genres)
                .Include(x => x.Author)
                .FirstOrDefaultAsync(x => x.Id == id);

            return item;
        }
    }
}

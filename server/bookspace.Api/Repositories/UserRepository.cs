using bookspace.Api.Data;
using bookspace.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookspace.Api.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(BookspaceContext context) : base(context)
        {
        }

        public override async Task<User> GetById(int id)
        {
            var item = await _model
                .Where(x => !x.isDeleted)
                .AsNoTracking()
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Id == id);

            return item;
        }

        public async Task<User> GetByEmail(string email)
        {
            var user = await _model.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);

            return user;
        }
    }
}

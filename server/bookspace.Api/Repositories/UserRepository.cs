using bookspace.Api.Data;
using bookspace.Api.Entities;
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

        public async Task<User> GetByEmail(string email)
        {
            
        }
    }
}

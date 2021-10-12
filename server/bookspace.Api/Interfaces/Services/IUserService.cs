﻿using bookspace.Api.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bookspace.Api.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
        Task Insert(User user);
    }
}
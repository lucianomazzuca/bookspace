using bookspace.Api.Entities;
using bookspace.Api.Exceptions;
using bookspace.Api.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookspace.Api.Services
{
    public class UserService : IUserService
    {
        private readonly UnitOfWork _unitOfWork;

        public UserService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var items = await _unitOfWork.UserRepository.GetAll();
            return items;
        }

        public async Task Insert(User user)
        {
            var userExists = await _unitOfWork.UserRepository.GetByEmail(user.Email);
            if (userExists != null)
            {
                throw new UserAlreadyExistsException("This email is already in use");
            }

            await _unitOfWork.UserRepository.Insert(user);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}

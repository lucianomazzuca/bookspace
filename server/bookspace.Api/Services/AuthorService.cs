using bookspace.Api.Entities;
using bookspace.Api.Exceptions;
using bookspace.Api.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookspace.Api.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly UnitOfWork _unitOfWork;

        public AuthorService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Author>> GetAll()
        {
            var authors = await _unitOfWork.AuthorRepository.GetAll();
            return authors;
        }

        public async Task<Author> GetById(int id)
        {
            var author = await _unitOfWork.AuthorRepository.GetById(id);
            return author;
        }

        public async Task Insert(Author author)
        {
            await _unitOfWork.AuthorRepository.Insert(author);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Update(Author author)
        {
            _unitOfWork.AuthorRepository.Update(author);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var author = await _unitOfWork.AuthorRepository.GetById(id);
            if (author == null)
            {
                throw new RecordNotFoundException();
            }

            _unitOfWork.AuthorRepository.SoftDelete(author);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}

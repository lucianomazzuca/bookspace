using bookspace.Api.Entities;
using bookspace.Api.Repositories;
using bookspace.Api.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookspace.Api.Services
{
    public class BookService
    {
        private readonly UnitOfWork _unitOfWork;

        public BookService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            var items = await _unitOfWork.BookRepository.GetAll();
            return items;
        }
    }
}

using bookspace.Api.Entities;
using bookspace.Api.Exceptions;
using bookspace.Api.Repositories;
using bookspace.Api.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookspace.Api.Services
{
    public class BookService : IBookService
    {
        private readonly UnitOfWork _unitOfWork;

        public BookService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Pagination<Book>> GetAll(PaginationFilter paginationFilter)
        {
            var items = await _unitOfWork.BookRepository.GetAll(paginationFilter);
            var pagination = new Pagination<Book>(items, items.Count, paginationFilter.PageNumber, paginationFilter.PageSize);
            return pagination;
        }

        public async Task Insert(Book book, List<int> genresIds)
        {
            if (book.AuthorId != null)
            {
                await this.CheckAuthor((int)book.AuthorId);
            }

            book.Genres.Clear();

            if (genresIds.Count > 0)
            {
                await this.AddGenres(book, genresIds);
            }

            await _unitOfWork.BookRepository.Insert(book);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Update(Book book, List<int> genresIds)
        {
            if (book.AuthorId != null)
            {
                await this.CheckAuthor((int)book.AuthorId);
            }

            book.Genres.Clear();

            if (genresIds.Count > 0)
            {
                await this.AddGenres(book, genresIds);
            }

            _unitOfWork.BookRepository.Update(book);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task SoftDelete(int id)
        {
            var book = await _unitOfWork.BookRepository.GetById(id);
            if (book == null)
            {
                throw new RecordNotFoundException();
            }

            _unitOfWork.BookRepository.SoftDelete(book);
            await _unitOfWork.SaveChangesAsync();
        }

        private async Task CheckAuthor(int id)
        {
            var authorExists = await _unitOfWork.AuthorRepository.GetById(id);
            if (authorExists == null)
            {
                throw new RecordNotFoundException($"Author with id {id} does not exist");
            }
        }

        private async Task AddGenres(Book book, List<int> genresIds)
        {
            foreach(int id in genresIds)
            {
                var genre = await _unitOfWork.GenreRepository.GetTracked(id);
                if (genre != null)
                {
                    book.Genres.Add(genre);
                }
            }
        }
    }
}

using bookspace.Api.Entities;
using bookspace.Api.Exceptions;
using bookspace.Api.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookspace.Api.Services
{
    public class GenreService : IGenreService
    {
        private readonly UnitOfWork _unitOfWork;

        public GenreService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            var genres = await _unitOfWork.GenreRepository.GetAll();
            return genres;
        }

        public async Task<Genre> GetById(int id)
        {
            var genre = await _unitOfWork.GenreRepository.GetById(id);
            return genre;
        }

        public async Task Insert(Genre genre)
        {
            await _unitOfWork.GenreRepository.Insert(genre);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Update(Genre genre)
        {
            _unitOfWork.GenreRepository.Update(genre);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var genre = await _unitOfWork.GenreRepository.GetById(id);
            if (genre == null)
            {
                throw new RecordNotFoundException();
            }

            _unitOfWork.GenreRepository.SoftDelete(genre);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}

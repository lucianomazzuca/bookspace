using bookspace.Api.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bookspace.Api.Services
{
    public interface IGenreService
    {
        Task Delete(int id);
        Task<IEnumerable<Genre>> GetAll();
        Task<Genre> GetById(int id);
        Task Insert(Genre genre);
        Task Update(Genre genre);
    }
}
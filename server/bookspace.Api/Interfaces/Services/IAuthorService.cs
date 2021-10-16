using bookspace.Api.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bookspace.Api.Services
{
    public interface IAuthorService
    {
        Task Delete(int id);
        Task<IEnumerable<Author>> GetAll();
        Task<Author> GetById(int id);
        Task Insert(Author author);
        Task Update(Author author);
    }
}
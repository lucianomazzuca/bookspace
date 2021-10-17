using bookspace.Api.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bookspace.Api.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAll();
        Task Insert(Book book, List<int> genresIds);
        Task SoftDelete(int id);
        Task Update(Book book, List<int> genresIds);
    }
}
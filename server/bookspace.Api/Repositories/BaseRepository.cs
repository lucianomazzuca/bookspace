using bookspace.Api.Data;
using bookspace.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookspace.Api.Repositories
{
    public class BaseRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly BookspaceContext _context;
        protected readonly DbSet<TEntity> _model;

        public BaseRepository(BookspaceContext context)
        {
            _context = context;
            _model = context.Set<TEntity>();
        }

        public async Task<TEntity> GetById(int id)
        {
            var item = await _model
                .Where(x => !x.isDeleted)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return item;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _model.Where(x => !x.isDeleted).ToListAsync();
        }

        public async Task Insert(TEntity entity)
        {
            await _model.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(TEntity entity)
        {
            entity.UpdatedAt = DateTime.Now;
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task SoftDelete(TEntity entity)
        {
            entity.isDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(TEntity entity)
        {
            _model.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}

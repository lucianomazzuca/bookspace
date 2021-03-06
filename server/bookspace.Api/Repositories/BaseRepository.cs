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

        public virtual async Task<TEntity> GetById(int id)
        {
            var item = await _model
                .Where(x => !x.isDeleted)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return item;
        }

        public virtual async Task<TEntity> GetTracked(int id)
        {
            var item = await _model
                .Where(x => !x.isDeleted)
                .FirstOrDefaultAsync(x => x.Id == id);

            return item;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _model.Where(x => !x.isDeleted).AsNoTracking().ToListAsync();
        }

        public virtual async Task Insert(TEntity entity)
        {
            await _model.AddAsync(entity);
        }

        public virtual void Update(TEntity entity)
        {
            entity.UpdatedAt = DateTime.Now;
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void SoftDelete(TEntity entity)
        {
            entity.isDeleted = true;
        }

        public void Delete(TEntity entity)
        {
            _model.Remove(entity);
        }
    }
}

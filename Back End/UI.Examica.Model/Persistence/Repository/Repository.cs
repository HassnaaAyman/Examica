using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UI.Examica.Model.Core.Repository;

namespace UI.Examica.Model.Persistence.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext context;
        protected readonly DbSet<TEntity> entities;
        public Repository(DbContext _context)
        {
            context = _context;
            entities = _context.Set<TEntity>();
        }
        public async Task Add(TEntity entity)
        {
            await entities.AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<TEntity> _entities)
        {
            await entities.AddRangeAsync(_entities);
        }

        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await entities.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await entities.ToListAsync();
        }

        public async Task<TEntity> GetById(params object[] id)
        {
            return await entities.FindAsync(id);
        }

        public void Remove(TEntity entity)
        {
            entities.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> _entities)
        {
            entities.RemoveRange(_entities);
        }

        public async Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return await entities.FirstOrDefaultAsync(predicate);
        }

        public void Update(TEntity entity)
        {
            entities.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}

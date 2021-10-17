using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EventsApp.DataEF
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {

        #region Protected 

        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        #endregion

        #region Ctor

        public BaseRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        #endregion

        #region Properties

        public IQueryable<T> Table
        {
            get { return _dbSet; }
        }


        #endregion

        #region Methods

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetAsync(params object[] key)
        {
            return await _dbSet.FindAsync(key);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(params object[] key)
        {
            var entity = await GetAsync(key);
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T updated)
        {
            if (updated == null)
                return;

            _dbSet.Update(updated);

            await _context.SaveChangesAsync();
        }

        public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.AnyAsync(predicate);
        }

        public void SetState(T entity, EntityState state)
        {
            _context.Entry(entity).State = state;
        }

        #endregion
    }
}

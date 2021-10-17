using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;

namespace EventsApp.DataEF
{
    public interface IBaseRepository<T> where T : class
    {
        #region Methods
        Task<List<T>> GetAllAsync();

        Task<T> GetAsync(params object[] key);

        Task AddAsync(T entity);

        Task RemoveAsync(T entity);

        Task RemoveAsync(params object[] key);

        Task UpdateAsync(T updated);

        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        void SetState(T entity, EntityState state);
        #endregion

        #region Sets
        IQueryable<T> Table { get; }

        #endregion
    }
}

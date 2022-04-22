using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.DataResults.Abstract;
using Core.DataResults.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Core.EFRepository.Abstract
{
    public interface IRepository<T> where T : class
    {
        Task<DataResult<T>> AddAsync(T entity);
        Task<DataResult<List<T>>> AddRangeAsync(List<T> entites);
        Task<DataResult<T>> DeleteByIdAsync(Guid id);
        Task<DataResult<T>> DeleteAsync(T entity);
        Task<DataResult<List<T>>> DeleteAsync(List<T> entities);
        Task<DataResult<T>> GetByIdAsync(Guid id);
        Task<DataResult<IQueryable<T>>> GetAllAsync();
        Task<DataResult<IQueryable<T>>> GetAllAsync(Func<T, bool>? predicate);
        Task<DataResult<T>> Get(Func<T, bool>? predicate);
        Task<DataResult<IQueryable<T>>> Take(int count);
        Task<DataResult<int>> SaveChangesAsync();
        Task<DataResult<IQueryable<T>>> AsNoTrackingAsync(Func<T,bool> predicate);
        Task<DataResult<IQueryable<T>>> AsNoTrackingAsync();
        Task<DataResult<IQueryable<T>>> Where(Func<T,bool> predicate);
        Task<DataResult<List<T>>> ToListAsync();
        Task<DataResult<List<T>>> ToListAsync(Func<T,bool> predicate);
        Task<DataResult<int>> Update(T entity);
        Task<DataResult<int>> UpdateRange(List<T> entities);
        Task<DataResult<IQueryable<T>>> GetAllLazyLoad(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] children);
        IEnumerable<T> Get(
        Expression<Func<T, bool>> filter = null, 
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
        string includeProperties = "");
    }

}

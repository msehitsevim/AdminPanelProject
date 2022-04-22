using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.DataResults.Abstract;
using Core.DataResults.Concrete;
using Core.EFRepository.Abstract;
using Microsoft.EntityFrameworkCore;
namespace Core.EFRepository.Concrete
{
    public class EFRepository<T, TContext> : IRepository<T> where T : class, new()
    where TContext : DbContext, new()
    {
        private readonly TContext _context;
        private readonly DbSet<T> _dbSet;// = new TContext().Set<T>();
        public EFRepository()
        {
            // AsNoTrackingList join için...
            // Tüm crud operasyonlar ASYNC olcak
            _context = new TContext();
            _dbSet = _context.Set<T>();
        }
        public async Task<DataResult<T>> AddAsync(T entity)
        {
            DataResult<T> result = new DataResult<T>();
            try
            {
                //var context = new TContext();
                //var dbSet = context.Set<T>();
                if (entity != null)
                {
                    // var added = _context.Entry(entity);
                    //added.State = EntityState.Added;
                    await Task.Run(() =>
                    {
                        _dbSet.AddAsync(entity);
                        _context.Entry(entity).State = EntityState.Added;
                        result.Result = entity;
                        result.Success = true;
                    });
                }
                return result;
            }

            catch (Exception ex)
            {
                result.Result = null;
                result.Success = false;
                result.Error = ex;
                return result;
            }

            //await context.SaveChangesAsync();
            //await _context.SaveChangesAsync();
        }

        public async Task<DataResult<List<T>>> AddRangeAsync(List<T> entites)
        {
            DataResult<List<T>> result = new DataResult<List<T>>();
            try
            {
                if (entites.Count > 0)
                {
                    //await _dbSet.AddRangeAsync(entites);
                    await Task.Run(() =>
                    {
                        //await _dbSet.AddRangeAsync(entites);
                        _dbSet.AddRangeAsync(entites);
                        entites.ForEach(e =>
                            _context.Entry(e).State = EntityState.Added
                        );
                    });
                    result.Result = entites;
                    result.Success = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Result = entites;
                result.Error = ex;
                return result;
            }
        }
        public async Task<DataResult<T>> DeleteByIdAsync(Guid id)
        {
            //var deletedObject = await context.Set<T>().FindAsync(id);
            DataResult<T> result = new DataResult<T>();
            var deletedObject = await _dbSet.FindAsync(id);
            try
            {
                if (deletedObject != null)
                {
                    /*var deleted = _context.Entry(deletedObject);
                    deleted.State = EntityState.Deleted;*/
                    await Task.Run(() =>
                    {
                        _dbSet.Remove(deletedObject);
                        _context.Entry(deletedObject).State = EntityState.Deleted;
                    });
                    //_dbSet.Remove(deletedObject)
                    result.Result = deletedObject;
                    result.Success = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                result.Result = deletedObject;
                result.Success = false;
                result.Error = ex;
                return result;
            }
            //_dbSet.Remove(deletedObject);
            //context.Set<T>().Remove(deletedObject);
            //context.SaveChanges();
        }

        public async Task<DataResult<T>> DeleteAsync(T entity)
        {
            DataResult<T> result = new DataResult<T>();
            try
            {
                if (entity != null)
                {
                    await Task.Run(() =>
                    {
                        _dbSet.Remove(entity);
                        _context.Entry(entity).State = EntityState.Deleted;
                    });
                    //_dbSet.Remove(entity)
                    result.Result = entity;
                    result.Success = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                result.Result = entity;
                result.Success = false;
                result.Error = ex;
                return result;
            }
            //_dbSet.Remove(entity);
            //context.Set<T>().Remove(entity);
            //await context.SaveChangesAsync();
        }

        public async Task<DataResult<List<T>>> DeleteAsync(List<T> entities)
        {
            //var attach = _context.Attach(entities);
            DataResult<List<T>> result = new DataResult<List<T>>();
            try
            {
                if (entities.Count > 0)
                {
                    // _context.Entry(entities).State = EntityState.Deleted;
                    await Task.Run(() =>
                    {
                        _dbSet.RemoveRange(entities);
                        entities.ForEach(deleted =>
                        {
                            _context.Entry(deleted).State = EntityState.Deleted;
                        });
                    });

                    /*entities.ToList().ForEach(e =>
                    {
                        _context.Entry(e).State = EntityState.Deleted;
                    });*/
                    //var deleted = _context.Entry<List<T>>(entities);
                    //deleted.State = EntityState.Deleted;
                    // _dbSet.RemoveRange(entities);
                    result.Result = entities;
                    result.Success = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                result.Result = entities;
                result.Success = false;
                result.Error = ex;
                return result;
            }

            //_dbSet.RemoveRange(entities);
            //context.SaveChanges();
        }

        public async Task<DataResult<IQueryable<T>>> GetAllAsync()
        {
            //Eksik var tamamla predicate'i Iqueryable çek // LazyLoading Kapat // İnclude'a bak
            DataResult<IQueryable<T>> result = new DataResult<IQueryable<T>>();
            //var context = new TContext();
            //var dbset = context.Set<T>();
            try
            {
                var context = new TContext();
                var dbSet = context.Set<T>();
                result.Result = await Task.Run<IQueryable<T>>(() => dbSet);
                result.Success = true;
                return result;
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Success = false;
                result.Error = ex;
                return result;
            }
        }

        public async Task<DataResult<T>> GetByIdAsync(Guid id)
        {
            DataResult<T> result = new DataResult<T>();
            var findingObject = await _dbSet.FindAsync(id);
            try
            {
                if (findingObject != null)
                    result.Result = findingObject;
                else
                    result.Result = null;
                result.Success = true;
                return result;
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Success = false;
                result.Error = ex;
                return result;
            }
        }

        public async Task<DataResult<int>> SaveChangesAsync()
        {
            DataResult<int> result = new DataResult<int>();
            try
            {
                var changes = await _context.SaveChangesAsync();
                result.Result = changes;
                result.Success = true;
                return result;
            }
            catch (Exception ex)
            {
                //result.Result = changes;
                result.Success = false;
                result.Error = ex;
                return result;
            }

            /* using(var context = new TContext())
             {
                 context.SaveChanges();
             }*/
        }
        public async Task<DataResult<T>> Get(Func<T, bool>? predicate)
        {
            DataResult<T> result = new DataResult<T>();
            try
            {
                var context = new TContext();
                var dbSet = context.Set<T>();
                if (predicate != null)
                    result.Result = await Task.Run<T>(() => dbSet.Where(predicate).DefaultIfEmpty().FirstOrDefault());
                else
                    result.Result = null;
                result.Success = true;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Result = null;
                result.Error = ex;
                return result;
            }
        }

        public async Task<DataResult<IQueryable<T>>> GetAllAsync(Func<T, bool>? predicate)
        {
            //var context = new TContext();
            //var dbSet = context.Set<T>();
            DataResult<IQueryable<T>> result = new DataResult<IQueryable<T>>();
            try
            {
                var context = new TContext();
                var dbSet = context.Set<T>();
                result.Result = await Task.Run<IQueryable<T>>(() =>
               //_dbSet.Where(predicate).AsQueryable());
               dbSet.Where(predicate).AsQueryable());
                result.Success = true;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Error = ex;
                result.Result = null;
                return result;
            }
            //return _dbSet.Where(predicate).AsQueryable();
        }

        public async Task<DataResult<IQueryable<T>>> Take(int count)
        {
            DataResult<IQueryable<T>> result = new DataResult<IQueryable<T>>();
            try
            {
                var context = new TContext();
                var dbSet = context.Set<T>();
                result.Result = await Task.Run<IQueryable<T>>(() =>
                   dbSet.Take(count));
                result.Success = true;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Error = ex;
                result.Result = null;
                return result;
            }

        }

        public async Task<DataResult<IQueryable<T>>> Where(Func<T, bool> predicate)
        {
            DataResult<IQueryable<T>> result = new DataResult<IQueryable<T>>();
            try
            {
                var context = new TContext();
                var dbSet = context.Set<T>();
                result.Result = await Task.Run<IQueryable<T>>(() =>
                    dbSet.Where(predicate).AsQueryable()
                );
                result.Success = true;
                return result;
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Error = ex;
                result.Success = false;
                return result;
            }
        }

        public async Task<DataResult<List<T>>> ToListAsync()
        {
            DataResult<List<T>> result = new DataResult<List<T>>();
            try
            {
                var context = new TContext();
                var dbSet = context.Set<T>();
                result.Result = await Task.Run<List<T>>(() => dbSet.ToListAsync());
                result.Success = true;
                result.Error = null;
                return result;
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Success = false;
                result.Error = ex;
                return result;
            }
        }

        public async Task<DataResult<List<T>>> ToListAsync(Func<T, bool> predicate)
        {
            // includes prop ekle foreach ile dön...
            DataResult<List<T>> result = new DataResult<List<T>>();
            try
            {
                var context = new TContext();
                var dbSet = context.Set<T>();
                var res = dbSet.Where(predicate);
                result.Result = await Task.Run<List<T>>(() =>
                    res.ToList()
                );
                result.Success = true;
                result.Error = null;
                return result;
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Success = false;
                result.Error = ex;
                return result;
            }
        }

        public async Task<DataResult<int>> Update(T entity)
        {
            DataResult<int> result = new DataResult<int>();
            try
            {
                if (entity != null)
                {
                    await Task.Run(() =>
                    {
                        //_dbSet.Attach(entity).State = EntityState.Modified;
                        _context.Entry(entity).State = EntityState.Modified;
                        // _dbSet.Attach(entity);
                        //var entry = _context.Entry(entity);
                        //entry.State = EntityState.Modified;
                    });
                }
                result.Result = 1;
                result.Success = true;
                return result;
            }
            catch (Exception ex)
            {
                result.Result = 0;
                result.Success = false;
                result.Error = ex;
                return result;
            }
        }

        public async Task<DataResult<IQueryable<T>>> GetAllLazyLoad(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] children)
        {
            DataResult<IQueryable<T>> result = new DataResult<IQueryable<T>>();
            try
            {
                result.Result = await Task.Run<IQueryable<T>>(() =>
                {
                    children.ToList().ForEach(x => _dbSet.Include(x).Load());
                    return _dbSet.AsNoTracking();
                });
                result.Success = true;
                return result;
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Error = ex;
                result.Success = false;
                return result;
            }
        }

        public virtual IEnumerable<T> Get(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }


            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public async Task<DataResult<int>> UpdateRange(List<T> entities)
        {
            DataResult<int> result = new DataResult<int>();
            try
            {
                if (entities != null)
                {
                    await Task.Run(() =>
                    {
                        entities.ForEach(data =>
                        {
                            _context.Entry(data).State = EntityState.Modified;
                        });

                    });
                }
                result.Result = 1;
                result.Success = true;
                return result;
            }
            catch (Exception ex)
            {
                result.Result = 0;
                result.Success = false;
                result.Error = ex;
                return result;
            }
        }

        public async Task<DataResult<IQueryable<T>>> AsNoTrackingAsync(Func<T, bool> predicate)
        {
            DataResult<IQueryable<T>> result = new DataResult<IQueryable<T>>();
            try
            {
                var context = new TContext();
                var dbSet = context.Set<T>();
                result.Result = await Task.Run<IQueryable<T>>(() =>
                {
                    return dbSet.AsTracking().Where(predicate).AsQueryable();
                });
                result.Success = true;
                return result;
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Success = false;
                result.Error = ex;
                return result;
            }
        }

        public async Task<DataResult<IQueryable<T>>> AsNoTrackingAsync()
        {
            DataResult<IQueryable<T>> result = new DataResult<IQueryable<T>>();
            try
            {
                var context = new TContext();
                var dbSet = context.Set<T>();
                result.Result = await Task.Run<IQueryable<T>>(() =>
                {
                    return dbSet.AsNoTracking();
                });
                result.Success = true;
                return result;
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Success = false;
                result.Error = ex;
                return result;
            }
        }
    }

}

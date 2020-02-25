using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace api.Data.Infrastructure
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        #region Properties
        private EmployeeQualificationContext _EmployeeQualificationContext;
        private readonly DbSet<T> dbSet;
        #endregion

        protected RepositoryBase(EmployeeQualificationContext EmployeeQualificationContext)
        {
            _EmployeeQualificationContext = EmployeeQualificationContext;
            dbSet = _EmployeeQualificationContext.Set<T>();
        }

        #region Implementation
        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            _EmployeeQualificationContext.Attach(entity);
            _EmployeeQualificationContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            _EmployeeQualificationContext.Remove(entity);
        }

        public virtual void DeleteMulti(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbSet.Remove(obj);
        }

        public async ValueTask<T> GetSingleById(int id)
        {
            var items = await dbSet.FindAsync(id);
            return items;
        }

        public async ValueTask<T> GetSingleById(string id)
        {
            var items = await dbSet.FindAsync(id);
            return items;
        }

        public async Task<int> Count()
        {
            var count = await _EmployeeQualificationContext.Set<T>().CountAsync();
            return count;
        }

        public async Task<List<T>> GetAll(string[] includes = null)
        {
            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = _EmployeeQualificationContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                var buildQuery = await query.AsQueryable().ToListAsync();
                return buildQuery;
            }

            var buildQ = await _EmployeeQualificationContext.Set<T>().AsQueryable().ToListAsync();
            return buildQ;
        }

        public async Task<T> GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null)
        {
            if (includes != null && includes.Count() > 0)
            {
                var query = _EmployeeQualificationContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                var buildQuery = await query.FirstOrDefaultAsync(expression);
                return buildQuery;
            }

            var buildQ = await _EmployeeQualificationContext.Set<T>().FirstOrDefaultAsync(expression);
            return buildQ;
        }

        public async Task<List<T>> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null)
        {
            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = _EmployeeQualificationContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                var buildQuery = await query.Where<T>(predicate).AsQueryable<T>().ToListAsync();
                return buildQuery;
            }

            var buildQ = await _EmployeeQualificationContext.Set<T>().Where<T>(predicate).AsQueryable<T>().ToListAsync();
            return buildQ;
        }

        public async Task<List<T>> GetMultiPaging(Expression<Func<T, bool>> predicate, int index = 0, int size = 20, string[] includes = null)
        {
            int skipCount = index * size;
            IQueryable<T> _resetSet;

            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = _EmployeeQualificationContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                _resetSet = predicate != null ? query.Where<T>(predicate).AsQueryable() : query.AsQueryable();
            }
            else
            {
                _resetSet = predicate != null ? _EmployeeQualificationContext.Set<T>().Where<T>(predicate).AsQueryable() : _EmployeeQualificationContext.Set<T>().AsQueryable();
            }

            _resetSet = skipCount == 0 ? _resetSet.Take(size) : _resetSet.Skip(skipCount).Take(size);
            var total = await _resetSet.CountAsync();
            var items = await _resetSet.AsQueryable().ToListAsync();
            return items;
        }

        public async Task<bool> CheckContains(Expression<Func<T, bool>> predicate)
        {
            int count = await _EmployeeQualificationContext.Set<T>().CountAsync<T>(predicate);
            return count > 0;
        }
        #endregion
    }
}
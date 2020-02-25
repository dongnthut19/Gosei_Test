using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace api.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
         // Marks an entity as new
        void Add(T entity);

        // Marks an entity as modified
        void Update(T entity);

        // Marks an entity to be removed
        void Delete(T entity);

        //Delete multi records
        void DeleteMulti(Expression<Func<T, bool>> where);

        // Get an entity by int id
        ValueTask<T> GetSingleById(int id);

        ValueTask<T> GetSingleById(string id);

        Task<T> GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null);

        Task<List<T>> GetAll(string[] includes = null);

        Task<List<T>> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null);

        Task<List<T>> GetMultiPaging(Expression<Func<T, bool>> filter, int index = 0, int size = 50, string[] includes = null);

        Task<List<T>> GetMultiSortingPaging(
            Expression<Func<T, bool>> whereClause = null,
            IOrderByClause<T>[] orderBy = null,
            int skip = 0,
            int top = 0,
            string[] includes = null);

        Task<int> Count();

        Task<bool> CheckContains(Expression<Func<T, bool>> predicate);
    }
}
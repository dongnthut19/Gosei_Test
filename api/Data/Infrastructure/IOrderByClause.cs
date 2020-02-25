using System.Linq;

namespace api.Data.Infrastructure
{
    public interface IOrderByClause<T>where T : class
    {
        IOrderedQueryable<T> ApplySort(IQueryable<T> query, bool firstSort);
    }
}
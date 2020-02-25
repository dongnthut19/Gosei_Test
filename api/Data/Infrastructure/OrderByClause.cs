using System;
using System.Linq;
using System.Linq.Expressions;
using api.Models.Enums;

namespace api.Data.Infrastructure
{
    public class OrderByClause<T, TProperty> : IOrderByClause<T>where T : class
  {
    private OrderByClause()
    {
    }

    public OrderByClause(
      Expression<Func<T, TProperty>> orderBy,
      SortDirection sortDirection = SortDirection.Ascending)
        {
            OrderBy = orderBy;
            SortDirection = sortDirection;
        }

 

    /// <summary>
    /// Order by expression
    /// </summary>
    private Expression<Func<T, TProperty>> OrderBy { get; set; }


    /// <summary>
    /// Sort direction
    /// </summary>
    private SortDirection SortDirection { get; set; }

 
    public IOrderedQueryable<T> ApplySort(IQueryable<T> query, bool firstSort)
    {
      if (SortDirection == SortDirection.Ascending)
      {
        if (firstSort)
        {
          return query.OrderBy(OrderBy);
        }
        else
        {
          return ((IOrderedQueryable<T>)query).ThenBy(OrderBy);
        }
      }
      else
      {
        if (firstSort)
        {
          return query.OrderByDescending(OrderBy);
        }
        else
        {
          return ((IOrderedQueryable<T>)query).ThenByDescending(OrderBy);
        }
      }
    }
  }
}
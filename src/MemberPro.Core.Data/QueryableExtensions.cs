using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MemberPro.Core.Data
{
    public static class QueryableExtensions
    {
        public static IQueryable<TEntity> WhereIf<TEntity>(this IQueryable<TEntity> queryable, bool condition,
            Expression<Func<TEntity, bool>> predicate)
        {
            if (condition)
            {
                return queryable.Where(predicate);
            }

            return queryable;
       }
    }
}


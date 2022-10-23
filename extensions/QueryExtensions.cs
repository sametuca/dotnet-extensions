using Marti.Data.ComplexTypes;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Marti.Core.Extensions;

public static class QueryableExtensions
{
    public static string ToSql<TEntity>(this IQueryable<TEntity> query) where TEntity : class
    {
        return query.ToQueryString();
    }

    public static IQueryable<T> DTOrderByDynamic<T>(
        this IQueryable<T> query,
        string orderByMember,
        DTOrderDir direction)
    {
        var queryElementTypeParam = Expression.Parameter(typeof(T));

        var memberAccess = Expression.PropertyOrField(queryElementTypeParam, orderByMember);

        var keySelector = Expression.Lambda(memberAccess, queryElementTypeParam);

        var orderBy = Expression.Call(
            typeof(Queryable),
            direction == DTOrderDir.ASC ? "OrderBy" : "OrderByDescending",
            new[] { typeof(T), memberAccess.Type },
            query.Expression,
            Expression.Quote(keySelector));

        return query.Provider.CreateQuery<T>(orderBy);
    }
}
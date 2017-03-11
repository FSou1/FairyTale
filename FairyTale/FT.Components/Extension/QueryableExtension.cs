using System;
using System.Linq;
using System.Linq.Expressions;

namespace FT.Components.Extension {
    public static class QueryableExtension {
        public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, bool asc) {
            return asc ? source.OrderBy(keySelector) : source.OrderByDescending(keySelector);
        }
    }
}
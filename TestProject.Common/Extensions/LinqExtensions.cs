using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace TestProject.Common.Extensions
{
    public static class LinqExtensions
    {
        public static IQueryable<T> Sort<T>(this IQueryable<T> source, string sortBy)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (string.IsNullOrEmpty(sortBy))
                throw new ArgumentNullException("sortBy");

            source = source.OrderBy(sortBy);

            return source;
        }

        public static IQueryable<T> SortDesc<T>(this IQueryable<T> source, string sortBy)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (string.IsNullOrEmpty(sortBy))
                throw new ArgumentNullException("sortBy");

            source = source.OrderBy($"{sortBy} DESC");

            return source;
        }
    }
}
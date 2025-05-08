using System.Linq;
using System.Linq.Dynamic.Core;

using Cracker.Admin.Core;

namespace Cracker.Admin.Extensions
{
    public static class QueryableExtension
    {
        public static IQueryable<T> StartPage<T>(this IQueryable<T> queryable, IPage search)
        {
            return queryable.Skip((search.Current - 1) * search.PageSize).Take(search.PageSize);
        }
    }
}
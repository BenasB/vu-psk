using Microsoft.EntityFrameworkCore;

namespace Recipes.DataAccess.Filtering;

public static class PaginationExtensions
{
    public const int PageSize = 100;

    public static async Task<PaginatedList<T>> Paginate<T>(this IQueryable<T> queryable, int? top, int? skip)
    {
        var take = top ?? PageSize;
        var offset = skip ?? 0;

        return new PaginatedList<T>
        {
            Items = await queryable.Skip(offset).Take(take).ToListAsync(),
            IsLastPage = take + offset >= queryable.Count(),
        };
    }
}

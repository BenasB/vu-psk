namespace Recipes.DataAccess.Filtering;

public static class PaginationExtensions
{
    public const int PageSize = 100;

    public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, int? top, int? skip)
    {
        var take = top ?? PageSize;
        var offset = skip ?? 0;
        return queryable.Skip(offset).Take(take);
    }
}

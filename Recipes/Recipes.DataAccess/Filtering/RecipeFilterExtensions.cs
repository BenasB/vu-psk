using Recipes.DataAccess.Entities;

namespace Recipes.DataAccess.Filtering;

public static class RecipeFilterExtensions
{
    public static IQueryable<RecipeEntity> FilterByTitle(this IQueryable<RecipeEntity> queryable, string? title) 
        => !string.IsNullOrEmpty(title) ? queryable.Where(r => r.Title.ToLower().Contains(title.ToLower())) : queryable;

    public static IQueryable<RecipeEntity> FilterByTags(this IQueryable<RecipeEntity> queryable, IList<long>? tagIds)
        => tagIds != null && tagIds.Any() ? queryable.Where(r => r.Tags.Any(tag => tagIds.Contains(tag.TagId))) : queryable;

    public static IQueryable<RecipeEntity> FilterByAuthor(this IQueryable<RecipeEntity> queryable, long? authorId)
        => authorId.HasValue ? queryable.Where(s => s.AuthorId == authorId) : queryable;
}

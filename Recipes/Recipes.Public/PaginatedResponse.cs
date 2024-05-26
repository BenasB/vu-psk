namespace Recipes.Public;

public class PaginatedResponse<T>
{
    public IEnumerable<T> Items { get; init; } = new List<T>();
    public bool IsLastPage { get; init; }
}
namespace Recipes.DataAccess.Filtering;

public class PaginatedList<T>
{
    public required IList<T> Items { get; set; }
    public bool IsLastPage { get; set; }
}

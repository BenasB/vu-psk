using Recipes.DataAccess.Entities;
using Recipes.DataAccess.Filtering;
using Recipes.Public;

namespace Recipes.Business.Services;

public static class MappingService
{
    public static RecipeEntity UpdateOldRecipeEntity(RecipeEntity oldRecipeEntity, RecipeUpdateDTO recipeDTO)
    {
        oldRecipeEntity.Title = recipeDTO.Title;
        oldRecipeEntity.Description = recipeDTO.Description;
        oldRecipeEntity.CookingTime = recipeDTO.CookingTime;
        oldRecipeEntity.Servings = recipeDTO.Servings;
        oldRecipeEntity.Ingredients = recipeDTO.Ingredients;
        oldRecipeEntity.Instructions = recipeDTO.Instructions;
        oldRecipeEntity.Image = recipeDTO.Image;
        oldRecipeEntity.RowVersion = recipeDTO.Version;

        return oldRecipeEntity;
    }

    public static RecipeEntity GetRecipeFromDTO(RecipeCreateDTO recipeDTO, int authorId)
    {
        return new RecipeEntity()
        {
            Title = recipeDTO.Title,
            AuthorId = authorId,
            Description = recipeDTO.Description,
            CookingTime = recipeDTO.CookingTime,
            Servings = recipeDTO.Servings,
            Ingredients = recipeDTO.Ingredients,
            Instructions = recipeDTO.Instructions,
            Image = recipeDTO.Image
        };
    }

    public static Recipe GetRecipeFromEntity(RecipeEntity recipeEntity)
    {
        return new Recipe
        {
            Id = recipeEntity.Id,
            Title = recipeEntity.Title,
            AuthorId = recipeEntity.AuthorId,
            Description = recipeEntity.Description,
            CookingTime = recipeEntity.CookingTime,
            Servings = recipeEntity.Servings,
            UpdatedAt = recipeEntity.UpdatedAt,
            Ingredients = recipeEntity.Ingredients,
            Instructions = recipeEntity.Instructions,
            Image = recipeEntity.Image,
            Tags = recipeEntity.Tags.Select(t => t.Tag).Select(GetTagFromEntity),
            Version = recipeEntity.RowVersion,
        };
    }

    public static Tag GetTagFromEntity(TagEntity tagEntity)
    {
        return new Tag
        {
            Id = tagEntity.Id,
            Name = tagEntity.Name,
        };
    }
    
    public static TagEntity GetTagFromDTO(TagCreateUpdateDTO tagDTO)
    {
        return new TagEntity
        {
            Name = tagDTO.Name,
        };
    }

    public static PaginatedResponse<T> GetPaginatedResponse<T, TEntity>(PaginatedList<TEntity> paginatedList, Func<TEntity, T> convert)
    {
        return new PaginatedResponse<T>
        {
            IsLastPage = paginatedList.IsLastPage,
            Items = paginatedList.Items.Select(convert),
        };
    }
}

namespace Recipes.API.Options;

public class FeatureToggles
{
    public const string SectionName = "FeatureToggles";

    public required bool AllowImplicitTagCreation { get; init; }
}

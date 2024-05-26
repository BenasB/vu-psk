using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Recipes.API.Middlewares;
using Recipes.API.Options;
using Recipes.Business.Services;
using Recipes.Business.Services.Interfaces;
using Recipes.DataAccess;
using Recipes.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
builder.Services.AddAuthorization();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
        In = ParameterLocation.Header, 
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey 
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        { 
            new OpenApiSecurityScheme 
            { 
                Reference = new OpenApiReference 
                { 
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer" 
                } 
            },
            new string[] { } 
        } 
    });
});

builder.Services.AddDbContext<RecipesDatabaseContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));


builder.Services.ConfigureOptions<ConfigureJwtBearerOptions>();
builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetRequiredSection(JwtOptions.SectionName));
builder.Services.Configure<FeatureToggles>(
    builder.Configuration.GetRequiredSection(FeatureToggles.SectionName));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IRecipesRepository, RecipesRepository>();

builder.Services.AddScoped<ITagsService, TagsService>();
builder.Services.AddScoped<IRecipesService, RecipesService>();
builder.Services.AddScoped<TagCreationService>();
builder.Services.AddScoped<TagValidationService>();
builder.Services.AddScoped<ITagValidationService>(serviceProvider =>
{
    var options = serviceProvider.GetRequiredService<IOptionsMonitor<FeatureToggles>>().CurrentValue;
    return options.AllowImplicitTagCreation
        ? serviceProvider.GetRequiredService<TagCreationService>()
        : serviceProvider.GetRequiredService<TagValidationService>();
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var recipesDbContext = scope.ServiceProvider.GetRequiredService<RecipesDatabaseContext>();
    if (app.Environment.IsDevelopment())
        recipesDbContext.Database.EnsureDeleted();

    recipesDbContext.Database.Migrate();

    if (app.Environment.IsDevelopment() && !recipesDbContext.Recipes.Any() && !recipesDbContext.Tags.Any())
        await DbInitializer.SeedDatabase(recipesDbContext);
}

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();

app.Run();
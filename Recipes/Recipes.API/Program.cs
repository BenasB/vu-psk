using Microsoft.EntityFrameworkCore;
using Recipes.API;
using Recipes.DataAccess;
using Recipes.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RecipesDatabaseContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

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

app.UseMiddleware<RequestResponseLoggingMiddleware>();
app.MapControllers();

app.Run();
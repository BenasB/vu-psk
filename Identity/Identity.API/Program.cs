using Identity.API.Models;
using Identity.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<IdentityDatabaseContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/users", async (IdentityDatabaseContext dbContext) =>
{
    var createdUser = new UserEntity() { Username = $"USER-{Guid.NewGuid()}", AssociatedRecipes = new List<int> { 69, 420 }, DateRegistered = DateTime.UtcNow };
    dbContext.Users.Add(createdUser);
    await dbContext.SaveChangesAsync();

    return dbContext.Users.Select(u => new
    {
        u.Id,
        u.Username,
        u.AssociatedRecipes,
        u.DateRegistered
    });
});

app.Run();
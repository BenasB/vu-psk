using Identity.DataAccess;
using Identity.DataAccess.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<IdentityDatabaseContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<IdentityDatabaseContext>().Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/users", async (IdentityDatabaseContext dbContext) =>
{
    return await dbContext.Users.Select(u => new
    {
        u.Id,
        u.Username,
        u.PasswordHash,
        u.Email,
        u.Roles
    }).ToListAsync();
});

app.MapPost("/users", async (IdentityDatabaseContext dbContext) =>
{
    var createdUser = new UserEntity()
    {
        Username = $"USER-{Guid.NewGuid()}",
        PasswordHash = Encoding.UTF8.GetBytes("abc"),
        Email = "abc@def.com",
        Roles = UserRole.Admin | UserRole.Member
    };

    dbContext.Users.Add(createdUser);
    await dbContext.SaveChangesAsync();

    return createdUser;
});

app.Run();
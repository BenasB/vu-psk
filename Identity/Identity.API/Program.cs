using Identity.Public;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/users", () =>
{
    var users = new User[]
    {
        new()
        {
            Username = "johndoe"
        }
    };
    
    return users;
});

app.Run();
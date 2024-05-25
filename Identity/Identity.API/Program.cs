using System.IdentityModel.Tokens.Jwt;

using Identity.DataAccess;
using Identity.API.Options;
using Identity.API.Helpers;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureOptions<ConfigureJwtBearerOptions>();
builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetRequiredSection(JwtOptions.SectionName));
builder.Services.AddSingleton<JwtGenerator, JwtGenerator>();

builder.Services.AddDbContext<IdentityDatabaseContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<IdentityDatabaseContext>().Database.Migrate();
}

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
using System.IdentityModel.Tokens.Jwt;
using System.Text;

using Identity.DataAccess;
using Identity.API.Options;
using Identity.API.Helpers;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();
builder.Services.AddAuthentication(x =>
{
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwt =>
{
    jwt.TokenValidationParameters = new TokenValidationParameters()
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT:Key").Value!)),
        ValidAudience = builder.Configuration.GetSection("JWT:Issuer").Value,
        ValidIssuer = builder.Configuration.GetSection("JWT:Issuer").Value,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddAuthorization(options =>
{
    var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
        JwtBearerDefaults.AuthenticationScheme);

    defaultAuthorizationPolicyBuilder =
        defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();

    options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JWTOptions>(
    builder.Configuration.GetSection(JWTOptions.JWT));
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
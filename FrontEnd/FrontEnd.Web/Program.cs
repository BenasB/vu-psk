using FrontEnd.Web.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using FrontEnd.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddFluentUIComponents();

builder.Services.AddHttpClient<IRecipeService, RecipeService>();
builder.Services.AddHttpClient<IIdentityService, IdentityService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
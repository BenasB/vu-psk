using BitzArt.Blazor.Auth;
using FrontEnd.Web.Auth;
using FrontEnd.Web.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using FrontEnd.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddFluentUIComponents();

builder.Services.AddTransient<AuthenticationDelegatingHandler>();
builder.Services.AddHttpClient<IRecipeService, RecipeService>()
    .AddHttpMessageHandler<AuthenticationDelegatingHandler>();
builder.Services.AddHttpClient<IIdentityService, IdentityService>();

builder.Services.AddCascadingAuthenticationState();
builder.AddBlazorAuth<SampleServerSideAuthenticationService>();

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
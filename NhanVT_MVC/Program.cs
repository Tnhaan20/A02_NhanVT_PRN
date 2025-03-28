using A02_Repos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using NhanVT_MVC.Components;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Blazor services
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

builder.Services.AddHttpContextAccessor();

// Your repositories
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<INewsArticleRepository, NewsArticleRepository>();
builder.Services.AddScoped<ICategoriesRepo, CategoriesRepo>();
builder.Services.AddScoped<ITagRepo, Tag_Repo>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// IMPORTANT: Call UseSession before mapping Blazor
app.UseSession();
app.UseAntiforgery();

// Finally, map your Blazor app
app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();

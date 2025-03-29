using A02_Repos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using NhanVT_MVC.Components;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Blazor services
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Disable prerendering by configuring the application to use InteractiveServer as default
builder.Services.AddServerSideBlazor(options =>
{
    // This affects how the server handles the Blazor components
    options.DetailedErrors = builder.Environment.IsDevelopment();
    options.DisconnectedCircuitMaxRetained = 100;
    options.DisconnectedCircuitRetentionPeriod = TimeSpan.FromMinutes(3);
});

// Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();

// Your repositories
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<INewsArticleRepository, NewsArticleRepository>();
builder.Services.AddScoped<ICategoriesRepo, CategoriesRepo>();
builder.Services.AddScoped<ITagRepo, Tag_Repo>();
builder.Services.AddScoped<NhanVT_MVC.Services.AuthService>();

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

// Map your Blazor app with interactive server rendering
app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();
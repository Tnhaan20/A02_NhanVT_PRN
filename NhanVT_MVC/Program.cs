using A02_Repos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using NhanVT_MVC.Components;


var builder = WebApplication.CreateBuilder(args);

// Add interactive server components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add session services
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

// Add HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Register repositories
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<INewsArticleRepository, NewsArticleRepository>();
builder.Services.AddScoped<ICategoriesRepo, CategoriesRepo>();
builder.Services.AddScoped<ITagRepo, Tag_Repo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();
app.UseRouting();
app.UseAntiforgery();

// Map razor components with interactive server render mode
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
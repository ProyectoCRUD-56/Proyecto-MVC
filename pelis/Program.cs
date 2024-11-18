using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Authentication.Cookies;
using pelis.Data;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<pelisContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("pelisContext") ?? throw new InvalidOperationException("Connection string 'pelisContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option => {
        option.LoginPath = "/Acceso/Index";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        option.AccessDeniedPath = "/Home/Privacy";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Acceso}/{action=Index}/{id?}");
app.MapControllers();
app.Run();

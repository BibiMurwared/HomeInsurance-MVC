
/*
 * File: Program.cs
 * Project: HomeInsurance-MVC
 * Author(s): Mohammad, Julia, BIbi
 * Date: April 11, 2026
 * Description:
 * This file contains the startup and configuration logic for the application.
 * It registers services, configures middleware, sets routing rules, and prepares
 * the MVC application to run.
 */

using HomeInsurance_MVC.Data;
using HomeInsurance_MVC.Repositories;
using HomeInsurance_MVC.Services;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
string? databaseConnectionString = builder.Configuration.GetConnectionString("HomeInventoryConnection");
if (string.IsNullOrWhiteSpace(databaseConnectionString))
{
    throw new InvalidOperationException("Connection string 'HomeInventoryConnection' was not found.");
}

builder.Services.AddDbContext<HomeInventoryContext>(options =>
    options.UseSqlServer(databaseConnectionString, sqlOptions =>
        sqlOptions.EnableRetryOnFailure()));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IItemService, ItemService>();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

WebApplication app = builder.Build();

using (IServiceScope startupScope = app.Services.CreateScope())
{
    HomeInventoryContext databaseContext = startupScope.ServiceProvider.GetRequiredService<HomeInventoryContext>();
    databaseContext.Database.EnsureCreated();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();

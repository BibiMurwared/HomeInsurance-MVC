
/*
 * File: Program.cs
 * Project: HomeInsurance-MVC
 * Author(s): Mohammad, Julia, BIbi
 * Date: [Enter Date]
 * Description:
 * This file contains the startup and configuration logic for the application.
 * It registers services, configures middleware, sets routing rules, and prepares
 * the MVC application to run.
 */


 /*
  * Purpose: Application startup and configuration.

What it does

This file configures:

MVC services
database context
middleware
routing
authentication later
authorization later
Why it exists

This is where the whole application is wired together.
  * Program.cs

 This is the startup file.

 It sets up:

 MVC services
 database connection
 routing
 authentication later

 Think of it as:
 the place where the app is wired together.

  */var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();

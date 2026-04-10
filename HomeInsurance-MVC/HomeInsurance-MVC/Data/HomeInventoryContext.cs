
/*
 * File: HomeInventoryContext.cs
 * Project: HomeInsurance-MVC
 * Author(s): Mohammad
 * Date: [Enter Date]
 * Description:
 * This class represents the Entity Framework Core database context for the
 * application. It provides access to the database tables and manages the
 * connection between the application's models and the SQL Server database.
 */
/*This is our database context.

It connects:

our C# models
our SQL database

Without this file:

no EF Core table mapping
no database querying through context
Purpose: Database context for EF Core.

What it does

This class tells Entity Framework:

what tables exist
what models should be stored
how the app connects to the database

It will eventually contain:

DbSet<Item> Items
DbSet<User> Users
Why it exists

Without this file, the app does not know how to talk to the database.

How it connects
Used by controllers
Maps models to SQL tables
Registered in Program.cs*/

namespace HomeInsurance_MVC.Data
{
    /// <summary>
    /// The HomeInventoryContext class is the database context for the application.
    /// It exposes DbSet properties for the models that must be stored in the database.
    /// </summary>
    public class Class
    {
    }
}

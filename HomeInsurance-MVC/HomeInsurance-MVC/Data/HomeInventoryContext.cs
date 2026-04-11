
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
    using HomeInsurance_MVC.Models;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The HomeInventoryContext class is the database context for the application.
    /// It exposes DbSet properties for the models that must be stored in the database.
    /// </summary>
    public class HomeInventoryContext : DbContext
    {
        /// <summary>
        /// Initializes a new context instance with options.
        /// </summary>
        public HomeInventoryContext(DbContextOptions<HomeInventoryContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the user table mapping.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the item table mapping.
        /// </summary>
        public DbSet<Item> Items { get; set; }

        /// <summary>
        /// Configures keys, relationships, and constraints.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(user => user.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasMany(user => user.Items)
                .WithOne(item => item.User)
                .HasForeignKey(item => item.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .Property(user => user.CreatedDate)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<Item>()
                .Property(item => item.CreatedDate)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<User>()
                .HasQueryFilter(user => !user.IsDeleted);

            modelBuilder.Entity<Item>()
                .HasQueryFilter(item => !item.IsDeleted);
        }
    }
}

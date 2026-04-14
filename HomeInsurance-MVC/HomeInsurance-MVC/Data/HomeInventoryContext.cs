
/*
 * File: HomeInventoryContext.cs
 * Project: HomeInsurance-MVC
 * Author(s): Mohammad
 * Date: 2026-04-13
 * Description:
 * This class represents the Entity Framework Core database context for the
 * application. It provides access to the database tables and manages the
 * connection between the application's models and the SQL Server database.
 */

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
        /// Gets or sets the user log table mapping.
        /// </summary>
        public DbSet<UserLog> UserLogs { get; set; }

        /// <summary>
        /// Gets or sets the system log table mapping.
        /// </summary>
        public DbSet<SystemLog> SystemLogs { get; set; }

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

            modelBuilder.Entity<Item>()
                .Property(item => item.EstimatedValue)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<UserLog>()
                .Property(userLog => userLog.CreatedDate)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<SystemLog>()
                .Property(systemLog => systemLog.CreatedDate)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<User>()
                .HasQueryFilter(user => !user.IsDeleted);

            modelBuilder.Entity<Item>()
                .HasQueryFilter(item => !item.IsDeleted);

            return;
        }
    }
}

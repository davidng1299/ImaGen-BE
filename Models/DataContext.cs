using Humanizer;
using Microsoft.EntityFrameworkCore;

namespace ImaGen_BE.Models;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Image> Images{ get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply snake_case to table and column names
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            // Table name
            entity.SetTableName(entity.GetTableName()?.Underscore());

            // Column names
            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(property.Name.Underscore());
            }

            // Keys
            foreach (var key in entity.GetKeys())
            {
                key.SetName(key.GetName()?.Underscore());
            }

            // Foreign keys
            foreach (var fk in entity.GetForeignKeys())
            {
                fk.SetConstraintName(fk.GetConstraintName()?.Underscore());
            }

            // Indexes
            foreach (var index in entity.GetIndexes())
            {
                index.SetDatabaseName(index.GetDatabaseName()?.Underscore());
            }
        }
    }
}
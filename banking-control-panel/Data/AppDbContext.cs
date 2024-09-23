using banking_control_panel.Models;
using Microsoft.EntityFrameworkCore;

namespace banking_control_panel.Data;

/// <summary>
///     Db Context for the application
/// </summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Client> Clients { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();

    }
}
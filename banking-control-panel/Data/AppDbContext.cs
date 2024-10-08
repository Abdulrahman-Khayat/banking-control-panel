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
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Search> Searches { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
        
        modelBuilder.Entity<Client>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<Client>().HasIndex(u => u.MobileNumber).IsUnique();
        
        modelBuilder.Entity<Client>().Navigation(c => c.Accounts).AutoInclude();
        modelBuilder.Entity<Client>().Navigation(c => c.Address).AutoInclude();

        modelBuilder.Entity<Search>()
            .Property(f => f.FilterParameters)
            .HasColumnType("jsonb");
    }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        AddTimestamps();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void AddTimestamps()
    {
        var entities = ChangeTracker.Entries()
            .Where(x => x.Entity is BaseModel && (x.State == EntityState.Added || x.State == EntityState.Modified));

        foreach (var entity in entities)
        {
            var now = DateTime.UtcNow; // current datetime

            if (entity.State == EntityState.Added)
            {
                ((BaseModel) entity.Entity).CreatedAt = now;
            }

            ((BaseModel) entity.Entity).UpdatedAt = now;
        }
    }
}
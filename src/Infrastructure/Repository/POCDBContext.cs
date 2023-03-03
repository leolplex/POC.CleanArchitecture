using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public interface IPOCDbContext : IDisposable
{
    DbSet<Account> Accounts { get; set; }

    Task<int> SaveChanges(CancellationToken cancellationToken = default(CancellationToken));

}

public partial class POCDBContext : DbContext, IPOCDbContext
{
    public DbSet<Account> Accounts { get; set; }

    public async Task<int> SaveChanges(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(true, cancellationToken);
        return result;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(connectionString:
           "Server=host;Port=5432;User Id=user;Password=pass;Database=db;");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(e => e.ToTable("accounts"));
        modelBuilder.Entity<Account>(entity =>
        {
            entity.Property(e => e.Id)
                                .HasColumnName("id");
            entity.Property(e => e.Address).IsRequired().HasColumnName("address");
            entity.Property(e => e.Phone).IsRequired().HasColumnName("phone");
        });

        base.OnModelCreating(modelBuilder);
    }
}

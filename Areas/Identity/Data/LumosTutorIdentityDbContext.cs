using Final.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Final.Areas.Identity.Data;

public class LumosTutorIdentityDbContext : IdentityDbContext<LumosTutorUser>
{
    public LumosTutorIdentityDbContext(DbContextOptions<LumosTutorIdentityDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Final.Models.Tutor>()
        .Property(p => p.Price).HasColumnType("decimal(18,4)");
        modelBuilder.Entity<Final.Models.Language>()
        .Property(p => p.Status).HasDefaultValue(false);

    }

    public DbSet<Final.Models.Language> Language { get; set; } = default!;

    public DbSet<Final.Models.Tutor>? Tutor { get; set; }
    // public DbSet<Final.Models.Schedule>? Schedule { get; set; }

    public DbSet<Final.Models.Booking>? Booking { get; set; }

    // public DbSet<Final.Models.OrderDetail>? OrderDetail { get; set; }
}

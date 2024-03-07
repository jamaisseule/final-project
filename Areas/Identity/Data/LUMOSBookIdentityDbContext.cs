using LUMOSBook.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LUMOSBook.Areas.Identity.Data;

public class LUMOSBookIdentityDbContext : IdentityDbContext<BookUser>
{
    public LUMOSBookIdentityDbContext(DbContextOptions<LUMOSBookIdentityDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
       base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.Entity<LUMOSBook.Models.Book>()
             .Property(p => p.Price).HasColumnType("decimal(18,4)");
        builder.Entity<LUMOSBook.Models.Author>()
             .HasIndex(p => p.Name).IsUnique();

    }

    public DbSet<LUMOSBook.Models.Category> Category { get; set; } = default!;

    public DbSet<LUMOSBook.Models.Book>? Book { get; set; }

    public DbSet<LUMOSBook.Models.Author>? Author { get; set; }

    public DbSet<LUMOSBook.Models.Publisher>? Publisher { get; set; }

    public DbSet<LUMOSBook.Models.Order> Order { get; set; }
    public DbSet<LUMOSBook.Models.OrderItem> OrderItem { get; set; }
}

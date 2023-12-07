using Microsoft.EntityFrameworkCore;

namespace Application.Database;

public class AppDbContext : DbContext
{
    public DbSet<Entity> Table { get; set; } = default!;
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}
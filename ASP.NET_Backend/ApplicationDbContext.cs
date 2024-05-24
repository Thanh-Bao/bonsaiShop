using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> User { set; get; }
    public DbSet<Product> Product { set; get; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
}

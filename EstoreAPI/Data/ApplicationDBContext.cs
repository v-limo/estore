using EStoreAPI.Data.Seeding;

namespace EStoreAPI.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    private readonly DataSeed _dataSeed = new(1000);

    public DbSet<Customer> Customers { get; init; } = null!;
    
    public DbSet<Product> Products { get; init; } = null!;
    public DbSet<Category> Categories { get; init; } = null!;
    public DbSet<Order> Orders { get; init; } = null!;
    public DbSet<Review> Reviews { get; init; } = null!;
    public DbSet<Cart> Carts { get; init; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var products = _dataSeed.SeedProduct();
        var categories = _dataSeed.SeedCategories();

        modelBuilder.Entity<Product>().HasData(products);
        modelBuilder.Entity<Category>().HasData(categories);
    }
}
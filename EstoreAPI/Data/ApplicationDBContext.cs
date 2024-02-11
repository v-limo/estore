using EStoreAPI.Data.Seeding;

namespace EStoreAPI.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    private readonly DataSeed _dataSeed;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        _dataSeed = new DataSeed(1000);
    }

    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Review> Reviews { get; set; } = null!;
    public DbSet<Cart> Carts { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var products = _dataSeed.SeedProduct();
        var categories = _dataSeed.SeedCategories();

        modelBuilder.Entity<Product>().HasData(products);
        modelBuilder.Entity<Category>().HasData(categories);
    }
}
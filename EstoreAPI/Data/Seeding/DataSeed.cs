using Bogus;

namespace EStoreAPI.Data.Seeding;

public class DataSeed
{
    private static List<Product> _products = [];
    private static List<Category> _categories = [];
    private readonly int _productAmount;

    public DataSeed(int productsAmount = 100)
    {
        _productAmount = productsAmount;
        _products = [];
        _categories = [];
        Init();
    }


    private void Init()
    {
        GenerateCategories();
        GenerateProducts();

        AddProductsToCategories();
    }

    private void AddProductsToCategories()
    {
        foreach (var category in _categories)
        {
            var products = _products.Where(p => p.CategoryId == category.Id);
            category.Products = products;
        }
    }


    private void GenerateProducts()
    {
        var productId = 1;
        var productFaker = new Faker<Product>()
            .RuleFor(p => p.Id, _ => productId++)
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .RuleFor(p => p.Price, f => decimal.Parse(f.Commerce.Price()))
            .RuleFor(p => p.CreatedAt, f => f.PickRandom(DateTime.MaxValue))
            .RuleFor(p => p.CategoryId, f => f.PickRandom(_categories).Id);
        _products = productFaker.Generate(_productAmount);
    }

    private void GenerateCategories()
    {
        List<Category> categories =
        [
            new Category
            {
                CreatedAt = default,
                Id = 1,
                Name = "Shoes",
                Products = []
            },
            new Category
            {
                CreatedAt = default,
                Id = 2,
                Name = "Clothing",
                Products = []
            },
            new Category
            {
                CreatedAt = DateTime.Now,
                Id = 3,
                Name = "Phones",
                Products = []
            }
        ];
        _categories = categories;
    }

    public List<Product> SeedProduct()
    {
        return _products;
    }

    public List<Category> SeedCategories()
    {
        return _categories;
    }
}
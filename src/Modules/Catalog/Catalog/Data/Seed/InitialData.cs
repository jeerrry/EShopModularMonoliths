namespace Catalog.Data.Seed;

public static class InitialData
{
    public static IEnumerable<Product> Products => new[]
    {
        Product.Create(Guid.NewGuid(), "IPhone", "Long Description", ["category1"], 500, "imageFile"),
        Product.Create(Guid.NewGuid(), "Samsung", "Long Description", ["category1"], 2321, "imageFile"),
        Product.Create(Guid.NewGuid(), "Xiaomi", "Long Description", ["category2"], 222, "imageFile"),
        Product.Create(Guid.NewGuid(), "Nokia", "Long Description", ["category2"], 333, "imageFile"),
    };
}
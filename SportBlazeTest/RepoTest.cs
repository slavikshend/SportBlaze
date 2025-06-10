using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi.BLL.Repos.Implementations;
using webapi.DAL.Context;
using webapi.DAL.Entities.Main;
using Xunit;

public class ProductRepoTests
{
    private readonly ProductRepo _repo;
    private readonly SportsShopDbContext _context;

    public ProductRepoTests()
    {
        var options = new DbContextOptionsBuilder<SportsShopDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new SportsShopDbContext(options);
        _repo = new ProductRepo(_context);
    }

    private void SeedDatabase()
    {
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

        if (!_context.Products.Any())
        {
            _context.Categories.AddRange(GetTestCategories());
            _context.SubCategories.AddRange(GetTestSubCategories());
            _context.Brands.AddRange(GetTestBrands());
            _context.Products.AddRange(GetTestProducts());
            _context.SaveChanges();
        }
    }

    private List<Category> GetTestCategories()
    {
        return new List<Category>
        {
            new Category { Name = "Category 1", Image = "Image 1" },
            new Category { Name = "Category 2", Image = "Image 2" }
        };
    }

    private List<SubCategory> GetTestSubCategories()
    {
        return new List<SubCategory>
        {
            new SubCategory { Name = "SubCategory 1", Description = "Description 1", ImageUrl = "ImageUrl 1", CategoryId = 1 },
            new SubCategory { Name = "SubCategory 2", Description = "Description 2", ImageUrl = "ImageUrl 2", CategoryId = 2 }
        };
    }

    private List<Brand> GetTestBrands()
    {
        return new List<Brand>
        {
            new Brand { Name = "Brand 1", Image = "Image 1" },
            new Brand { Name = "Brand 2", Image = "Image 2" }
        };
    }

    private List<Product> GetTestProducts()
    {
        return new List<Product>
        {
            new Product
            {
                Name = "Product 1",
                Description = "Description 1",
                ImageUrl = "ImageUrl 1",
                Quantity = 10,
                Price = 100,
                Discount = 10,
                BrandId = 1,
                SubCategoryId = 1
            },
            new Product
            {
                Name = "Product 2",
                Description = "Description 2",
                ImageUrl = "ImageUrl 2",
                Quantity = 20,
                Price = 200,
                Discount = 0,
                BrandId = 2,
                SubCategoryId = 2
            }
        };
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllProducts()
    {
        SeedDatabase();
        var result = await _repo.GetAllAsync();

        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsCorrectProduct()
    {
        SeedDatabase();
        var product = await _repo.GetByIdAsync(1);

        Assert.NotNull(product);
        Assert.Equal("Product 1", product.Name);
    }

    [Fact]
    public async Task CreateAsync_AddsNewProduct()
    {
        SeedDatabase();
        var newProduct = new Product
        {
            Name = "Product 3",
            Description = "Description 3",
            ImageUrl = "ImageUrl 3",
            Quantity = 30,
            Price = 300,
            Discount = 30,
            BrandId = 1,
            SubCategoryId = 1
        };

        var createdProduct = await _repo.CreateAsync(newProduct);
        var productInDb = await _context.Products.FindAsync(createdProduct.Id);

        Assert.NotNull(productInDb);
        Assert.Equal("Product 3", productInDb.Name);
    }

    [Fact]
    public async Task DeleteAsync_RemovesProduct()
    {
        SeedDatabase();
        var result = await _repo.DeleteAsync(1);
        var productInDb = await _context.Products.FindAsync(1);

        Assert.True(result);
        Assert.Null(productInDb);
    }

    [Fact]
    public async Task UpdateAsync_UpdatesProduct()
    {
        SeedDatabase();
        var productToUpdate = await _context.Products.FindAsync(1);
        productToUpdate.Name = "Updated Product";

        var updatedProduct = await _repo.UpdateAsync(productToUpdate);
        var productInDb = await _context.Products.FindAsync(updatedProduct.Id);

        Assert.NotNull(productInDb);
        Assert.Equal("Updated Product", productInDb.Name);
    }

    [Fact]
    public async Task GetSpecialOfferProductsAsync_ReturnsDiscountedProducts()
    {
        SeedDatabase();
        var result = await _repo.GetSpecialOfferProductsAsync();

        Assert.Single(result);
        Assert.Equal("Product 1", result.First().Name);
    }

    [Fact]
    public async Task GetProductDetailsByIdAsync_ReturnsProductDetails()
    {
        SeedDatabase();
        var product = await _repo.GetProductDetailsByIdAsync(1);

        Assert.NotNull(product);
        Assert.Equal("Product 1", product.Name);
    }

    [Fact]
    public async Task SearchProductsByNameAsync_ReturnsMatchingProducts()
    {
        SeedDatabase();
        var result = await _repo.SearchProductsByNameAsync("Product");

        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetProductsBySubcategoryAsync_ReturnsCorrectProducts()
    {
        SeedDatabase();
        var result = await _repo.GetProductsBySubcategoryAsync("SubCategory 1");

        Assert.Single(result);
        Assert.Equal("Product 1", result.First().Name);
    }
}

using CosmeticHub.Controllers;
using CosmeticHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;





public class ProductsControllerTests : IDisposable
{
    private readonly MyDbContext _context;
    private readonly ProductsController _controller;

    public ProductsControllerTests()
    {
        // Replace with your actual connection string
        var connectionString = "YourConnectionString";
        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        _context = new MyDbContext(options);
        _controller = new ProductsController(_context);

        // Seed the database with test data (if needed)
        // ...
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    [Fact]
    public async Task Index_ReturnsListOfProducts()
    {
        // Arrange
        // Seed some products into the database

        // Act
        var result = await _controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = viewResult.Model as List<Product>;
        Assert.NotNull(model);
        Assert.NotEmpty(model);
    }

    [Fact]
    public async Task Details_ValidId_ReturnsProduct()
    {
        // Arrange
        var productId = 1; // Assuming a product with ID 1 exists

        // Act
        var result = await _controller.Details(productId);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var product = viewResult.Model as Product;
        Assert.NotNull(product);
        Assert.Equal(productId, product.ProductId);
    }

    [Fact]
    public async Task Details_InvalidId_ReturnsNotFound()
    {
        // Arrange
        var invalidId = 999; // Assuming no product with this ID
        // Act
        var result = await _controller.Details(invalidId);
        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Create_ValidProduct_ReturnsRedirectAndSavesProduct()
    {
        // Arrange
        var newProduct = new Product
        {
            ProductName = "Test Product",
            Price = 19.99m,
            QuantityInStock = 10,
            Category = "Makeup"
        };
        // Act
        var result = await _controller.Create(newProduct);
        // Assert
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(nameof(_controller.Index), redirectResult.ActionName);
        // Verify product is saved in the database
        var savedProduct = await _context.Products.FirstOrDefaultAsync(p => p.ProductName == newProduct.ProductName);
        Assert.NotNull(savedProduct);
        Assert.Equal(newProduct.ProductName, savedProduct.ProductName);
        Assert.Equal(newProduct.Price, savedProduct.Price);
    }



    
}

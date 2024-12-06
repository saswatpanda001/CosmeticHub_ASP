using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CosmeticHub.Models;
using Microsoft.AspNetCore.Http;
using CosmeticHub.Controllers;




public class UserServiceTestsWithMoq{

    [Fact]
    public async Task Create_ValidProduct_SavesProductAndRedirects()
    {
        // Arrange
        var mockContext = new Mock<MyDbContext>();
        var newProduct = new Product
        {
            ProductName = "New Product",
            Description = "This is a new product",
            Price = 19.99m,
            QuantityInStock = 10,
            Category = "Cosmetics"
        };

        mockContext.Setup(m => m.Add(It.IsAny<Product>())).Verifiable();
       

        var controller = new ProductsController(mockContext.Object);

        // Act
        var result = await controller.Create(newProduct);

        // Assert
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(nameof(controller.Index), redirectResult.ActionName);
        mockContext.Verify(m => m.Add(It.Is<Product>(p => p.Equals(newProduct))));
    }


    [Fact]
    public async Task Index_ReturnsListOfProducts()
    {
        // Arrange
        var mockContext = new Mock<MyDbContext>();
        var products = new List<Product>
        {
            new Product { ProductId = 1, ProductName = "Product 1" },
            new Product { ProductId = 2, ProductName = "Product 2" }
        };

        mockContext.Setup(m => m.Products);
        var controller = new ProductsController(mockContext.Object);

        // Act
        var result = await controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = viewResult.Model as List<Product>;

        Assert.NotNull(model);
        Assert.Equal(2, model.Count);
    }


























}


using CosmeticHub.Controllers;
using CosmeticHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;


public class UsersControllerTests
{
    public readonly MyDbContext _dbContext;
    private readonly UsersController _controller;
    private readonly IDbContextTransaction _transaction;

    public UsersControllerTests()
    {
        var dbContextOptions = new DbContextOptionsBuilder<MyDbContext>()
            .UseSqlServer("Server=DESKTOP-6BQR3PO\\SQLEXPRESS;Database=CosmeticDB;Trusted_Connection=True;Encrypt=False")
            .Options;

        _dbContext = new MyDbContext(dbContextOptions);
        _transaction = _dbContext.Database.BeginTransaction();  // Begin a transaction

        _controller = new UsersController(_dbContext);
    }

    // Use this method to roll back changes after each test
    public void Dispose()
    {
        _transaction.Rollback();  // Rollback the transaction
        _transaction.Dispose();
    }

    [Fact]
    public void Delete_ValidId_DeletesUser()
    {
        // Arrange
        var userId = 1;

        // Act
        var result = _controller.Delete(userId);

        // Assert
        var deletedUser = _dbContext.Users.Find(userId);
        Assert.Null(deletedUser);  // User should be deleted
    }
    [Fact]
    public async Task Get_InvalidId_ReturnsNotFound()
    {
        // Arrange
        var nonExistingId = 10;

        // Act
        var actionResult = await _controller.Details(nonExistingId);

        // Assert
        Assert.IsType<NotFoundResult>(actionResult);
    }






    [Fact]
    public async Task Index_ReturnsListOfUsers()
    {
        // Arrange
        // Add some seed data to the database (if needed)

        // Act
        var result = await _controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = viewResult.Model as List<User>;
        Assert.NotNull(model);
        Assert.NotEmpty(model);
    }

   

    [Fact]
    public async Task Details_InvalidId_ReturnsNotFound()
    {
        // Arrange
        var invalidId = 999; // Assuming no user with this ID

        // Act
        var result = await _controller.Details(invalidId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }







}


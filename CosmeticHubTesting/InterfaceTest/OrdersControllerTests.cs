using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using CosmeticHub.Controllers;
using CosmeticHub.Models;

namespace CosmeticHub.Tests
{
    public class OrdersControllerTests
    {
        private readonly Mock<DbSet<Order>> _orderSetMock;
        private readonly Mock<MyDbContext> _contextMock;
        private readonly OrdersController _controller;

        public OrdersControllerTests()
        {
            _orderSetMock = new Mock<DbSet<Order>>();
            _contextMock = new Mock<MyDbContext>();
            _contextMock.Setup(m => m.Orders).Returns(_orderSetMock.Object);
            _controller = new OrdersController(_contextMock.Object);
        }


       
        [Fact]
        public async Task Edit_ReturnsViewWithOrder()
        {
            // Arrange
            var order = new Order { OrderId = 1, UserId = 1, TotalPrice = 100, OrderStatus = "Delivered", PlacedAt = DateTime.Now };

            _orderSetMock.Setup(m => m.FindAsync(1)).ReturnsAsync(order);

            // Act
            var result = await _controller.Edit(1) as ViewResult;

            // Assert
            var model = Assert.IsAssignableFrom<Order>(result.ViewData.Model);
            Assert.Equal(1, model.OrderId);
        }



        [Fact]
        public async Task DeleteConfirmed_RedirectsToIndexOnSuccess()
        {
            // Arrange
            var order = new Order { OrderId = 1, UserId = 1, TotalPrice = 100, OrderStatus = "Delivered", PlacedAt = DateTime.Now };

            _orderSetMock.Setup(m => m.FindAsync(1)).ReturnsAsync(order);

            // Act
            var result = await _controller.DeleteConfirmed(1) as RedirectToActionResult;

            // Assert
            _orderSetMock.Verify(x => x.Remove(order), Times.Once);
            _contextMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
            Assert.Equal("Index", result.ActionName);
        }




        // Your test methods will go here...
    }
}

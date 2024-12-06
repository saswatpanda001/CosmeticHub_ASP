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
    public class ReviewsControllerTests
    {
        private readonly Mock<DbSet<Review>> _reviewSetMock;
        private readonly Mock<MyDbContext> _contextMock;
        private readonly ReviewsController _controller;

        public ReviewsControllerTests()
        {
            _reviewSetMock = new Mock<DbSet<Review>>();
            _contextMock = new Mock<MyDbContext>();
            _contextMock.Setup(m => m.Reviews).Returns(_reviewSetMock.Object);
            _controller = new ReviewsController(_contextMock.Object);
        }

        [Fact]
        public async Task Edit_ReturnsViewWithReview()
        {
            // Arrange
            var review = new Review { ReviewId = 1, UserId = 1, ProductId = 1, Rating = 5, Comment = "Great!", CreatedAt = DateTime.Now };

            _reviewSetMock.Setup(m => m.FindAsync(1)).ReturnsAsync(review);

            // Act
            var result = await _controller.Edit(1) as ViewResult;

            // Assert
            var model = Assert.IsAssignableFrom<Review>(result.ViewData.Model);
            Assert.Equal(1, model.ReviewId);
        }

        [Fact]
        public async Task DeleteConfirmed_RedirectsToIndexOnSuccess()
        {
            // Arrange
            var review = new Review { ReviewId = 1, UserId = 1, ProductId = 1, Rating = 5, Comment = "Great!", CreatedAt = DateTime.Now };

            _reviewSetMock.Setup(m => m.FindAsync(1)).ReturnsAsync(review);

            // Act
            var result = await _controller.DeleteConfirmed(1) as RedirectToActionResult;

            // Assert
            _reviewSetMock.Verify(x => x.Remove(review), Times.Once);
            _contextMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
            Assert.Equal("Index", result.ActionName);
        }
    }
}

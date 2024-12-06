using Microsoft.AspNetCore.Mvc;
using CosmeticHub.Models;
using System.Text;  // For StringBuilder
using Microsoft.AspNetCore.Mvc;  // For IActionResult and File method
using System.Linq;  // For LINQ queries like ToList()
using CsvHelper;  // For CsvHelper library
using CsvHelper.Configuration;  // For CSV configuration (if required)


public class ExportController : Controller
{
    private readonly MyDbContext _context;

    public ExportController(MyDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult ExportToCsv()
    {
        var products = _context.Products.ToList();
        var reviews = _context.Reviews.ToList();
        var users = _context.Users.ToList(); // Assuming you have a Users table
        var orders = _context.Orders.ToList(); // Assuming you have an Orders table

        var builder = new StringBuilder();

        // Add headers for products
        builder.AppendLine("ProductId,ProductName,Description,Price,QuantityInStock,Category");
        foreach (var product in products)
        {
            builder.AppendLine($"{product.ProductId},{product.ProductName},{product.Description},{product.Price},{product.QuantityInStock},{product.Category}");
        }

        // Add headers for reviews
        builder.AppendLine();
        builder.AppendLine("ReviewId,UserId,ProductId,Rating,Comment,CreatedAt");
        foreach (var review in reviews)
        {
            builder.AppendLine($"{review.ReviewId},{review.UserId},{review.ProductId},{review.Rating},{review.Comment},{review.CreatedAt}");
        }

        // Add headers for users
        builder.AppendLine();
        builder.AppendLine("UserId,Username,Email,CreatedAt");
        foreach (var user in users)
        {
            builder.AppendLine($"{user.UserId},{user.Username},{user.Email},{user.CreatedAt}");
        }

        // Add headers for orders
        builder.AppendLine();
        builder.AppendLine("OrderId,UserId,ProductId,Quantity,TotalPrice,OrderDate");
        foreach (var order in orders)
        {
            builder.AppendLine($"{order.OrderId},{order.UserId},{order.OrderId},{order.OrderStatus},{order.TotalPrice},{order.PlacedAt}");
        }

        // Return as a downloadable CSV file
        var fileName = "ExportData.csv";
        return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", fileName);
    }
}


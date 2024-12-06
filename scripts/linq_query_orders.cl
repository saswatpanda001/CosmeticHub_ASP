using System.Diagnostics;
using CosmeticHub.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CosmeticHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            using (var context = new MyDbContext())
            {
                //Get all orders
                Console.Write("\n\nGet all orders");
                var allOrders = context.Orders.ToList();

                foreach (var order in allOrders)
                {
                    Console.WriteLine($"Order ID: {order.OrderId}, Total Price: {order.TotalPrice}, Status: {order.OrderStatus}");
                }

                // Get orders by status
                Console.Write("\n\nGet orders by status");
                var shippedOrders = context.Orders
                            .Where(o => o.OrderStatus == "Shipped")
                            .ToList();

                foreach (var order in shippedOrders)
                {
                    Console.WriteLine($"Order ID: {order.OrderId}, Status: {order.OrderStatus}, Placed At: {order.PlacedAt}");
                }

                // Get total sales(sum of total price):
                Console.Write("\n\nGet total sales (sum of total price):");
                var totalSales = context.Orders
                         .Sum(o => o.TotalPrice);

                Console.WriteLine($"Total Sales: {totalSales}");




            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

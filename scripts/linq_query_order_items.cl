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

                //Retrieve All Order Items
                Console.Write("\n\nRetrieve All Order Items");
                var allOrderItems = context.OrderItems.ToList();

                foreach (var item in allOrderItems)
                {
                    Console.WriteLine($"Order Item ID: {item.OrderItemId}, Order ID: {item.OrderId}, Product ID: {item.ProductId}, Quantity: {item.Quantity}, Price Each: {item.PriceEach}");
                }

                // Find Order Items by Order ID:
                Console.Write("\n\nFind Order Items by Order ID:");
                var itemsForOrder = context.OrderItems
                            .Where(oi => oi.OrderId == 1)
                            .ToList();

                foreach (var item in itemsForOrder)
                {
                    Console.WriteLine($"Order Item ID: {item.OrderItemId}, Product ID: {item.ProductId}, Quantity: {item.Quantity}, Price Each: {item.PriceEach}");
                }


                // Calculate Total Price for an Order
                Console.Write("\n\nCalculate Total Price for an Order");
                var totalPrice = context.OrderItems
                         .Where(oi => oi.OrderId == 1)
                         .Sum(oi => oi.Quantity * oi.PriceEach);

                Console.WriteLine($"Total price for Order ID 1: {totalPrice}");

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

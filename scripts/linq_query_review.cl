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

                //Get all reviews
                Console.Write("\n\nGet all reviews");
                var allReviews = context.Reviews.ToList();

                foreach (var review in allReviews)
                {
                        Console.WriteLine($"Review ID: {review.ReviewId}, Product ID: {review.ProductId}, Rating: {review.Rating}, Comment: {review.Comment}");
                }

                // Get reviews for a specific product
                Console.Write("\n\nGet reviews for a specific product");
                var productReviews = context.Reviews
                          .Where(r => r.ProductId == 1)
                          .ToList();

                foreach (var review in productReviews)
                {
                    Console.WriteLine($"Review ID: {review.ReviewId}, Rating: {review.Rating}, Comment: {review.Comment}");
                }

                // Get reviews with a specific rating of 5:
                Console.Write("\n\nGet reviews with a specific rating if 5");
                var highRatedReviews = context.Reviews
                             .Where(r => r.Rating == 5)
                             .ToList();

                foreach (var review in highRatedReviews)
                {
                    Console.WriteLine($"Review ID: {review.ReviewId}, Product ID: {review.ProductId}, Comment: {review.Comment}");
                }

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

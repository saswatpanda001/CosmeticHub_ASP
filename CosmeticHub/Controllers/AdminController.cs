using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CosmeticHub.Models;


namespace CosmeticHub.Controllers
{
    public class AdminController : Controller
    {

        private readonly MyDbContext _context;

        public AdminController(MyDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }



        [HttpGet]
        public async Task<IActionResult> AdminView()
        {
            // Retrieve the UserId from the session
            var userId = HttpContext.Session.GetInt32("UserID");

            // If UserId is not found, redirect to login page or handle as needed
            if (userId == null)
            {
                return RedirectToAction("Login", "Accounts");
            }

            // Fetch total users, products, and orders from the database
            var totalUsers = await _context.Users.CountAsync();
            var totalProducts = await _context.Products.CountAsync();
            var totalOrders = await _context.Orders.CountAsync();

            // Create an object to pass to the view
            var adminDashboardData = new AdminDashboardViewModel
            {
                TotalUsers = totalUsers,
                TotalProducts = totalProducts,
                TotalOrders = totalOrders
            };

            // Return the view with the data
            return View(adminDashboardData);
        }


    }
}

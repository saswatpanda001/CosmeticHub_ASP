using CosmeticHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CosmeticHub.Controllers
{
    public class CustomerController : Controller
    {
        private readonly MyDbContext _context;

        public CustomerController(MyDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CustomerView()
        {
            // Retrieve the UserId from the session
            var userId = HttpContext.Session.GetInt32("UserID");

            // If UserId is not found, redirect to login page or handle as needed
            if (userId == null)
            {
                return RedirectToAction("Login", "Accounts");
            }

            string username = HttpContext.Session.GetString("Username") ?? "Guest";
            ViewBag.Username = username; // Pass it to the view
            var products = _context.Products.ToList();
            return View(products); // Pass the room list to the view
        }

        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            // Get the current cart from session
            var cart = HttpContext.Session.GetString("Cart");

            // If the cart doesn't exist, create a new one
            if (string.IsNullOrEmpty(cart))
            {
                cart = productId.ToString(); // Initialize with the first product
            }
            else
            {
                // Append the new product ID to the existing cart string
                cart += $"-{productId}";
            }

            // Store the updated cart in session
            HttpContext.Session.SetString("Cart", cart);

            // Redirect to the cart page
            return RedirectToAction("Cart");
        }

        // Display the cart
        public IActionResult Cart()
        {
            // Get the cart from session
            var cart = HttpContext.Session.GetString("Cart");

            if (string.IsNullOrEmpty(cart))
            {
                return View(new List<Product>()); // Return empty if no products in cart
            }

            // Convert the cart string to a list of product IDs
            var productIds = cart.Split('-').Select(int.Parse).ToList();

            // Fetch products from the database based on the product IDs
            var products = _context.Products.Where(p => productIds.Contains(p.ProductId)).ToList();

            return View(products); // Pass the products to the view
        }



        [HttpPost]
        public IActionResult DeleteFromCart(int productId)
        {
            var cart = HttpContext.Session.GetString("Cart");
            var userId = HttpContext.Session.GetInt32("UserID");
            

            // If UserId is not found, redirect to login page or handle as needed
            if (userId == null)
            {
                return RedirectToAction("Login", "Accounts");
            }

            // Get the current cart from session
            

            if (string.IsNullOrEmpty(cart))
            {
                return RedirectToAction("Cart"); // If cart is empty, redirect to the cart page
            }

            // Split the cart string into a list of product IDs
            var productIds = cart.Split('-').Select(int.Parse).ToList();

            // Remove the selected product ID from the list
            productIds.Remove(productId);

            // If the list is empty, clear the session
            if (productIds.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                // Update the cart string in session
                HttpContext.Session.SetString("Cart", string.Join("-", productIds));
            }

            // Redirect back to the cart page
            return RedirectToAction("Cart");
        }


        [HttpPost]
        public async Task<IActionResult> Purchase(decimal totalPrice, string cartDetails, string shippingAddress)
        {
            var userId = HttpContext.Session.GetInt32("UserID");

            if (userId == null)
            {
                return RedirectToAction("Login", "Accounts");
            }

            // Deserialize cart details
            var cartItems = JsonConvert.DeserializeObject<List<CartItem>>(cartDetails);

            Random random = new Random();
            int orderId = random.Next(1, 10001);

            var newOrder = new Order
            {
                UserId = userId.Value,
                OrderRec = orderId,
                TotalPrice = totalPrice,
                OrderStatus = "Processing",
                PlacedAt = DateTime.Now,
                ShippingAddress = shippingAddress
         
            };

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            foreach (var item in cartItems)
            {
                var orderItem = new OrderItem
                {
                    OrderId = orderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    PriceEach = item.Price
                };

                _context.OrderItems.Add(orderItem);
            }

            await _context.SaveChangesAsync();

            HttpContext.Session.Remove("Cart"); // Clear the cart

            return RedirectToAction("DisplayUserOrders","Orders");
        }

        // Define CartItem class
        public class CartItem
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
        }



    }
}

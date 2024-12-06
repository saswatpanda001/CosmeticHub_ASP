
using Microsoft.AspNetCore.Mvc;
using CosmeticHub.Models;
namespace CosmeticHub.Controllers
{
    public class AccountsController : Controller
    {
        public readonly MyDbContext _context; // Assuming EF Core for dat  abase access

        public AccountsController(MyDbContext context)
        {
            _context = context;
        }



        // GET: Login page
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login page
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Check credentials in the database
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user == null)
            {
                // Invalid login attempt
                ViewBag.Error = "Invalid username or password.";
                return View();
            }

            // Store user data in session if login is successful
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetInt32("UserID", user.UserId);

            // Redirect based on user type
            if (user.UserType.ToLower() == "admin")
            {
                return RedirectToAction("CustomerView", "Customer");
            }
            else
            {
                return RedirectToAction("CustomerView", "Customer");
            }
        }


        // GET: Login page
        [HttpGet]
        public IActionResult AdminLogin()
        {
            return View();
        }




        [HttpPost]
        public IActionResult AdminLogin(string username, string password)
        {
            // Check credentials in the database
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user == null)
            {
                // Invalid login attempt
                ViewBag.Error = "Invalid username or password.";
                return View();
            }
            else {
                if (user.UserType.ToLower() != "admin")
                {
                    ViewBag.Error = "User does not belong to admin category";
                    return View();
                }
            }

            // Store user data in session if login is successful
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetInt32("UserID", user.UserId);

            // Redirect based on user type
            if (user.UserType.ToLower() == "admin")
            {
                return RedirectToAction("AdminView", "Admin");
            }
            else
            {
                return RedirectToAction("AdminView", "Admin");
            }
        }






        [HttpGet]
        public IActionResult Logout()
        {

            HttpContext.Session.Remove("UserID");
            HttpContext.Session.Remove("Username");
            return RedirectToAction("Login", "Accounts");
        }


        // GET: Signup
        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        // POST: Signup
        [HttpPost]
        public IActionResult Signup(User user)
        {
            // Check if the model is valid
            if (ModelState.IsValid)
            {
                // Validate if Username already exists
                var existingUserByName = _context.Users.FirstOrDefault(u => u.Username == user.Username);
                if (existingUserByName != null)
                {
                    ViewBag.ErrorMessage = "Username already exists!";
                    return View(user);
                }

                // Validate if Email already exists
                var existingUserByEmail = _context.Users.FirstOrDefault(u => u.Email == user.Email);
                if (existingUserByEmail != null)
                {
                    ViewBag.ErrorMessage = "Email already exists!";
                    return View(user);
                }

                // Validate Password Strength
                if (user.Password.Length < 6)
                {
                    ViewBag.ErrorMessage = "Password must be at least 6 characters long!";
                    return View(user);
                }

                // Validate phone number (must be exactly 10 digits)
                if (user.PhoneNumber != null && (user.PhoneNumber.Length != 10 || !user.PhoneNumber.All(char.IsDigit)))
                {
                    ViewBag.ErrorMessage = "Phone number must be exactly 10 digits.";
                    return View(user);
                }

                // Set default role as "Customer"
                user.UserType = "Customer";

                // Set the CreatedAt date
                user.CreatedAt = DateTime.Now;

                // Add the user to the database
                _context.Users.Add(user);
                _context.SaveChanges();

                // Redirect to login page after successful signup
                return RedirectToAction("Login", "Accounts");
            }

            // Return the view if the model is not valid
            return View(user);
        }

        [HttpGet]
        public IActionResult PasswordReset()
        {
            return View();
        }



        [HttpPost]
        public IActionResult ResetPassword(string username, string email, string phoneNumber, string password, string confirmPassword)
        {
            // Validation
            if (password != confirmPassword)
            {
                ViewBag.Error = "Passwords do not match.";
                return View("PasswordReset");
            }

            var user = _context.Users
                .FirstOrDefault(u => u.Username == username && u.Email == email && u.PhoneNumber == phoneNumber);

            if (user == null)
            {
                ViewBag.Error = "User not found or details do not match.";
                return View("PasswordReset");
            }

            // Update the user's password
            user.Password = password; // You should hash the password before saving to the database.
            _context.SaveChanges();

            return RedirectToAction("Login", "Accounts"); // Redirect to login page after successful reset
        }























    }
}

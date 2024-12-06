public IActionResult Index()
{

    using (var context = new MyDbContext())
    {
        //Display all the users
        Console.WriteLine("\n\nDisplay all the users");
        var users = context.Users.ToList();

        foreach (var user in users)
        {
            Console.WriteLine($"Username: {user.Username}, Name: {user.FirstName} {user.LastName}, Email: {user.Email}");
        }

        //Display all the users of type customers
        Console.WriteLine("\n\nDisplay all the users of type customers");
        var customers = context.Users.Where(u => u.UserType == "customer").ToList();
        foreach (var customer in customers)
        {
            Console.WriteLine($"Username: {customer.Username}, Name: {customer.FirstName} {customer.LastName}, Email: {customer.Email}");
        }

        //Display all the users with phoneno (projection)
        Console.WriteLine("\n\nDisplay all the users with phoneno (projection)");
        var userPhoneNumbers = context.Users
.Select(u => new { u.Username, u.PhoneNumber })
.ToList();

        foreach (var user in userPhoneNumbers)
        {
            Console.WriteLine($"Username: {user.Username}, Phone Number: {user.PhoneNumber}");
        }
    }
    return View();
}
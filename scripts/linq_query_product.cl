    public IActionResult Index()
    {
        using (var context = new MyDbContext())
        {
            //Get all products
            Console.Write("\n\nGet all products");
            var allProducts = context.Products.ToList();
            foreach (var product in allProducts)
            {
                Console.WriteLine($"Product: {product.ProductName}, Price: {product.Price}, Stock: {product.QuantityInStock}");
            }

            //Get products by category
            Console.Write("\n\nGet products by category");
            var electronicsProducts = context.Products
                               .Where(p => p.Category == "Makeup")
                               .ToList();
            foreach (var product in electronicsProducts)
            {
                Console.WriteLine($"Product: {product.ProductName}, Price: {product.Price}");
            }

            //Get products with price greater than a specific value
            Console.Write("\n\nGet products with price greater than 30");
            var expensiveProducts = context.Products
                             .Where(p => p.Price > 30)
                             .ToList();
            foreach (var product in expensiveProducts)
            {
                Console.WriteLine($"Product: {product.ProductName}, Price: {product.Price}");
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
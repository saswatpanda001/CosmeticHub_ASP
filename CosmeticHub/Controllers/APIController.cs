using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace CosmeticHub.Controllers
{
    public class APIController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public APIController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            // Retrieve the UserId from the session
            var userId = HttpContext.Session.GetInt32("UserID");

            // If UserId is not found, redirect to login page or handle as needed
            if (userId == null)
            {
                return RedirectToAction("Login", "Accounts");
            }

            var httpClient = _httpClientFactory.CreateClient();
            var apiUrl = "https://api.thenewsapi.com/v1/news/all?api_token=exzBTEt6mmhtJyunqz8PkM4dASEcuWEqKy90ZiFL&language=en&limit=5";
            var response = await httpClient.GetStringAsync(apiUrl);

            var json = JObject.Parse(response);
            var articles = json["data"];
            Console.Write(articles);

            return View(articles);
        }
    }
}

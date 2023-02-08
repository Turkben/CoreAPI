using Microsoft.AspNetCore.Mvc;

namespace CoreApi.WebApp.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

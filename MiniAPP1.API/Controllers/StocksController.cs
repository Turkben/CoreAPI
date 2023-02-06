using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MiniAPP1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StocksController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetStocks()
        {
            var userName = HttpContext.User.Identity.Name;
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            return Ok($"Api1-StocksController userName:{userName} - userId:{userId}");
        }
    }
}

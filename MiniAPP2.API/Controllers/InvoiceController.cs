using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MiniAPP2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InvoiceController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetInvoices()
        {
            var userName = HttpContext.User.Identity.Name;
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            return Ok($"Api2-InvoiceController:  userName:{userName} - userId:{userId}");
        }
    }
}

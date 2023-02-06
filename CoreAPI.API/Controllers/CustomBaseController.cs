using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Libary.Dtos;

namespace CoreAPI.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        [NonAction]
        public IActionResult CreateActionResult<T>(Response<T> response) where T : class 
        {
            if (response.StatusCode == 204)
            {
                return new ObjectResult(null)
                {
                    StatusCode = response.StatusCode,
                };
            }

            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }
    }
}

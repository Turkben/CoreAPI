using CoreAPI.Core.DTOs;
using CoreAPI.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : CustomBaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
        {
            return CreateActionResult(await _userService.CreateUserAsync(createUserDto));
        }
        
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            //var userName= HttpContext.User.Claims.Where(x => x.Type == "UserName").FirstOrDefault();
            return CreateActionResult(await _userService.GetUserByNameAsync(HttpContext.User.Identity.Name));
        }   
    }
}

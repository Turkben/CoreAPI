using CoreAPI.Core.DTOs;
using CoreAPI.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreAPI.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : CustomBaseController
    {

        private readonly IAuthenticationService _authService;

        public AuthController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken(LoginDto loginDto)
        {
            var result = await _authService.CreateTokenAsync(loginDto);
            return CreateActionResult(result);
        }

        [HttpPost]
        public IActionResult CreateTokenForClient(ClientLoginDto clientLoginDto)
        {
            var result = _authService.CreateTokenForClient(clientLoginDto);
            return CreateActionResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> RevokeRefreshToken(RefreshTokenDto refreshTokenDto)
        {
            var result = await _authService.RevokeRefreshTokenAsync(refreshTokenDto.Token);
            return CreateActionResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTokenByRefreshToken(RefreshTokenDto refreshTokenDto)
        {
            var result = await _authService.CreateTokenByRefreshTokenAsync(refreshTokenDto.Token);
            return CreateActionResult(result);
        }
    }
}

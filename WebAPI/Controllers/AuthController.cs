using Business.Abstract;
using Entities.DTOs.AuthDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            var result = await _authService.RegisterAsync(registerDTO);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var result = await _authService.LoginAsync(loginDTO);
            if(result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshTokenLogin(string refreshToken)
        {
            var result = await _authService.RefreshTokenLoginAsync(refreshToken);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("[action]/{userId}")]
        public async Task<IActionResult> AssignRoleToUser(string userId, string[] role)
        {
            var result = await _authService.AssignRoleToUserAsync(userId, role);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPut("[action]/{userId}")]
        public async Task<IActionResult> Logout(string userId)
        {
            var result = await _authService.LogoutAsync(userId);
            if(result.Success)
                return Ok(result);  
            return BadRequest(result);
        }

    }

}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SetoApi.Service;
using SetoClass.Models;

namespace SetoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AuthController(AuthService authService, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _authService = authService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // Kullanıcı kaydı (register) endpoint'i
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request.Email, request.Password, request.FirstName, request.LastName);
            if (result.Succeeded)
            {
                return Ok(new { Message = "User registered successfully!" });
            }

            return BadRequest(result.Errors);
        }

        // Kullanıcı girişi (login) endpoint'i
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _authService.LoginAsync(request.Email, request.Password);
            if (token == null)
            {
                return Unauthorized(new { Message = "Invalid email or password." });
            }

            return Ok(new { Token = token });
        }

        // Kullanıcıya rol atama endpoint'i
        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return NotFound(new { Message = "User not found." });
            }

            var roleExist = await _roleManager.RoleExistsAsync(request.RoleName);
            if (!roleExist)
            {
                return NotFound(new { Message = "Role not found." });
            }

            var result = await _userManager.AddToRoleAsync(user, request.RoleName);
            if (result.Succeeded)
            {
                return Ok(new { Message = "Role assigned successfully!" });
            }

            return BadRequest(result.Errors);
        }

        public class AssignRoleRequest
        {
            public string Email { get; set; }
            public string RoleName { get; set; }
        }
    }

    // Register için istek modeli
    public class RegisterRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    // Login için istek modeli
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

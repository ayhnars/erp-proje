using Microsoft.AspNetCore.Mvc;

namespace ErpApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        // POST: api/Account/Register
        [HttpPost("register")]
        public IActionResult Register()
        {
            // TODO: Add registration logic
            return Ok(new { message = "Registration successful." });
        }

        // POST: api/Account/Login
        [HttpPost("login")]
        public IActionResult Login()
        {
            // Kodları Ekleyin
            return Ok(new { token = "dummy-jwt-token" });
        }

        // POST: api/Account/ForgotPassword
        [HttpPost("forgotpassword")]
        public IActionResult ForgotPassword()
        {
            // Kodları Ekleyin
            return Ok(new { message = "Password reset link sent." });
        }
    }

}
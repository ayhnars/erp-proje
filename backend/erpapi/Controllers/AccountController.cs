using Microsoft.AspNetCore.Mvc;
using Services.Contrats;

namespace ErpApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthManager _authManager;

        public AccountController(IAuthManager authManager)
        {
            _authManager = authManager;
        }

        [HttpGet("roles")]
        public IActionResult GetRoles()
        {
            return Ok(_authManager.Roles);
        }
    }
}
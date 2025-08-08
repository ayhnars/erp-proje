using System.Security.Claims;
using AutoMapper;
using Entities;
using Entities.Dtos.UserDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Contrats;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<ErpUser> _userManager;
    private readonly IJwtHandler _jwtHandler;

    private readonly IAuthManager _authManager;
    private readonly IMapper _mapper;

    public AccountController(UserManager<ErpUser> userManager, IJwtHandler jwtHandler, IMapper mapper, IAuthManager authManager)
    {
        _userManager = userManager;
        _jwtHandler = jwtHandler;
        _mapper = mapper;
        _authManager = authManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] ErpUserDtoforRegister userDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = _mapper.Map<ErpUser>(userDto);
        user.UserName = userDto.Email;

        var result = await _userManager.CreateAsync(user, userDto.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        // Rol atamadan önce rollerin varlığını kontrol et veya startup'ta seed et
        string roleName = userDto.isBoss ? "Manager" : "Worker";


        var addRoleResult = await _userManager.AddToRoleAsync(user, roleName);
        if (!addRoleResult.Succeeded)
            return BadRequest(addRoleResult.Errors);

        return Ok(new { message = "Kullanıcı başarıyla oluşturuldu." });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] ErpUserDtoForLogin loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null)
            return Unauthorized(new { message = "Kullanıcı bulunamadı." });

        var passwordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        if (!passwordValid)
            return Unauthorized(new { message = "Geçersiz şifre." });

        var roles = await _userManager.GetRolesAsync(user);
        var tokenDto = _jwtHandler.CreateToken(user, roles);

        // Refresh token user objesine kaydet (eğer varsa)
        user.RefreshToken = tokenDto.RefreshToken;
        user.RefreshTokenExpiryDate = tokenDto.RefreshTokenExpiration;
        await _userManager.UpdateAsync(user);

        return Ok(tokenDto);
    }
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
            return BadRequest(new { message = "Kullanıcı kimliği bulunamadı." });

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return NotFound(new { message = "Kullanıcı bulunamadı." });

        user.RefreshToken = null;
        user.RefreshTokenExpiryDate = null;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Çıkış işlemi başarısız." });

        return Ok(new { message = "Başarıyla çıkış yapıldı." });
    }
    [HttpGet("roles")]
    public async Task<IActionResult> GetUserRoles()
    {
        var roles = _authManager.Roles;
        if (roles == null || !roles.Any())
            return NotFound(new { message = "Kullanıcı rolleri bulunamadı." });
        return Ok(roles);
    }
    [HttpGet("users")]
    public async Task<IActionResult> GetUsers()
    {
        var users = _authManager.GetAllUsers();
        if (users == null || !users.Any())
            return NotFound(new { message = "Kullanıcılar bulunamadı." });

        return Ok(users);
    }

}

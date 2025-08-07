using System.Security.Claims;
using AutoMapper;
using Entities;
using Entities.Dtos.UserDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Contrats;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ErpUser> _userManager;
    private readonly IJwtHandler _jwtHandler;

    private readonly IMapper _mapper;

    public AuthController(UserManager<ErpUser> userManager, IJwtHandler jwtHandler, IMapper mapper)
    {
        _userManager = userManager;
        _jwtHandler = jwtHandler;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] ErpUserDtoforRegister userDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // AutoMapper ile userDto'yu direkt ErpUser'a çeviriyoruz
        var user = _mapper.Map<ErpUser>(userDto);

        // Username'i de email olarak set et, bunu AutoMapper profiline ekleyebilirsin
        user.UserName = userDto.Email;

        var result = await _userManager.CreateAsync(user, userDto.Password);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

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
}

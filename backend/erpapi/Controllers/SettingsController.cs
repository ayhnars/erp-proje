using AutoMapper;
using Entities;
using Entities.Dtos.CompanyDtos;
using Entities.Dtos.UserDtos;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Contrats;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Kullanıcının giriş yapmış olması gerekir
public class SettingsController : ControllerBase
{
    private readonly UserManager<ErpUser> _userManager;
    private readonly IAuthManager _authManager; //AuthManager kullanıcı komutlarını yönetir
    private readonly IMapper _mapper; // Dtolar ile Entity'ler arasında dönüşüm yapar.

    private readonly ICompanyManager _companyManager; // Şirket bilgilerini yöneten servis

    public SettingsController(UserManager<ErpUser> userManager, IAuthManager authManager, IMapper mapper, ICompanyManager companyManager)
    {
        _userManager = userManager;
        _authManager = authManager;
        _mapper = mapper;
        _companyManager = companyManager;
    }

    // Kullanıcının mevcut bilgilerini getir
    [Authorize]
    [HttpGet("Profile")]
    public async Task<IActionResult> GetProfileSettings()
    {
        var userName = User.Identity?.Name; // JWT içinden gelir
        if (string.IsNullOrEmpty(userName))
            return Unauthorized();
        var user = await _authManager.GetOneUserForUpdate(userName);
        if (user == null)
            return NotFound();
        return Ok(user);
    }


    // Kullanıcının bilgilerini güncelle
    [Authorize]
    [HttpPut("Profile")]
    public async Task<IActionResult> UpdateProfileSettings([FromBody] ErpUserDtoForUpdate userDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userName = User.Identity?.Name; // JWT'den gelen username (email)
        if (string.IsNullOrEmpty(userName))
            return Unauthorized();

        await _authManager.Update(userDto);

        return Ok(new { message = "Ayarlar başarıyla güncellendi." });
    }

    [HttpGet("Company")]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> GetCompanySettings()
    {
        var userName = User.Identity?.Name;
        if (string.IsNullOrEmpty(userName))
            return Unauthorized();

        // Diyelim kullanıcıya bağlı şirket bilgisi user.CompanyId ile geliyor
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null)
            throw new KeyNotFoundException("Kullanıcı bulunamadı.");
        if (user.CompanyId == null)
        {
            throw new KeyNotFoundException("Kullanıcıya bağlı şirket bulunamadı.");
        }
        var company = await _companyManager.GetCompanyByIdAsync(user.CompanyId.Value);

        CompanyDtoForUpdate companyDto = _mapper.Map(company, new CompanyDtoForUpdate());

        if (company == null)
            throw new KeyNotFoundException("Şirket bulunamadı.");
        return Ok(companyDto);
    }

    // Şirket bilgilerini güncelle
    [HttpPut("Company")]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> UpdateCompanySettings([FromBody] CompanyDtoForUpdate companyDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userName = User.Identity?.Name;
        if (string.IsNullOrEmpty(userName))
            return Unauthorized();

        var user = await _userManager.FindByNameAsync(userName);
        if (user == null || user.CompanyId == null)
            return NotFound();

        if (user.CompanyId != companyDto.CompanyId)
            return BadRequest(new { message = "Kendi şirket bilgilerinizi güncelleyebilirsiniz." });

        var company = await _companyManager.GetCompanyByIdAsync(user.CompanyId.Value);

        _mapper.Map(companyDto, company);

        await _companyManager.UpdateCompanyAsync(company);

        return Ok(new { message = "Şirket bilgileri başarıyla güncellendi." });
    }

    [HttpPost("ChangePassword")]
    [Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userName = User.Identity?.Name;
        if (string.IsNullOrEmpty(userName))
            return Unauthorized();

        var user = await _userManager.FindByNameAsync(userName);
        if (user == null)
            return NotFound();
        if (changePasswordDto.NewPassword == changePasswordDto.OldPassword)
            return BadRequest(new { message = "Yeni şifre eski şifre ile aynı olamaz." });
        var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.OldPassword, changePasswordDto.NewPassword);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok(new { message = "Şifre başarıyla değiştirildi." });
    }
}

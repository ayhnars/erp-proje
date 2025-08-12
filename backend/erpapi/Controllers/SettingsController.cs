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
    private readonly IAuthManager _authManager; // Senin yazdığın AuthManager
    private readonly IMapper _mapper;

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

        var user = await _userManager.FindByNameAsync(userName);
        if (user == null)
            return NotFound();

        var userDto = _mapper.Map<ErpUserDtoForUpdate>(user);
        return Ok(userDto);
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

        // Kendi hesabını güncellesin
        userDto.Email = userName;

        await _authManager.UpdateAsync(userDto);
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
        if (user == null || user.CompanyId == null)
            return NotFound();

        var company = await _companyManager.GetCompanyByIdAsync(user.CompanyId.Value);
        if (company == null)
            return NotFound();

        var companyDto = _mapper.Map<CompanyDto>(company);
        return Ok(companyDto);
    }

    // Şirket bilgilerini güncelle
    [HttpPut("Company")]
    [Authorize(Roles = "Manager")]

    public async Task<IActionResult> UpdateCompanySettings([FromBody] CompanyDto companyDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userName = User.Identity?.Name;
        if (string.IsNullOrEmpty(userName))
            return Unauthorized();

        var user = await _userManager.FindByNameAsync(userName);
        if (user == null || user.CompanyId == null)
            return NotFound();

        if (user.CompanyId != companyDto.CompanyID)
            return BadRequest(new { message = "Kendi şirket bilgilerinizi güncelleyebilirsiniz." });

        var company = await _companyManager.GetCompanyByIdAsync(user.CompanyId.Value);

        _mapper.Map(companyDto, company);

        await _companyManager.UpdateCompanyAsync(company);

        return Ok(new { message = "Şirket bilgileri başarıyla güncellendi." });
    }
}

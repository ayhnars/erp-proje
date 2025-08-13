using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Services.Contrats;
using Entities;          // ErpUser
using Entities.Dtos;     // DTO'lar

namespace Services
{
    public class AuthManager : IAuthManager
    {
        private readonly UserManager<ErpUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthManager> _logger;

        public AuthManager(
            UserManager<ErpUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper,
            ILogger<AuthManager> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _logger = logger;
        }

        public IEnumerable<IdentityRole> Roles => _roleManager.Roles;

        public IEnumerable<ErpUser> GetAllUsers()
            => _userManager.Users.ToList();

        public async Task<ErpUser?> GetOneUser(string userName)
            => await _userManager.FindByNameAsync(userName);

        public async Task<ErpUserDtoForUpdate?> GetOneUserForUpdate(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return user is null ? null : _mapper.Map<ErpUserDtoForUpdate>(user);
        }

        public async Task<IdentityResult> CreateUser(ErpUserDtoForRegister userDto)
        {
            var user = _mapper.Map<ErpUser>(userDto);

            var result = await _userManager.CreateAsync(user, userDto.Password);
            if (!result.Succeeded) return result;

            const string defaultRole = "Employee";
            if (!await _roleManager.RoleExistsAsync(defaultRole))
                await _roleManager.CreateAsync(new IdentityRole(defaultRole));

            await _userManager.AddToRoleAsync(user, defaultRole);
            return result;
        }

        public async Task Update(ErpUserDtoForUpdate userDto)
        {
            var user = await _userManager.FindByNameAsync(userDto.UserName);
            if (user is null) return;

            _mapper.Map(userDto, user);
            await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> ResetPassword(ResetPasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
                return IdentityResult.Failed(new IdentityError { Description = "Kullanıcı bulunamadı." });

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return await _userManager.ResetPasswordAsync(user, token, model.ConfirmPassword);
        }

        public async Task<IdentityResult> DeleteOneUser(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user is null)
                return IdentityResult.Failed(new IdentityError { Description = "Kullanıcı bulunamadı." });

            return await _userManager.DeleteAsync(user);
        }
    }
}

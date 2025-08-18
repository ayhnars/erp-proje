using Entities;
using Entities.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Services.Contrats;
using AutoMapper;
using Entities.Dtos.UserDtos;

namespace Services
{
    public class AuthManager : IAuthManager
    {
        private readonly UserManager<ErpUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        private readonly IJwtHandler _jwtHandler;

        public AuthManager(UserManager<ErpUser> userManager,
                           RoleManager<IdentityRole> roleManager,
                           IMapper mapper,
                           IJwtHandler jwtHandler)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _jwtHandler = jwtHandler;
        }

        public IEnumerable<IdentityRole> Roles => _roleManager.Roles;

        public IEnumerable<ErpUser> GetAllUsers()
        {
            return _userManager.Users.ToList();
        }

        public async Task<ErpUser> GetOneUser(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<ErpUserDtoForUpdate> GetOneUserForUpdate(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                throw new Exception("Güncellenmek istenen kullanıcı bulunamadı.");
            return _mapper.Map<ErpUserDtoForUpdate>(user);
        }

        public async Task<IdentityResult> CreateUser(ErpUserDtoforRegister userDto)
        {
            var user = _mapper.Map<ErpUser>(userDto);
            var result = await _userManager.CreateAsync(user, userDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user,userDto.isBoss ?  "Manager" : "Employee"); // varsayılan rol
            }

            return result;
        }

        public async Task Update(ErpUserDtoForUpdate userDto)
        {
            var user = await _userManager.FindByNameAsync(userDto.Email);
            if (user == null) return;

            _mapper.Map(userDto, user);
            await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> ResetPassword(ResetPasswordDto model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "Kullanıcı bulunamadı." });

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
        }

        public async Task<IdentityResult> DeleteOneUser(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "Kullanıcı bulunamadı." });

            return await _userManager.DeleteAsync(user);
        }
        public async Task<TokenDto?> LoginAsync(ErpUserDtoForLogin loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                return null;

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isPasswordValid)
                return null;

            // Kullanıcının rollerini çekiyoruz
            var roles = await _userManager.GetRolesAsync(user);

            // Token oluşturuluyor
            var token = _jwtHandler.CreateToken(user, roles.ToList());

            // Refresh token da üretiliyor
            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpiryDate = token.RefreshTokenExpiration;
            await _userManager.UpdateAsync(user);

            return token;
        }
        public async Task<bool> LogoutAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return false;

            user.RefreshToken = null;
            user.RefreshTokenExpiryDate = null;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

    }
}

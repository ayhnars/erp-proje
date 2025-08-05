using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Entities.Dtos;
using Microsoft.AspNetCore.Identity;
using Services.Contrats;

namespace Services
{
    public class AuthManager : IAuthManager
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthManager(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IEnumerable<IdentityRole> Roles => _roleManager.Roles.ToList();

        public IEnumerable<ErpUser> GetAllUsers()
        {
            return _userManager.Users.ToList();
        }

        public async Task<ErpUser> GetOneUser(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<ErpUserForUpdate> GetOneUserForUpdate(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                return null;

            // Map IdentityUser to UserDtoForUpdate
            return new ErpUserForUpdate
            {
                UserName = user.UserName,
                Email = user.Email,
                // Add other properties as needed
            };
        }

        public async Task<IdentityResult> CreateUser(ErpUserDtoforRegister userDto)
        {
            var user = new IdentityUser
            {
                UserName = userDto.UserName,
                Email = userDto.Email
                // Add other properties as needed
            };

            var result = await _userManager.CreateAsync(user, userDto.Password);

            if (result.Succeeded && !string.IsNullOrEmpty(userDto.Role))
            {
                await _userManager.AddToRoleAsync(user, userDto.Role);
            }

            return result;
        }

        public async Task Update(UserDtoForUpdate userDto)
        {
            var user = await _userManager.FindByNameAsync(userDto.UserName);
            if (user != null)
            {
                user.Email = userDto.Email;
                // Update other properties as needed

                await _userManager.UpdateAsync(user);
            }
        }

        public async Task<IdentityResult> ResetPassword(ResetPasswordDto model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
        }

        public async Task<IdentityResult> DeleteOneUser(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });

            return await _userManager.DeleteAsync(user);
        }
    }
}

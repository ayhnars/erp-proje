using Entities;
using Entities.Dtos;
using Entities.Dtos.UserDtos;
using Microsoft.AspNetCore.Identity;

namespace Services.Contrats
{
    public interface IAuthManager
    {
        IEnumerable<IdentityRole> Roles { get; }
        IEnumerable<ErpUser> GetAllUsers();
        Task<ErpUser> GetOneUser(string userName);
        Task<ErpUserDtoForUpdate> GetOneUserForUpdate(string userName);
        Task<IdentityResult> CreateUser(ErpUserDtoforRegister userDto);
        Task Update(ErpUserDtoForUpdate userDto);
        Task<IdentityResult> ResetPassword(ResetPasswordDto model);
        Task<IdentityResult> DeleteOneUser(string userName);

        Task<TokenDto?> LoginAsync(ErpUserDtoForLogin loginDto);
        Task<bool> LogoutAsync(string userId);
    }
}

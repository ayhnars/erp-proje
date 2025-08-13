using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Entities;          // ErpUser
using Entities.Dtos;     // ErpUserDtoForRegister, ErpUserDtoForUpdate, ResetPasswordDto

namespace Services.Contrats
{
    public interface IAuthManager
    {
        IEnumerable<IdentityRole> Roles { get; }

        IEnumerable<ErpUser> GetAllUsers();

        Task<ErpUser?> GetOneUser(string userName);

        Task<ErpUserDtoForUpdate?> GetOneUserForUpdate(string userName);

        Task<IdentityResult> CreateUser(ErpUserDtoForRegister userDto);

        Task Update(ErpUserDtoForUpdate userDto);

        Task<IdentityResult> ResetPassword(ResetPasswordDto model);

        Task<IdentityResult> DeleteOneUser(string userName);
    }
}

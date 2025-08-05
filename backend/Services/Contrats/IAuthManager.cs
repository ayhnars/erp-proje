using System.Threading.Tasks;
using Entities.Dtos;

namespace Services.Contrats
{
    public interface IAuthManager
    { 
        IEnumerable<IdentityRole> Roles { get;}
        IEnumerable<IdentityUser> GetAllUsers();
        Task<IdentityUser> GetOneUser(string userName);
        Task<UserDtoForUpdate> GetOneUserForUpdate(string userName);
        Task<IdentityResult> CreateUser(ErpUserDtoforRegister userDto);
        Task Update(UserDtoForUpdate userDto);
        Task<IdentityResult> ResetPassword(ResetPasswordDto model);
        Task<IdentityResult> DeleteOneUser(string userName);
    }
}

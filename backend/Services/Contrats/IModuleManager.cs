using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Services.Contrats
{
    public interface IModuleManager
    {
        Task<IEnumerable<Modules>> GetAllModulesAsync();
        Task<Modules> GetModuleByIdAsync(int id);
        Task CreateModuleAsync(Modules module);
        Task UpdateModuleAsync(Modules module);
        Task DeleteModuleAsync(int id);
    }
}

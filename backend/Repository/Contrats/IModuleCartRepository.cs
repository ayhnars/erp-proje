using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Repository.Contrats
{
    public interface IModuleCartRepository
    {
        Task<ModuleCart?> GetByIdAsync(int id);
        Task<IEnumerable<ModuleCart>> GetByUserAsync(string userId);

        // Eksik olanlar:
        Task CreateAsync(ModuleCart entity);
        void Update(ModuleCart entity);
        void Delete(ModuleCart entity);
        Task SaveAsync();
    }
}

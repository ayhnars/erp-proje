using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;
using Services.Contrats;
using Repository;

namespace Services
{
    public class ModuleManager : IModuleManager
    {
        private readonly IRepositoryBase<Modules> _moduleRepository;

        public ModuleManager(IRepositoryBase<Modules> moduleRepository)
        {
            _moduleRepository = moduleRepository;
        }

        public async Task<IEnumerable<Modules>> GetAllModulesAsync()
        {
            return await _moduleRepository.GetAllAsync();
        }

        public async Task<Modules> GetModuleByIdAsync(int id)
        {
            return await _moduleRepository.GetByIdAsync(id);
        }

        public async Task CreateModuleAsync(Modules module)
        {
            _moduleRepository.Create(module);
            // Assuming RepositoryBase does not save changes, you may need to call SaveChangesAsync on your DbContext elsewhere
            await Task.CompletedTask;
        }

        public async Task UpdateModuleAsync(Modules module)
        {
            _moduleRepository.Update(module);
            // Assuming RepositoryBase does not save changes, you may need to call SaveChangesAsync on your DbContext elsewhere
            await Task.CompletedTask;
        }

        public async Task DeleteModuleAsync(int id)
        {
            var module = await _moduleRepository.GetByIdAsync(id);
            if (module != null)
            {
                _moduleRepository.Delete(module);
                // Assuming RepositoryBase does not save changes, you may need to call SaveChangesAsync on your DbContext elsewhere
            }
            await Task.CompletedTask;
        }
    }
}

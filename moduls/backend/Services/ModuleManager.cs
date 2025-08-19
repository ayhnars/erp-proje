using Entities;
using Repository;
using Services.Contrats;

namespace Services
{
    public class ModuleManager : IModuleManager
    {
        private readonly ModuleRepository _moduleRepository;

        public ModuleManager(ModuleRepository moduleRepository)
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
            await Task.CompletedTask;
        }

        public async Task UpdateModuleAsync(Modules module)
        {
            _moduleRepository.Update(module);
            await Task.CompletedTask;
        }

        public async Task DeleteModuleAsync(int id)
        {
            var module = await _moduleRepository.GetByIdAsync(id);
            if (module != null)
            {
                _moduleRepository.Delete(module);
            }
            await Task.CompletedTask;
        }
    }
}

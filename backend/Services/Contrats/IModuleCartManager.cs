using Entities.Dtos;

namespace Services.Contrats
{
    public interface IModuleCartManager
    {
        Task<ModuleCartDto> CreateAsync(ModuleCartForCreateDto dto);
        Task<ModuleCartDto?> GetAsync(int id);
        Task<IEnumerable<ModuleCartDto>> GetByUserAsync(string userId);
        Task UpdateAsync(int id, ModuleCartForUpdateDto dto);
        Task DeleteAsync(int id);
    }
}

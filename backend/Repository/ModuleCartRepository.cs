using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using Repository.Contrats;
using Repository;

namespace Repository
{
    public class ModuleCartRepository
        : RepositoryBase<ModuleCart>, IModuleCartRepository
    {
        private readonly RepositoryContext _context;

        public ModuleCartRepository(RepositoryContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ModuleCart?> GetByIdAsync(int id) =>
            await _context.ModuleCarts.AsNoTracking()
                                      .FirstOrDefaultAsync(mc => mc.CartID == id);

        public async Task<IEnumerable<ModuleCart>> GetByUserAsync(string userId) =>
            await _context.ModuleCarts.AsNoTracking()
                                      .Where(mc => mc.UserId == userId)
                                      .ToListAsync();

        // --- Yeni eklediklerimiz ---
        public async Task CreateAsync(ModuleCart entity)
        {
            await _context.ModuleCarts.AddAsync(entity);
        }

        public void Update(ModuleCart entity) => _context.ModuleCarts.Update(entity);

        public void Delete(ModuleCart entity) => _context.ModuleCarts.Remove(entity);

        public Task SaveAsync() => _context.SaveChangesAsync();
    }
}

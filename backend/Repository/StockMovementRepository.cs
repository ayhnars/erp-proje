using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repository;

namespace Repositories
{
    public class StockMovementRepository : RepositoryBase<StockMovement>, IStockMovementRepository
    {
        public StockMovementRepository(RepositoryContext context) : base(context) { }

        public async Task<IEnumerable<StockMovement>> GetAllStockMovementsAsync(bool trackChanges) =>
            await FindAll(trackChanges).ToListAsync();

        public async Task<StockMovement?> GetStockMovementAsync(int id, bool trackChanges) =>
            await FindByCondition(s => s.MovementID == id, trackChanges).SingleOrDefaultAsync();

        public void CreateStockMovement(StockMovement stockMovement) => Create(stockMovement);
        public void UpdateStockMovement(StockMovement stockMovement) => Update(stockMovement);
        public void DeleteStockMovement(StockMovement stockMovement) => Delete(stockMovement);
    }
}

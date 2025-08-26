using Entities.Models;
using Repositories.Contracts;
using Repository;
using Services.Contracts;

namespace Services
{
    public class StockMovementManager : IStockMovementManager
    {
        private readonly IRepositoryManager _repository;

        public StockMovementManager(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<StockMovement>> GetAllAsync() =>
            await _repository.StockMovement.GetAllStockMovementsAsync(false);

        public async Task<StockMovement?> GetByIdAsync(int id) =>
            await _repository.StockMovement.GetStockMovementAsync(id, false);

        public async Task CreateAsync(StockMovement stockMovement)
        {
            _repository.StockMovement.CreateStockMovement(stockMovement);
            await _repository.SaveAsync();
        }

        public async Task UpdateAsync(StockMovement stockMovement)
        {
            _repository.StockMovement.UpdateStockMovement(stockMovement);
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(StockMovement stockMovement)
        {
            _repository.StockMovement.DeleteStockMovement(stockMovement);
            await _repository.SaveAsync();
        }
    }
}

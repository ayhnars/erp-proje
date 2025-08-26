using Entities.Models;


namespace Repositories.Contracts
{
    public interface IStockMovementRepository
    {
        Task<IEnumerable<StockMovement>> GetAllStockMovementsAsync(bool trackChanges);
        Task<StockMovement?> GetStockMovementAsync(int id, bool trackChanges);
        void CreateStockMovement(StockMovement stockMovement);
        void UpdateStockMovement(StockMovement stockMovement);
        void DeleteStockMovement(StockMovement stockMovement);
    }
}

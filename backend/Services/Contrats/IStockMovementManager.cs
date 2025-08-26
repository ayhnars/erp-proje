using Entities.Models;


namespace Services.Contracts
{
    public interface IStockMovementManager
    {
        Task<IEnumerable<StockMovement>> GetAllAsync();
        Task<StockMovement?> GetByIdAsync(int id);
        Task CreateAsync(StockMovement stockMovement);
        Task UpdateAsync(StockMovement stockMovement);
        Task DeleteAsync(StockMovement stockMovement);
    }
}

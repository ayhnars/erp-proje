using Repositories.Contracts;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRepositoryManager
    {
        ICategoryRepository Category { get; }
        ICustomerRepository Customer { get; }
        IProductRepository Product { get; }
        IStockMovementRepository StockMovement { get; }

        Task SaveAsync();
        void Save();
    }
}

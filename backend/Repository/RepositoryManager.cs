using Entities;
using Repositories;
using Repositories.Contracts;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;

        private ICategoryRepository _category;
        private ICustomerRepository _customer;
        private IProductRepository _product;
        private IStockMovementRepository _stockMovement;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public ICategoryRepository Category => _category ??= new CategoryRepository(_repositoryContext);
        public ICustomerRepository Customer => _customer ??= new CustomerRepository(_repositoryContext);
        public IProductRepository Product => _product ??= new ProductRepository(_repositoryContext);
        public IStockMovementRepository StockMovement => _stockMovement ??= new StockMovementRepository(_repositoryContext);

        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
        public void Save() => _repositoryContext.SaveChanges();
    }
}

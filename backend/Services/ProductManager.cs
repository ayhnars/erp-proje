using Entities.Models;
using Repositories.Contracts;
using Repository;
using Services.Contracts;

namespace Services
{
    public class ProductManager : IProductManager
    {
        private readonly IRepositoryManager _repository;

        public ProductManager(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Product>> GetAllAsync() =>
            await _repository.Product.GetAllProductsAsync(false);

        public async Task<Product?> GetByIdAsync(int id) =>
            await _repository.Product.GetProductAsync(id, false);

        public async Task CreateAsync(Product product)
        {
            _repository.Product.CreateProduct(product);
            await _repository.SaveAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _repository.Product.UpdateProduct(product);
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            _repository.Product.DeleteProduct(product);
            await _repository.SaveAsync();
        }
    }
}

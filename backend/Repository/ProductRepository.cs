using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repository;

namespace Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(RepositoryContext context) : base(context) { }

        public async Task<IEnumerable<Product>> GetAllProductsAsync(bool trackChanges) =>
            await FindAll(trackChanges).ToListAsync();

        public async Task<Product?> GetProductAsync(int id, bool trackChanges) =>
            await FindByCondition(p => p.ProductID == id, trackChanges).SingleOrDefaultAsync();

        public void CreateProduct(Product product) => Create(product);
        public void UpdateProduct(Product product) => Update(product);
        public void DeleteProduct(Product product) => Delete(product);
    }
}

using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repository;

namespace Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext context) : base(context) { }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges) =>
            await FindAll(trackChanges).ToListAsync();

        public async Task<Category?> GetCategoryAsync(int id, bool trackChanges) =>
            await FindByCondition(c => c.CategoryID == id, trackChanges).SingleOrDefaultAsync();

        public void CreateCategory(Category category) => Create(category);
        public void UpdateCategory(Category category) => Update(category);
        public void DeleteCategory(Category category) => Delete(category);
    }
}

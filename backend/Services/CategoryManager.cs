using Entities.Models;
using Repositories.Contracts;
using Repository;
using Services.Contracts;

namespace Services
{
    public class CategoryManager : ICategoryManager
    {
        private readonly IRepositoryManager _repository;

        public CategoryManager(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Category>> GetAllAsync() =>
            await _repository.Category.GetAllCategoriesAsync(false);

        public async Task<Category?> GetByIdAsync(int id) =>
            await _repository.Category.GetCategoryAsync(id, false);

        public async Task CreateAsync(Category category)
        {
            _repository.Category.CreateCategory(category);
            await _repository.SaveAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _repository.Category.UpdateCategory(category);
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            _repository.Category.DeleteCategory(category);
            await _repository.SaveAsync();
        }
    }
}

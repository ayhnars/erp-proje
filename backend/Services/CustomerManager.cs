using Entities.Models;
using Repositories.Contracts;
using Repository;
using Services.Contracts;

namespace Services
{
    public class CustomerManager : ICustomerManager
    {
        private readonly IRepositoryManager _repository;

        public CustomerManager(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync() =>
            await _repository.Customer.GetAllCustomersAsync(false);

        public async Task<Customer?> GetByIdAsync(int id) =>
            await _repository.Customer.GetCustomerAsync(id, false);

        public async Task CreateAsync(Customer customer)
        {
            _repository.Customer.CreateCustomer(customer);
            await _repository.SaveAsync();
        }

        public async Task UpdateAsync(Customer customer)
        {
            _repository.Customer.UpdateCustomer(customer);
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(Customer customer)
        {
            _repository.Customer.DeleteCustomer(customer);
            await _repository.SaveAsync();
        }
    }
}

using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repository;

namespace Repositories
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(RepositoryContext context) : base(context) { }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync(bool trackChanges) =>
            await FindAll(trackChanges).ToListAsync();

        public async Task<Customer?> GetCustomerAsync(int id, bool trackChanges) =>
            await FindByCondition(c => c.CustomerID == id, trackChanges).SingleOrDefaultAsync();

        public void CreateCustomer(Customer customer) => Create(customer);
        public void UpdateCustomer(Customer customer) => Update(customer);
        public void DeleteCustomer(Customer customer) => Delete(customer);
    }
}

using Entities;
using Entities.Models;
using Repository;
using System.Threading.Tasks;

namespace Repository.Contrats
{
    public interface ICompanyRepository : IRepositoryBase<Company>
    {
        Task<Company> GetCompanyByNameAsync(string companyName);
    }
}

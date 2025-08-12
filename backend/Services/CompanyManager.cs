using Services.Contrats;
using Entities;
using Entities.Models;
using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using Repository.Contrats;

namespace Services
{

    public class CompanyManager : ICompanyManager
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyManager(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<List<Company>> GetAllCompaniesAsync()
        {
            return await _companyRepository.GetAllAsync();
        }

        public async Task<Company> GetCompanyByIdAsync(int id)
        {
            return await _companyRepository.GetByIdAsync(id);
        }

        public async Task<Company> GetCompanyByNameAsync(string name)
        {
            return await _companyRepository.GetCompanyByNameAsync(name);
        }

        public async Task CreateCompanyAsync(Company company)
        {
            _companyRepository.Create(company);
        }

        public async Task UpdateCompanyAsync(Company company)
        {
            _companyRepository.Update(company);
        }

        public async Task DeleteCompanyAsync(int id)
        {
            var company = await _companyRepository.GetByIdAsync(id);
            if (company != null)
            {
                _companyRepository.Delete(company);
            }
        }
    }
}

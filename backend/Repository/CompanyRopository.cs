using Entities;  // Company modeli burda
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Contrats;
using System.Threading.Tasks;

namespace Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext context) : base(context)
        {
        }

        // İstersen buraya Company'ye özel metodlar ekleyebilirsin
        // Örneğin spesifik filtreleme, özel sorgular vb.

        // Örnek: Company adına göre arama
        public async Task<Company> GetCompanyByNameAsync(string companyName)
        {
            var company = await FindByCondition(c => c.CompanyName == companyName)
                .FirstOrDefaultAsync();
            if (company == null)
            {
                // Şirket bulunamadı
                throw new KeyNotFoundException($"Company with name {companyName} not found.");
            }
            return company;
        }
    }
}

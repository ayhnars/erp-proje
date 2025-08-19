using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Repository
{
    // Tüm repo'larýnýz RepositoryContext kullanýyorsa tip güvenliði için DbContext yerine bunu kullanmak daha iyi.
    public abstract class RepositoryBase<T> where T : class
    {
        protected readonly RepositoryContext _context;

        protected RepositoryBase(RepositoryContext context)
        {
            _context = context;
        }

        // Sorgu metodlarý (NoTracking)
        public IQueryable<T> FindAll()
            => _context.Set<T>().AsNoTracking();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> predicate)
            => _context.Set<T>().Where(predicate).AsNoTracking();

        // Deðiþiklik metodlarý (Save'i bilerek burada yapmýyoruz; UnitOfWork/Service tarafýnda çaðrýlýr)
        public void Create(T entity) => _context.Set<T>().Add(entity);
        public void Update(T entity) => _context.Set<T>().Update(entity);
        public void Delete(T entity) => _context.Set<T>().Remove(entity);

        // Async yardýmcýlar
        public async Task<List<T>> GetAllAsync()
            => await _context.Set<T>().AsNoTracking().ToListAsync();

        // Bulamazsa null dönebilir -> T? kullan
        public async Task<T?> GetByIdAsync(object id)
            => await _context.Set<T>().FindAsync(id);

        // Ýstersen ortak Save
        public Task<int> SaveChangesAsync()
            => _context.SaveChangesAsync();
    }
}

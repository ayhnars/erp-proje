using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Repository
{
    // T�m repo'lar�n�z RepositoryContext kullan�yorsa tip g�venli�i i�in DbContext yerine bunu kullanmak daha iyi.
    public abstract class RepositoryBase<T> where T : class
    {
        protected readonly RepositoryContext _context;

        protected RepositoryBase(RepositoryContext context)
        {
            _context = context;
        }

        // Sorgu metodlar� (NoTracking)
        public IQueryable<T> FindAll()
            => _context.Set<T>().AsNoTracking();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> predicate)
            => _context.Set<T>().Where(predicate).AsNoTracking();

        // De�i�iklik metodlar� (Save'i bilerek burada yapm�yoruz; UnitOfWork/Service taraf�nda �a�r�l�r)
        public void Create(T entity) => _context.Set<T>().Add(entity);
        public void Update(T entity) => _context.Set<T>().Update(entity);
        public void Delete(T entity) => _context.Set<T>().Remove(entity);

        // Async yard�mc�lar
        public async Task<List<T>> GetAllAsync()
            => await _context.Set<T>().AsNoTracking().ToListAsync();

        // Bulamazsa null d�nebilir -> T? kullan
        public async Task<T?> GetByIdAsync(object id)
            => await _context.Set<T>().FindAsync(id);

        // �stersen ortak Save
        public Task<int> SaveChangesAsync()
            => _context.SaveChangesAsync();
    }
}

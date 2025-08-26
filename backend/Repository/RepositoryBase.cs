using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Entities;

namespace Repository
{
    // Tüm repo'larınız RepositoryContext kullanıyorsa tip güvenliği için DbContext yerine bunu kullanmak daha iyi.
    public abstract class RepositoryBase<T> where T : class
    {
        protected readonly RepositoryContext _context;


        public RepositoryBase(RepositoryContext repositoryContext)
        {
            _context = repositoryContext;
        }

        // Sorgu metodları (NoTracking)
        public IQueryable<T> FindAll()
            => _context.Set<T>().AsNoTracking();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> predicate)
            => _context.Set<T>().Where(predicate).AsNoTracking();

        // Değişiklik metodları (Save'i bilerek burada yapmıyoruz; UnitOfWork/Service tarafında çağrılır)
        public void Create(T entity) => _context.Set<T>().Add(entity);
        public void Update(T entity) => _context.Set<T>().Update(entity);
        public void Delete(T entity) => _context.Set<T>().Remove(entity);

        // Async yardımcılar
        public async Task<List<T>> GetAllAsync()
            => await _context.Set<T>().AsNoTracking().ToListAsync();

        // Bulamazsa null dönebilir -> T? kullan
        public async Task<T?> GetByIdAsync(object id)
            => await _context.Set<T>().FindAsync(id);

        // İstersen ortak Save
        public Task<int> SaveChangesAsync()
            => _context.SaveChangesAsync();
    }
}

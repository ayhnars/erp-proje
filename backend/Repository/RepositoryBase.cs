using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    // Tüm repository'lerin ortak tabanı
    public abstract class RepositoryBase<T> where T : class
    {
        protected readonly RepositoryContext _context;

        protected RepositoryBase(RepositoryContext context)
        {
            _context = context;
        }

        // Sorgular (NoTracking)
        public IQueryable<T> FindAll()
            => _context.Set<T>().AsNoTracking();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> predicate)
            => _context.Set<T>().Where(predicate).AsNoTracking();

        // Değişiklikler
        public void Create(T entity) => _context.Set<T>().Add(entity);
        public void Update(T entity) => _context.Set<T>().Update(entity);
        public void Delete(T entity) => _context.Set<T>().Remove(entity);

        // Async yardımcılar
        public async Task<List<T>> GetAllAsync()
            => await _context.Set<T>().AsNoTracking().ToListAsync();

        public async Task<T?> GetByIdAsync(object id)
            => await _context.Set<T>().FindAsync(id);

        public Task<int> SaveChangesAsync()
            => _context.SaveChangesAsync();
    }
}

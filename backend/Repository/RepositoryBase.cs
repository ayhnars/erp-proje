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
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext _context { get; set; }

<<<<<<< HEAD
=======

>>>>>>> dev
        public RepositoryBase(RepositoryContext repositoryContext)
        {
            _context = repositoryContext;
        }

        public IQueryable<T> FindAll()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression).AsNoTracking();
        }

        // Değişiklik metodları (Save'i bilerek burada yapmıyoruz; UnitOfWork/Service tarafında çağrılır)
        public void Create(T entity) => _context.Set<T>().Add(entity);
        public void Update(T entity) => _context.Set<T>().Update(entity);
        public void Delete(T entity) => _context.Set<T>().Remove(entity);

        // Async yardımcılar
        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        // Bulamazsa null dönebilir -> T? kullan
        public async Task<T?> GetByIdAsync(object id)
            => await _context.Set<T>().FindAsync(id);

        // İstersen ortak Save
        public Task<int> SaveChangesAsync()
            => _context.SaveChangesAsync();
    }
}

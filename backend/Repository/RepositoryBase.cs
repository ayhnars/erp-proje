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
<<<<<<< HEAD
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
=======
    // Tüm repo'larınız RepositoryContext kullanıyorsa tip güvenliği için DbContext yerine bunu kullanmak daha iyi.
    public abstract class RepositoryBase<T> where T : class
>>>>>>> 8ca21fc (Ignore build outputs; remove bin/obj and resolve merge noise)
    {
        protected RepositoryContext RepositoryContext { get; set; }

<<<<<<< HEAD
=======
        protected RepositoryBase(RepositoryContext context)
>>>>>>> 8ca21fc (Ignore build outputs; remove bin/obj and resolve merge noise)
        public RepositoryBase(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

<<<<<<< HEAD
=======
        // Sorgu metodları (NoTracking)
>>>>>>> 8ca21fc (Ignore build outputs; remove bin/obj and resolve merge noise)
        public IQueryable<T> FindAll()
        {
            return RepositoryContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return RepositoryContext.Set<T>().Where(expression).AsNoTracking();
        }

<<<<<<< HEAD
        public void Create(T entity)
        {
            RepositoryContext.Set<T>().Add(entity);
            // Eğer DbContext'in değişikliklerini hemen kaydetmek istiyorsan
            RepositoryContext.SaveChanges();
        }

        public void Update(T entity)
        {
            RepositoryContext.Set<T>().Update(entity);
            RepositoryContext.SaveChanges();

        }

        public void Delete(T entity)
        {
            RepositoryContext.Set<T>().Remove(entity);
            RepositoryContext.SaveChanges();

        }

=======
        // Değişiklik metodları (Save'i bilerek burada yapmıyoruz; UnitOfWork/Service tarafında çağrılır)
        public void Create(T entity) => _context.Set<T>().Add(entity);
        public void Update(T entity) => _context.Set<T>().Update(entity);
        public void Delete(T entity) => _context.Set<T>().Remove(entity);

        // Async yardımcılar
>>>>>>> 8ca21fc (Ignore build outputs; remove bin/obj and resolve merge noise)
        public async Task<List<T>> GetAllAsync()
        {
            return await RepositoryContext.Set<T>().ToListAsync();
        }

<<<<<<< HEAD
        public async Task<T> GetByIdAsync(object id)
        {
            return await RepositoryContext.Set<T>().FindAsync(id);
        }
=======
        // Bulamazsa null dönebilir -> T? kullan
        public async Task<T?> GetByIdAsync(object id)
            => await _context.Set<T>().FindAsync(id);

        // İstersen ortak Save
        public Task<int> SaveChangesAsync()
            => _context.SaveChangesAsync();
>>>>>>> 8ca21fc (Ignore build outputs; remove bin/obj and resolve merge noise)
    }
}

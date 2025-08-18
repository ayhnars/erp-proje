using System;
using Repository;

namespace Repository
{
    public class RepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        // Gerektiğinde repository özelliklerini buraya ekleyin, örneğin:
        // public IUserRepository UserRepository => new UserRepository(_repositoryContext);

        public void Save()
        {
            _repositoryContext.SaveChanges();
        }
    }
}

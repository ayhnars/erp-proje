using Entities;

namespace Repository
{
    public class ModuleRepository : RepositoryBase<Modules>, IRepositoryBase<Modules>
    {
        public ModuleRepository(RepositoryContext context) : base(context)
        {
        }

    }
}

using System.Linq.Expressions;


public interface IRepositoryBase<T> where T : class
{
    IQueryable<T> FindAll();
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);

    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(object id);  // <-- T? olmalý (null dönebilir)
}

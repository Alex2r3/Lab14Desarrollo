using System.Linq.Expressions;

namespace Lab13_2_AlexanderVasquez.Domain.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> GetById(int id);
    Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
    IQueryable<T> Query();
    Task Add(T entity);
    Task<T> AddAndSave(T entity);
    void Update(T entity);
    Task<T> UpdateAndSave(T entity);
    Task Delete(int id);
    void Delete(T entity);
}

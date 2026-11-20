using System.Linq.Expressions;
using Lab13_2_AlexanderVasquez.Domain.Repositories;
using Lab13_2_AlexanderVasquez.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Lab13_2_AlexanderVasquez.Infrastructure.Adapters.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAll() => await _dbSet.ToListAsync();

    public async Task<T?> GetById(int id) => await _dbSet.FindAsync(id);

    public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate) => 
        await _dbSet.Where(predicate).ToListAsync();

    public IQueryable<T> Query() => _dbSet.AsQueryable();

    public async Task Add(T entity) => await _dbSet.AddAsync(entity);

    public async Task<T> AddAndSave(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public void Update(T entity) => _dbSet.Update(entity);

    public async Task<T> UpdateAndSave(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task Delete(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
        }
    }

    public void Delete(T entity) => _dbSet.Remove(entity);
}

using System.Collections;
using Lab13_2_AlexanderVasquez.Domain.Models;
using Lab13_2_AlexanderVasquez.Domain.Repositories;
using Lab13_2_AlexanderVasquez.Infrastructure.Data;

namespace Lab13_2_AlexanderVasquez.Infrastructure.Adapters.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private Hashtable? _repositories;

    private IClientRepository? _clients;
    private IProductRepository? _products;
    private IOrderRepository? _orders;
    private IOrderdetailRepository? _orderdetails;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IClientRepository Clients => _clients ??= new ClientRepository(_context);
    public IProductRepository Products => _products ??= new ProductRepository(_context);
    public IOrderRepository Orders => _orders ??= new OrderRepository(_context);
    public IOrderdetailRepository Orderdetails => _orderdetails ??= new OrderdetailRepository(_context);

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        _repositories ??= new Hashtable();

        var type = typeof(TEntity).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(GenericRepository<>);
            var repositoryInstance = Activator.CreateInstance(
                repositoryType.MakeGenericType(typeof(TEntity)), _context);

            _repositories.Add(type, repositoryInstance);
        }

        return (IGenericRepository<TEntity>)_repositories[type]!;
    }

    public async Task<int> Complete()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}

using Lab13_2_AlexanderVasquez.Domain.Models;

namespace Lab13_2_AlexanderVasquez.Domain.Repositories;

public interface IUnitOfWork : IDisposable
{
    IClientRepository Clients { get; }
    IProductRepository Products { get; }
    IOrderRepository Orders { get; }
    IOrderdetailRepository Orderdetails { get; }

    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
    Task<int> Complete();
}

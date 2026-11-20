using Lab13_2_AlexanderVasquez.Domain.Models;

namespace Lab13_2_AlexanderVasquez.Domain.Repositories;

public interface IOrderdetailRepository : IGenericRepository<Orderdetail>
{
    Task<object> GetDetailsByOrderId(int orderId);
    Task<int> GetTotalQuantityByOrderId(int orderId);
}

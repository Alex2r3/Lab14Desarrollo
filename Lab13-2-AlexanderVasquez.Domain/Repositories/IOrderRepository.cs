using Lab13_2_AlexanderVasquez.Domain.Models;

namespace Lab13_2_AlexanderVasquez.Domain.Repositories;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task<IEnumerable<Order>> GetOrdersAfterDate(DateTime date);
    Task<object> GetAllOrdersWithDetails();
}

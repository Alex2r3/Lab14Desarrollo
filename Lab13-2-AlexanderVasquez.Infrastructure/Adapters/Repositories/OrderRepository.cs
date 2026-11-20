using Lab13_2_AlexanderVasquez.Domain.Models;
using Lab13_2_AlexanderVasquez.Domain.Repositories;
using Lab13_2_AlexanderVasquez.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Lab13_2_AlexanderVasquez.Infrastructure.Adapters.Repositories;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Order>> GetOrdersAfterDate(DateTime date)
    {
        return await _context.Orders
            .Where(o => o.Orderdate > date)
            .ToListAsync();
    }

    public async Task<object> GetAllOrdersWithDetails()
    {
        return await _context.Orderdetails
            .Select(od => new
            {
                od.Orderid,
                ProductName = od.Product.Name,
                od.Quantity
            })
            .ToListAsync();
    }
}

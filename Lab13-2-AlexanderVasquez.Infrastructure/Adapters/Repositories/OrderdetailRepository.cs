using Lab13_2_AlexanderVasquez.Domain.Models;
using Lab13_2_AlexanderVasquez.Domain.Repositories;
using Lab13_2_AlexanderVasquez.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Lab13_2_AlexanderVasquez.Infrastructure.Adapters.Repositories;

public class OrderdetailRepository : GenericRepository<Orderdetail>, IOrderdetailRepository
{
    public OrderdetailRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<object> GetDetailsByOrderId(int orderId)
    {
        return await _context.Orderdetails
            .Where(od => od.Orderid == orderId)
            .Select(od => new
            {
                ProductName = od.Product.Name,
                od.Quantity
            })
            .ToListAsync();
    }

    public async Task<int> GetTotalQuantityByOrderId(int orderId)
    {
        return await _context.Orderdetails
            .Where(od => od.Orderid == orderId)
            .Select(od => od.Quantity)
            .SumAsync();
    }
}

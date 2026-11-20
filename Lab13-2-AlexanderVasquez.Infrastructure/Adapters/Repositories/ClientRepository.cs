using Lab13_2_AlexanderVasquez.Domain.Models;
using Lab13_2_AlexanderVasquez.Domain.Repositories;
using Lab13_2_AlexanderVasquez.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Lab13_2_AlexanderVasquez.Infrastructure.Adapters.Repositories;

public class ClientRepository : GenericRepository<Client>, IClientRepository
{
    public ClientRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Client>> GetByName(string name)
    {
        return await _context.Clients
            .Where(c => c.Name.ToLower().Contains(name.ToLower()))
            .ToListAsync();
    }

    public async Task<object?> GetClientWithMostOrders()
    {
        return await _context.Orders
            .GroupBy(o => o.Clientid)
            .OrderByDescending(g => g.Count())
            .Select(g => new { ClientId = g.Key, TotalPedidos = g.Count() })
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<string>> GetProductsSoldToClient(int clientId)
    {
        return await _context.Orders
            .Where(o => o.Clientid == clientId)
            .SelectMany(o => o.Orderdetails)
            .Select(od => od.Product.Name)
            .Distinct()
            .ToListAsync();
    }
}

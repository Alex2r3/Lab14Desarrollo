using Lab13_2_AlexanderVasquez.Domain.Models;

namespace Lab13_2_AlexanderVasquez.Domain.Repositories;

public interface IClientRepository : IGenericRepository<Client>
{
    Task<IEnumerable<Client>> GetByName(string name);
    Task<object?> GetClientWithMostOrders();
    Task<IEnumerable<string>> GetProductsSoldToClient(int clientId);
}

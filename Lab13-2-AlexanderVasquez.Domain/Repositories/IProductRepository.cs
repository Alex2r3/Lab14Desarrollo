using Lab13_2_AlexanderVasquez.Domain.Models;

namespace Lab13_2_AlexanderVasquez.Domain.Repositories;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<IEnumerable<Product>> GetByPriceGreaterThan(decimal price);
    Task<Product?> GetMostExpensiveProduct();
    Task<decimal> GetAveragePrice();
    Task<IEnumerable<Product>> GetProductsWithoutDescription();
    Task<IEnumerable<string>> GetClientsWhoBoughtProduct(int productId);
}

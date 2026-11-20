using Lab13_2_AlexanderVasquez.Domain.Models;
using Lab13_2_AlexanderVasquez.Domain.Repositories;
using Lab13_2_AlexanderVasquez.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Lab13_2_AlexanderVasquez.Infrastructure.Adapters.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Product>> GetByPriceGreaterThan(decimal price)
    {
        return await _context.Products
            .Where(p => p.Price > price)
            .ToListAsync();
    }

    public async Task<Product?> GetMostExpensiveProduct()
    {
        return await _context.Products
            .OrderByDescending(p => p.Price)
            .FirstOrDefaultAsync();
    }

    public async Task<decimal> GetAveragePrice()
    {
        if (!await _context.Products.AnyAsync()) return 0;
        return await _context.Products.AverageAsync(p => p.Price);
    }

    public async Task<IEnumerable<Product>> GetProductsWithoutDescription()
    {
        return await _context.Products
            .Where(p => string.IsNullOrEmpty(p.Description))
            .ToListAsync();
    }

    public async Task<IEnumerable<string>> GetClientsWhoBoughtProduct(int productId)
    {
        return await _context.Orderdetails
            .Where(od => od.Productid == productId)
            .Select(od => od.Order.Client.Name)
            .Distinct()
            .ToListAsync();
    }
}

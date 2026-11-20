using Lab13_2_AlexanderVasquez.Application.DTOs.Products;

namespace Lab13_2_AlexanderVasquez.Application.DTOs.Orders;

public class OrderDto
{
    public int OrderId { get; set; }
    public int? ClientId { get; set; }
    public DateTime OrderDate { get; set; }
}

public class CreateOrderDto
{
    public int ClientId { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
}

public class OrderDetailsDto
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public List<ProductDto> Products { get; set; } = new();
}

public class SalesByClientDto
{
    public string ClientName { get; set; } = string.Empty;
    public decimal TotalSales { get; set; }
}

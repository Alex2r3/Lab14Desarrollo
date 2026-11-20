namespace Lab13_2_AlexanderVasquez.Application.DTOs.Products;

public class ProductDto
{
    public int? ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

public class CreateProductDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
}

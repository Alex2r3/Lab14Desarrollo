namespace Lab13_2_AlexanderVasquez.Application.DTOs.Reports;

public class SalesByClientReportItemDto
{
    public int ClientId { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int TotalOrders { get; set; }
    public int TotalProductsPurchased { get; set; }
    public decimal TotalSpent { get; set; }
}

public class ProductPerformanceReportItemDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int TotalUnitsSold { get; set; }
    public decimal TotalRevenue { get; set; }
    public int UniqueBuyersCount { get; set; }
}

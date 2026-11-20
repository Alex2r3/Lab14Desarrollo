namespace Lab13_2_AlexanderVasquez.Application.DTOs.Clients;

public class ClientDto
{
    public int? Clientid { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public int? TotalProducts { get; set; }
}

public class CreateClientDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class ClientOrderDto
{
    public string ClientName { get; set; } = string.Empty;
    public List<Lab13_2_AlexanderVasquez.Application.DTOs.Orders.OrderDto> Orders { get; set; } = new();
}

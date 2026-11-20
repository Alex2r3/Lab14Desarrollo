namespace Lab13_2_AlexanderVasquez.Application.DTOs.Orderdetails;

public class OrderdetailDto
{
    public int Orderdetailid { get; set; }
    public int Orderid { get; set; }
    public int Productid { get; set; }
    public int Quantity { get; set; }
    public string? ProductName { get; set; }
}

public class CreateOrderdetailDto
{
    public int Orderid { get; set; }
    public int Productid { get; set; }
    public int Quantity { get; set; }
}

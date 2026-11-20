using MediatR;
using Lab13_2_AlexanderVasquez.Application.DTOs.Orders;
using Lab13_2_AlexanderVasquez.Application.DTOs.Products;
using Lab13_2_AlexanderVasquez.Domain.Models;
using Lab13_2_AlexanderVasquez.Domain.Repositories;

namespace Lab13_2_AlexanderVasquez.Application.UseCases.Orders.Queries;

public sealed record GetAllOrdersQuery : IRequest<IEnumerable<OrderDto>>;

internal sealed class GetAllOrdersQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderDto>>
{
    public async Task<IEnumerable<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await unitOfWork.Orders.GetAll();
        return orders.Select(o => new OrderDto
        {
            OrderId = o.Orderid,
            ClientId = o.Clientid,
            OrderDate = o.Orderdate
        }).ToList();
    }
}

public sealed record GetOrderByIdQuery(int Id) : IRequest<OrderDto?>;

internal sealed class GetOrderByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetOrderByIdQuery, OrderDto?>
{
    public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var o = await unitOfWork.Orders.GetById(request.Id);
        if (o == null) return null;
        return new OrderDto
        {
            OrderId = o.Orderid,
            ClientId = o.Clientid,
            OrderDate = o.Orderdate
        };
    }
}

public sealed record GetOrdersAfterDateQuery(DateTime Date) : IRequest<IEnumerable<OrderDto>>;

internal sealed class GetOrdersAfterDateQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetOrdersAfterDateQuery, IEnumerable<OrderDto>>
{
    public async Task<IEnumerable<OrderDto>> Handle(GetOrdersAfterDateQuery request, CancellationToken cancellationToken)
    {
        var orders = await unitOfWork.Orders.GetOrdersAfterDate(request.Date);
        return orders.Select(o => new OrderDto
        {
            OrderId = o.Orderid,
            ClientId = o.Clientid,
            OrderDate = o.Orderdate
        }).ToList();
    }
}

public sealed record GetAllOrdersWithDetailsQuery : IRequest<object>;

internal sealed class GetAllOrdersWithDetailsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllOrdersWithDetailsQuery, object>
{
    public async Task<object> Handle(GetAllOrdersWithDetailsQuery request, CancellationToken cancellationToken)
    {
        return await unitOfWork.Orders.GetAllOrdersWithDetails();
    }
}

public sealed record GetOrdersWithIncludeQuery : IRequest<IEnumerable<OrderDetailsDto>>;

internal sealed class GetOrdersWithIncludeQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetOrdersWithIncludeQuery, IEnumerable<OrderDetailsDto>>
{
    public Task<IEnumerable<OrderDetailsDto>> Handle(GetOrdersWithIncludeQuery request, CancellationToken cancellationToken)
    {
        var ordersWithDetails = unitOfWork.Repository<Order>().Query()
            .Select(order => new OrderDetailsDto
            {
                OrderId = order.Orderid,
                OrderDate = order.Orderdate,
                Products = order.Orderdetails.Select(od => new ProductDto
                {
                    ProductId = od.Productid,
                    ProductName = od.Product.Name,
                    Quantity = od.Quantity,
                    Price = od.Product.Price
                }).ToList()
            }).ToList();

        return Task.FromResult<IEnumerable<OrderDetailsDto>>(ordersWithDetails);
    }
}

public sealed record GetSalesByClientQuery : IRequest<IEnumerable<SalesByClientDto>>;

internal sealed class GetSalesByClientQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetSalesByClientQuery, IEnumerable<SalesByClientDto>>
{
    public Task<IEnumerable<SalesByClientDto>> Handle(GetSalesByClientQuery request, CancellationToken cancellationToken)
    {
        var salesByClient = unitOfWork.Repository<Order>().Query()
            .GroupBy(order => order.Clientid)
            .Select(group => new SalesByClientDto
            {
                ClientName = unitOfWork.Repository<Client>().Query()
                    .Where(c => c.Clientid == group.Key)
                    .Select(c => c.Name)
                    .FirstOrDefault() ?? "Desconocido",
                TotalSales = group.Sum(order => order.Orderdetails
                    .Sum(detail => detail.Quantity * detail.Product.Price))
            })
            .OrderByDescending(s => s.TotalSales)
            .ToList();

        return Task.FromResult<IEnumerable<SalesByClientDto>>(salesByClient);
    }
}

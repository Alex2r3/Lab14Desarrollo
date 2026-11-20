using MediatR;
using Lab13_2_AlexanderVasquez.Application.DTOs.Orderdetails;
using Lab13_2_AlexanderVasquez.Domain.Repositories;

namespace Lab13_2_AlexanderVasquez.Application.UseCases.Orderdetails.Queries;

public sealed record GetAllOrderdetailsQuery : IRequest<IEnumerable<OrderdetailDto>>;

internal sealed class GetAllOrderdetailsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllOrderdetailsQuery, IEnumerable<OrderdetailDto>>
{
    public async Task<IEnumerable<OrderdetailDto>> Handle(GetAllOrderdetailsQuery request, CancellationToken cancellationToken)
    {
        var details = await unitOfWork.Orderdetails.GetAll();
        return details.Select(d => new OrderdetailDto
        {
            Orderdetailid = d.Orderdetailid,
            Orderid = d.Orderid,
            Productid = d.Productid,
            Quantity = d.Quantity
        }).ToList();
    }
}

public sealed record GetOrderdetailByIdQuery(int Id) : IRequest<OrderdetailDto?>;

internal sealed class GetOrderdetailByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetOrderdetailByIdQuery, OrderdetailDto?>
{
    public async Task<OrderdetailDto?> Handle(GetOrderdetailByIdQuery request, CancellationToken cancellationToken)
    {
        var d = await unitOfWork.Orderdetails.GetById(request.Id);
        if (d == null) return null;
        return new OrderdetailDto
        {
            Orderdetailid = d.Orderdetailid,
            Orderid = d.Orderid,
            Productid = d.Productid,
            Quantity = d.Quantity
        };
    }
}

public sealed record GetDetailsByOrderIdQuery(int OrderId) : IRequest<object>;

internal sealed class GetDetailsByOrderIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetDetailsByOrderIdQuery, object>
{
    public async Task<object> Handle(GetDetailsByOrderIdQuery request, CancellationToken cancellationToken)
    {
        return await unitOfWork.Orderdetails.GetDetailsByOrderId(request.OrderId);
    }
}

public sealed record GetTotalQuantityByOrderIdQuery(int OrderId) : IRequest<int>;

internal sealed class GetTotalQuantityByOrderIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetTotalQuantityByOrderIdQuery, int>
{
    public async Task<int> Handle(GetTotalQuantityByOrderIdQuery request, CancellationToken cancellationToken)
    {
        return await unitOfWork.Orderdetails.GetTotalQuantityByOrderId(request.OrderId);
    }
}

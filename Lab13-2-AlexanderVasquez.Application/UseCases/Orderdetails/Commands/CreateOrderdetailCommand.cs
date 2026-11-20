using MediatR;
using Lab13_2_AlexanderVasquez.Domain.Models;
using Lab13_2_AlexanderVasquez.Domain.Repositories;

namespace Lab13_2_AlexanderVasquez.Application.UseCases.Orderdetails.Commands;

public sealed record CreateOrderdetailCommand : IRequest<int>
{
    public int Orderid { get; init; }
    public int Productid { get; init; }
    public int Quantity { get; init; }
}

internal sealed class CreateOrderdetailCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateOrderdetailCommand, int>
{
    public async Task<int> Handle(CreateOrderdetailCommand request, CancellationToken cancellationToken)
    {
        var detail = new Orderdetail
        {
            Orderid = request.Orderid,
            Productid = request.Productid,
            Quantity = request.Quantity
        };

        await unitOfWork.Orderdetails.Add(detail);
        await unitOfWork.Complete();
        return detail.Orderdetailid;
    }
}

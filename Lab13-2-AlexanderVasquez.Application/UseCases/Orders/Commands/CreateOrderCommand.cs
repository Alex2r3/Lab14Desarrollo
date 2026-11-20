using MediatR;
using Lab13_2_AlexanderVasquez.Domain.Models;
using Lab13_2_AlexanderVasquez.Domain.Repositories;

namespace Lab13_2_AlexanderVasquez.Application.UseCases.Orders.Commands;

public sealed record CreateOrderCommand : IRequest<int>
{
    public int ClientId { get; init; }
    public DateTime OrderDate { get; init; } = DateTime.UtcNow;
}

internal sealed class CreateOrderCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateOrderCommand, int>
{
    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order
        {
            Clientid = request.ClientId,
            Orderdate = request.OrderDate
        };

        await unitOfWork.Orders.Add(order);
        await unitOfWork.Complete();
        return order.Orderid;
    }
}

using MediatR;
using Lab13_2_AlexanderVasquez.Application.DTOs.Clients;
using Lab13_2_AlexanderVasquez.Application.DTOs.Orders;
using Lab13_2_AlexanderVasquez.Domain.Models;
using Lab13_2_AlexanderVasquez.Domain.Repositories;

namespace Lab13_2_AlexanderVasquez.Application.UseCases.Clients.Queries;

public sealed record GetAllClientsQuery : IRequest<IEnumerable<ClientDto>>;

internal sealed class GetAllClientsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllClientsQuery, IEnumerable<ClientDto>>
{
    public async Task<IEnumerable<ClientDto>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
    {
        var clients = await unitOfWork.Clients.GetAll();
        return clients.Select(c => new ClientDto
        {
            Clientid = c.Clientid,
            Name = c.Name,
            Email = c.Email
        }).ToList();
    }
}

public sealed record GetClientByIdQuery(int Id) : IRequest<ClientDto?>;

internal sealed class GetClientByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetClientByIdQuery, ClientDto?>
{
    public async Task<ClientDto?> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
    {
        var c = await unitOfWork.Clients.GetById(request.Id);
        if (c == null) return null;
        return new ClientDto
        {
            Clientid = c.Clientid,
            Name = c.Name,
            Email = c.Email
        };
    }
}

public sealed record GetClientsByNameQuery(string Name) : IRequest<IEnumerable<ClientDto>>;

internal sealed class GetClientsByNameQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetClientsByNameQuery, IEnumerable<ClientDto>>
{
    public async Task<IEnumerable<ClientDto>> Handle(GetClientsByNameQuery request, CancellationToken cancellationToken)
    {
        var clients = await unitOfWork.Clients.GetByName(request.Name);
        return clients.Select(c => new ClientDto
        {
            Clientid = c.Clientid,
            Name = c.Name,
            Email = c.Email
        }).ToList();
    }
}

public sealed record GetTopClientWithMostOrdersQuery : IRequest<object?>;

internal sealed class GetTopClientWithMostOrdersQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetTopClientWithMostOrdersQuery, object?>
{
    public async Task<object?> Handle(GetTopClientWithMostOrdersQuery request, CancellationToken cancellationToken)
    {
        return await unitOfWork.Clients.GetClientWithMostOrders();
    }
}

public sealed record GetProductsSoldToClientQuery(int ClientId) : IRequest<IEnumerable<string>>;

internal sealed class GetProductsSoldToClientQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetProductsSoldToClientQuery, IEnumerable<string>>
{
    public async Task<IEnumerable<string>> Handle(GetProductsSoldToClientQuery request, CancellationToken cancellationToken)
    {
        return await unitOfWork.Clients.GetProductsSoldToClient(request.ClientId);
    }
}

public sealed record GetClientsAndOrdersQuery : IRequest<IEnumerable<ClientOrderDto>>;

internal sealed class GetClientsAndOrdersQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetClientsAndOrdersQuery, IEnumerable<ClientOrderDto>>
{
    public Task<IEnumerable<ClientOrderDto>> Handle(GetClientsAndOrdersQuery request, CancellationToken cancellationToken)
    {
        var clientOrders = unitOfWork.Repository<Client>().Query()
            .Select(client => new ClientOrderDto
            {
                ClientName = client.Name,
                Orders = client.Orders.Select(o => new OrderDto
                {
                    OrderId = o.Orderid,
                    ClientId = o.Clientid,
                    OrderDate = o.Orderdate
                }).ToList()
            }).ToList();

        return Task.FromResult<IEnumerable<ClientOrderDto>>(clientOrders);
    }
}

public sealed record GetClientsWithProductCountQuery : IRequest<IEnumerable<ClientDto>>;

internal sealed class GetClientsWithProductCountQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetClientsWithProductCountQuery, IEnumerable<ClientDto>>
{
    public Task<IEnumerable<ClientDto>> Handle(GetClientsWithProductCountQuery request, CancellationToken cancellationToken)
    {
        var result = unitOfWork.Repository<Client>().Query()
            .Select(client => new ClientDto
            {
                Clientid = client.Clientid,
                Name = client.Name,
                Email = client.Email,
                TotalProducts = client.Orders.Sum(o => o.Orderdetails.Sum(od => od.Quantity))
            }).ToList();

        return Task.FromResult<IEnumerable<ClientDto>>(result);
    }
}

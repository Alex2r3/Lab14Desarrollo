using MediatR;
using Lab13_2_AlexanderVasquez.Domain.Models;
using Lab13_2_AlexanderVasquez.Domain.Repositories;

namespace Lab13_2_AlexanderVasquez.Application.UseCases.Clients.Commands;

public sealed record CreateClientCommand : IRequest<int>
{
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
}

internal sealed class CreateClientCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateClientCommand, int>
{
    public async Task<int> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var client = new Client
        {
            Name = request.Name,
            Email = request.Email
        };

        await unitOfWork.Clients.Add(client);
        await unitOfWork.Complete();
        return client.Clientid;
    }
}

using MediatR;
using Lab13_2_AlexanderVasquez.Domain.Models;
using Lab13_2_AlexanderVasquez.Domain.Repositories;

namespace Lab13_2_AlexanderVasquez.Application.UseCases.Products.Commands;

public sealed record CreateProductCommand : IRequest<int>
{
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public decimal Price { get; init; }
}

internal sealed class CreateProductCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateProductCommand, int>
{
    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price
        };

        await unitOfWork.Products.Add(product);
        await unitOfWork.Complete();
        return product.Productid;
    }
}

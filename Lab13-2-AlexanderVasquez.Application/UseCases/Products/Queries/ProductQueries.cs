using MediatR;
using Lab13_2_AlexanderVasquez.Application.DTOs.Products;
using Lab13_2_AlexanderVasquez.Domain.Models;
using Lab13_2_AlexanderVasquez.Domain.Repositories;

namespace Lab13_2_AlexanderVasquez.Application.UseCases.Products.Queries;

public sealed record GetAllProductsQuery : IRequest<IEnumerable<ProductDto>>;

internal sealed class GetAllProductsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
{
    public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await unitOfWork.Products.GetAll();
        return products.Select(p => new ProductDto
        {
            ProductId = p.Productid,
            ProductName = p.Name,
            Description = p.Description,
            Price = p.Price
        }).ToList();
    }
}

public sealed record GetProductByIdQuery(int Id) : IRequest<ProductDto?>;

internal sealed class GetProductByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
    public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var p = await unitOfWork.Products.GetById(request.Id);
        if (p == null) return null;
        return new ProductDto
        {
            ProductId = p.Productid,
            ProductName = p.Name,
            Description = p.Description,
            Price = p.Price
        };
    }
}

public sealed record GetProductsByPriceGreaterThanQuery(decimal Price) : IRequest<IEnumerable<ProductDto>>;

internal sealed class GetProductsByPriceGreaterThanQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetProductsByPriceGreaterThanQuery, IEnumerable<ProductDto>>
{
    public async Task<IEnumerable<ProductDto>> Handle(GetProductsByPriceGreaterThanQuery request, CancellationToken cancellationToken)
    {
        var products = await unitOfWork.Products.GetByPriceGreaterThan(request.Price);
        return products.Select(p => new ProductDto
        {
            ProductId = p.Productid,
            ProductName = p.Name,
            Description = p.Description,
            Price = p.Price
        }).ToList();
    }
}

public sealed record GetMostExpensiveProductQuery : IRequest<ProductDto?>;

internal sealed class GetMostExpensiveProductQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetMostExpensiveProductQuery, ProductDto?>
{
    public async Task<ProductDto?> Handle(GetMostExpensiveProductQuery request, CancellationToken cancellationToken)
    {
        var p = await unitOfWork.Products.GetMostExpensiveProduct();
        if (p == null) return null;
        return new ProductDto
        {
            ProductId = p.Productid,
            ProductName = p.Name,
            Description = p.Description,
            Price = p.Price
        };
    }
}

public sealed record GetAveragePriceQuery : IRequest<decimal>;

internal sealed class GetAveragePriceQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAveragePriceQuery, decimal>
{
    public async Task<decimal> Handle(GetAveragePriceQuery request, CancellationToken cancellationToken)
    {
        return await unitOfWork.Products.GetAveragePrice();
    }
}

public sealed record GetProductsWithoutDescriptionQuery : IRequest<IEnumerable<ProductDto>>;

internal sealed class GetProductsWithoutDescriptionQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetProductsWithoutDescriptionQuery, IEnumerable<ProductDto>>
{
    public async Task<IEnumerable<ProductDto>> Handle(GetProductsWithoutDescriptionQuery request, CancellationToken cancellationToken)
    {
        var products = await unitOfWork.Products.GetProductsWithoutDescription();
        return products.Select(p => new ProductDto
        {
            ProductId = p.Productid,
            ProductName = p.Name,
            Description = p.Description,
            Price = p.Price
        }).ToList();
    }
}

public sealed record GetClientsWhoBoughtProductQuery(int ProductId) : IRequest<IEnumerable<string>>;

internal sealed class GetClientsWhoBoughtProductQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetClientsWhoBoughtProductQuery, IEnumerable<string>>
{
    public async Task<IEnumerable<string>> Handle(GetClientsWhoBoughtProductQuery request, CancellationToken cancellationToken)
    {
        return await unitOfWork.Products.GetClientsWhoBoughtProduct(request.ProductId);
    }
}

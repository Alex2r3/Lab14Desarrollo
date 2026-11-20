using MediatR;
using Lab13_2_AlexanderVasquez.Application.DTOs.Reports;
using Lab13_2_AlexanderVasquez.Application.Ports;
using Lab13_2_AlexanderVasquez.Domain.Models;
using Lab13_2_AlexanderVasquez.Domain.Repositories;

namespace Lab13_2_AlexanderVasquez.Application.UseCases.Reports.Queries;

public sealed record GenerateSalesByClientExcelReportQuery : IRequest<byte[]>;

internal sealed class GenerateSalesByClientExcelReportQueryHandler(
    IUnitOfWork unitOfWork,
    IExcelReportService excelReportService) : IRequestHandler<GenerateSalesByClientExcelReportQuery, byte[]>
{
    public Task<byte[]> Handle(GenerateSalesByClientExcelReportQuery request, CancellationToken cancellationToken)
    {
        var data = unitOfWork.Repository<Client>().Query()
            .Select(client => new SalesByClientReportItemDto
            {
                ClientId = client.Clientid,
                ClientName = client.Name,
                Email = client.Email,
                TotalOrders = client.Orders.Count,
                TotalProductsPurchased = client.Orders.Sum(o => o.Orderdetails.Sum(od => od.Quantity)),
                TotalSpent = client.Orders.Sum(o => o.Orderdetails.Sum(od => od.Quantity * od.Product.Price))
            })
            .OrderByDescending(c => c.TotalSpent)
            .ToList();

        var fileBytes = excelReportService.GenerateSalesByClientReport(data);
        return Task.FromResult(fileBytes);
    }
}

public sealed record GenerateProductPerformanceExcelReportQuery : IRequest<byte[]>;

internal sealed class GenerateProductPerformanceExcelReportQueryHandler(
    IUnitOfWork unitOfWork,
    IExcelReportService excelReportService) : IRequestHandler<GenerateProductPerformanceExcelReportQuery, byte[]>
{
    public Task<byte[]> Handle(GenerateProductPerformanceExcelReportQuery request, CancellationToken cancellationToken)
    {
        var data = unitOfWork.Repository<Product>().Query()
            .Select(product => new ProductPerformanceReportItemDto
            {
                ProductId = product.Productid,
                ProductName = product.Name,
                Description = product.Description ?? "Sin descripción",
                UnitPrice = product.Price,
                TotalUnitsSold = product.Orderdetails.Sum(od => od.Quantity),
                TotalRevenue = product.Orderdetails.Sum(od => od.Quantity * product.Price),
                UniqueBuyersCount = product.Orderdetails.Select(od => od.Order.Clientid).Distinct().Count()
            })
            .OrderByDescending(p => p.TotalRevenue)
            .ToList();

        var fileBytes = excelReportService.GenerateProductPerformanceReport(data);
        return Task.FromResult(fileBytes);
    }
}

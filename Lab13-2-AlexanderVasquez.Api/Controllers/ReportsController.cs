using MediatR;
using Microsoft.AspNetCore.Mvc;
using Lab13_2_AlexanderVasquez.Application.UseCases.Reports.Queries;

namespace Lab13_2_AlexanderVasquez.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ReportsController(IMediator mediator) : ControllerBase
{
    [HttpGet("sales-by-client/excel")]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> DownloadSalesByClientExcelReport()
    {
        var fileBytes = await mediator.Send(new GenerateSalesByClientExcelReportQuery());
        var fileName = $"Reporte_Ventas_Por_Cliente_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
        return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
    }

    [HttpGet("products-performance/excel")]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> DownloadProductsPerformanceExcelReport()
    {
        var fileBytes = await mediator.Send(new GenerateProductPerformanceExcelReportQuery());
        var fileName = $"Reporte_Rendimiento_Productos_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
        return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
    }
}

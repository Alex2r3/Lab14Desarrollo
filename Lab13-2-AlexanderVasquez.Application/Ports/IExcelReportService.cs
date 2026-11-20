using Lab13_2_AlexanderVasquez.Application.DTOs.Reports;

namespace Lab13_2_AlexanderVasquez.Application.Ports;

public interface IExcelReportService
{
    byte[] GenerateSalesByClientReport(IEnumerable<SalesByClientReportItemDto> items);
    byte[] GenerateProductPerformanceReport(IEnumerable<ProductPerformanceReportItemDto> items);
}

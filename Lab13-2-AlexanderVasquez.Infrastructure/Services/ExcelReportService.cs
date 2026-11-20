using ClosedXML.Excel;
using Lab13_2_AlexanderVasquez.Application.DTOs.Reports;
using Lab13_2_AlexanderVasquez.Application.Ports;

namespace Lab13_2_AlexanderVasquez.Infrastructure.Services;

public class ExcelReportService : IExcelReportService
{
    public byte[] GenerateSalesByClientReport(IEnumerable<SalesByClientReportItemDto> items)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Ventas por Cliente");

        worksheet.Cell(1, 1).Value = "REPORTE CONSOLIDADO DE VENTAS POR CLIENTE";
        var titleRange = worksheet.Range(1, 1, 1, 6);
        titleRange.Merge();
        titleRange.Style.Font.Bold = true;
        titleRange.Style.Font.FontSize = 14;
        titleRange.Style.Font.FontColor = XLColor.White;
        titleRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#1F4E79");
        titleRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

        worksheet.Cell(2, 1).Value = $"Fecha de generación: {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
        var subRange = worksheet.Range(2, 1, 2, 6);
        subRange.Merge();
        subRange.Style.Font.Italic = true;
        subRange.Style.Font.FontSize = 10;
        subRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

        string[] headers = ["ID Cliente", "Nombre Cliente", "Correo Electrónico", "Nº Pedidos", "Productos Comprados", "Total Facturado ($)"];
        for (int i = 0; i < headers.Length; i++)
        {
            var cell = worksheet.Cell(4, i + 1);
            cell.Value = headers[i];
            cell.Style.Font.Bold = true;
            cell.Style.Font.FontColor = XLColor.White;
            cell.Style.Fill.BackgroundColor = XLColor.FromHtml("#2E75B6");
            cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        }

        int currentRow = 5;
        foreach (var item in items)
        {
            worksheet.Cell(currentRow, 1).Value = item.ClientId;
            worksheet.Cell(currentRow, 2).Value = item.ClientName;
            worksheet.Cell(currentRow, 3).Value = item.Email;
            worksheet.Cell(currentRow, 4).Value = item.TotalOrders;
            worksheet.Cell(currentRow, 5).Value = item.TotalProductsPurchased;
            worksheet.Cell(currentRow, 6).Value = item.TotalSpent;

            worksheet.Cell(currentRow, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Cell(currentRow, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Cell(currentRow, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Cell(currentRow, 6).Style.NumberFormat.Format = "$#,##0.00";

            if (currentRow % 2 == 0)
            {
                worksheet.Range(currentRow, 1, currentRow, 6).Style.Fill.BackgroundColor = XLColor.FromHtml("#F2F4F7");
            }

            worksheet.Range(currentRow, 1, currentRow, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            worksheet.Range(currentRow, 1, currentRow, 6).Style.Border.InsideBorder = XLBorderStyleValues.Thin;

            currentRow++;
        }

        if (currentRow > 5)
        {
            worksheet.Cell(currentRow, 1).Value = "TOTAL GENERAL";
            var totalLabelRange = worksheet.Range(currentRow, 1, currentRow, 3);
            totalLabelRange.Merge();
            totalLabelRange.Style.Font.Bold = true;
            totalLabelRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

            worksheet.Cell(currentRow, 4).FormulaA1 = $"SUM(D5:D{currentRow - 1})";
            worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
            worksheet.Cell(currentRow, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell(currentRow, 5).FormulaA1 = $"SUM(E5:E{currentRow - 1})";
            worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
            worksheet.Cell(currentRow, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell(currentRow, 6).FormulaA1 = $"SUM(F5:F{currentRow - 1})";
            worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
            worksheet.Cell(currentRow, 6).Style.NumberFormat.Format = "$#,##0.00";

            var totalRowRange = worksheet.Range(currentRow, 1, currentRow, 6);
            totalRowRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#D9E1F2");
            totalRowRange.Style.Border.TopBorder = XLBorderStyleValues.Thin;
            totalRowRange.Style.Border.BottomBorder = XLBorderStyleValues.Double;
        }

        worksheet.Columns().AdjustToContents();

        using var memoryStream = new MemoryStream();
        workbook.SaveAs(memoryStream);
        return memoryStream.ToArray();
    }

    public byte[] GenerateProductPerformanceReport(IEnumerable<ProductPerformanceReportItemDto> items)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Rendimiento de Productos");

        worksheet.Cell(1, 1).Value = "CATÁLOGO Y RENDIMIENTO COMERCIAL DE PRODUCTOS";
        var titleRange = worksheet.Range(1, 1, 1, 7);
        titleRange.Merge();
        titleRange.Style.Font.Bold = true;
        titleRange.Style.Font.FontSize = 14;
        titleRange.Style.Font.FontColor = XLColor.White;
        titleRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#1E4D2B");
        titleRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

        worksheet.Cell(2, 1).Value = $"Fecha de generación: {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
        var subRange = worksheet.Range(2, 1, 2, 7);
        subRange.Merge();
        subRange.Style.Font.Italic = true;
        subRange.Style.Font.FontSize = 10;
        subRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

        string[] headers = ["ID", "Producto", "Descripción", "Precio Unitario ($)", "Unidades Vendidas", "Ingresos Totales ($)", "Compradores Únicos"];
        for (int i = 0; i < headers.Length; i++)
        {
            var cell = worksheet.Cell(4, i + 1);
            cell.Value = headers[i];
            cell.Style.Font.Bold = true;
            cell.Style.Font.FontColor = XLColor.White;
            cell.Style.Fill.BackgroundColor = XLColor.FromHtml("#2E7D32");
            cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        }

        int currentRow = 5;
        foreach (var item in items)
        {
            worksheet.Cell(currentRow, 1).Value = item.ProductId;
            worksheet.Cell(currentRow, 2).Value = item.ProductName;
            worksheet.Cell(currentRow, 3).Value = item.Description;
            worksheet.Cell(currentRow, 4).Value = item.UnitPrice;
            worksheet.Cell(currentRow, 5).Value = item.TotalUnitsSold;
            worksheet.Cell(currentRow, 6).Value = item.TotalRevenue;
            worksheet.Cell(currentRow, 7).Value = item.UniqueBuyersCount;

            worksheet.Cell(currentRow, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Cell(currentRow, 4).Style.NumberFormat.Format = "$#,##0.00";
            worksheet.Cell(currentRow, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Cell(currentRow, 6).Style.NumberFormat.Format = "$#,##0.00";
            worksheet.Cell(currentRow, 7).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            if (currentRow % 2 == 0)
            {
                worksheet.Range(currentRow, 1, currentRow, 7).Style.Fill.BackgroundColor = XLColor.FromHtml("#E8F5E9");
            }

            worksheet.Range(currentRow, 1, currentRow, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            worksheet.Range(currentRow, 1, currentRow, 7).Style.Border.InsideBorder = XLBorderStyleValues.Thin;

            currentRow++;
        }

        if (currentRow > 5)
        {
            worksheet.Cell(currentRow, 1).Value = "TOTAL GENERAL";
            var totalLabelRange = worksheet.Range(currentRow, 1, currentRow, 3);
            totalLabelRange.Merge();
            totalLabelRange.Style.Font.Bold = true;
            totalLabelRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

            worksheet.Cell(currentRow, 4).FormulaA1 = $"AVERAGE(D5:D{currentRow - 1})";
            worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
            worksheet.Cell(currentRow, 4).Style.NumberFormat.Format = "$#,##0.00";

            worksheet.Cell(currentRow, 5).FormulaA1 = $"SUM(E5:E{currentRow - 1})";
            worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
            worksheet.Cell(currentRow, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell(currentRow, 6).FormulaA1 = $"SUM(F5:F{currentRow - 1})";
            worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
            worksheet.Cell(currentRow, 6).Style.NumberFormat.Format = "$#,##0.00";

            worksheet.Cell(currentRow, 7).Value = "";

            var totalRowRange = worksheet.Range(currentRow, 1, currentRow, 7);
            totalRowRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#C8E6C9");
            totalRowRange.Style.Border.TopBorder = XLBorderStyleValues.Thin;
            totalRowRange.Style.Border.BottomBorder = XLBorderStyleValues.Double;
        }

        worksheet.Columns().AdjustToContents();

        using var memoryStream = new MemoryStream();
        workbook.SaveAs(memoryStream);
        return memoryStream.ToArray();
    }
}

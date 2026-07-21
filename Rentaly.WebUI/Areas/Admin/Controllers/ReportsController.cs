using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using Rentaly.Businesslayer.Abstract;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.EntityLayer.Entities;
using Rentaly.WebUI.Areas.Admin.Models;
using Rentaly.WebUI.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Helpers;
namespace Rentaly.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReportsController : Controller
    {
        private readonly ICarService _carService;
        private readonly IBranchService _branchService;
        private readonly IBookingService _bookingService;

        public ReportsController(ICarService carService, IBranchService branchService, IBookingService bookingService)
        {
            _carService = carService;
            _branchService = branchService;
            _bookingService = bookingService;
        }

        public async Task<IActionResult> Index(int? branchId, CarStatus? status, int page = 1)
        {
            var model = await BuildReportModelAsync(branchId, status, page, pageSize: 5);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ExportExcel(int? branchId, CarStatus? status)
        {
            var model = await BuildReportModelAsync(branchId, status, page: 1, pageSize: int.MaxValue);

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Araç Raporu");

            var headers = new[] { "Plaka", "Araç", "Yıl", "Şube", "Durum", "Günlük Ücret", "Doluluk (%)", "Toplam Kazanç" };
            for (int i = 0; i < headers.Length; i++)
                ws.Cell(1, i + 1).Value = headers[i];

            ws.Row(1).Style.Font.Bold = true;

            int row = 2;
            foreach (var car in model.CarRows)
            {
                ws.Cell(row, 1).Value = car.PlateNumber;
                ws.Cell(row, 2).Value = car.CarName;
                ws.Cell(row, 3).Value = car.Year;
                ws.Cell(row, 4).Value = car.BranchName;
                ws.Cell(row, 5).Value = car.StatusText;
                ws.Cell(row, 6).Value = car.DailyPrice;
                ws.Cell(row, 7).Value = car.OccupancyPercent;
                ws.Cell(row, 8).Value = car.TotalEarnings;
                row++;
            }

            ws.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();

            return File(content,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"AracRaporu_{DateTime.Now:yyyyMMdd_HHmm}.xlsx");
        }

        [HttpGet]
        public async Task<IActionResult> ExportPdf(int? branchId, CarStatus? status)
        {
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

            var model = await BuildReportModelAsync(branchId, status, page: 1, pageSize: int.MaxValue);

            var pdfBytes = GeneratePdfReport(model);

            return File(pdfBytes, "application/pdf", $"AracRaporu_{DateTime.Now:yyyyMMdd_HHmm}.pdf");
        }

        private byte[] GeneratePdfReport(ReportsViewModel model)
        {
            using var stream = new MemoryStream();

            QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(QuestPDF.Helpers.PageSizes.A4.Landscape());
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header().Column(col =>
                    {
                        col.Item().Text("Rentaly - Araç Bazlı Detaylı Rapor").FontSize(18).Bold();
                        col.Item().Text($"Oluşturulma Tarihi: {DateTime.Now:dd.MM.yyyy HH:mm}").FontSize(9);
                        col.Item().Text($"Toplam Araç: {model.TotalCarCount} | Müsait: {model.AvailableCarCount} | Kirada: {model.RentedCarCount} | Bakımda: {model.MaintenanceCarCount}").FontSize(9);
                    });

                    page.Content().PaddingTop(15).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(1.5f);
                            columns.RelativeColumn(1.5f);
                            columns.RelativeColumn(1.5f);
                            columns.RelativeColumn(1.5f);
                        });

                        table.Header(header =>
                        {
                            string[] headers = { "Plaka", "Araç", "Yıl", "Şube", "Durum", "Günlük Ücret", "Doluluk", "Toplam Kazanç" };
                            foreach (var h in headers)
                            {
                                header.Cell().Background(QuestPDF.Helpers.Colors.Grey.Lighten3).Padding(4).Text(h).Bold();
                            }
                        });

                        foreach (var car in model.CarRows)
                        {
                            table.Cell().Padding(4).Text(car.PlateNumber);
                            table.Cell().Padding(4).Text(car.CarName);
                            table.Cell().Padding(4).Text(car.Year.ToString());
                            table.Cell().Padding(4).Text(car.BranchName);
                            table.Cell().Padding(4).Text(car.StatusText);
                            table.Cell().Padding(4).Text(car.DailyPrice.ToString("C0"));
                            table.Cell().Padding(4).Text($"%{car.OccupancyPercent}");
                            table.Cell().Padding(4).Text(car.TotalEarnings.ToString("C0"));
                        }
                    });

                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.CurrentPageNumber();
                        x.Span(" / ");
                        x.TotalPages();
                    });
                });
            }).GeneratePdf(stream);

            return stream.ToArray();
        }
        private async Task<ReportsViewModel> BuildReportModelAsync(int? branchId, CarStatus? status, int page, int pageSize)
        {
            var allCars = await _carService.TGetListAsync(); // Brand, CarModel, Category, Branch Include'lu olmalı
            var branches = await _branchService.TGetListAsync();
            var allBookings = await _bookingService.TGetListAsync();

            var filtered = allCars.AsEnumerable();

            if (branchId.HasValue)
                filtered = filtered.Where(c => c.BranchId == branchId.Value);

            if (status.HasValue)
                filtered = filtered.Where(c => c.Status == status.Value);

            var filteredList = filtered.ToList();

            int totalCount = filteredList.Count;
            int totalPages = pageSize >= totalCount ? 1 : (int)Math.Ceiling(totalCount / (double)pageSize);

            var pagedCars = filteredList
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(c =>
                {
                    var carBookings = allBookings.Where(b => b.CarId == c.CarId && b.Status != "Cancelled").ToList();
                    var totalEarnings = carBookings.Sum(b => b.TotalPrice);
                    var totalRentedDays = carBookings.Sum(b => Math.Max(1, (b.DropOffDate.Date - b.PickUpDate.Date).Days));
                    var daysSinceCreated = Math.Max(1, (DateTime.Now - c.CreatedDate).Days);
                    var occupancy = (int)Math.Round(Math.Min(100.0, (double)totalRentedDays / daysSinceCreated * 100));

                    var (statusText, _, _, _) = CarStatusDisplayHelper.GetDisplay(c.Status);

                    return new CarReportRow
                    {
                        CarId = c.CarId,
                        PlateNumber = c.PlateNumber,
                        CarName = $"{c.Brand?.BrandName} {c.CarModel?.ModelName}",
                        Year = c.Year,
                        ImageUrl = c.ImageUrl,
                        BranchName = c.Branch?.BranchName,
                        StatusText = statusText,
                        StatusCssClass = c.Status.ToString().ToLower(),
                        DailyPrice = c.DailyPrice,
                        OccupancyPercent = occupancy,
                        TotalEarnings = totalEarnings
                    };
                })
                .ToList();

            var branchDistribution = allCars
                .GroupBy(c => c.Branch?.BranchName ?? "Atanmamış")
                .Select(g => new BranchDistributionRow
                {
                    BranchName = g.Key,
                    TotalCars = g.Count(),
                    AvailableCount = g.Count(c => c.Status == CarStatus.Available),
                    RentedCount = g.Count(c => c.Status == CarStatus.Rented),
                    MaintenanceCount = g.Count(c => c.Status == CarStatus.Maintenance),
                    OccupancyPercent = g.Count() > 0 ? (int)Math.Round((double)g.Count(c => c.Status == CarStatus.Rented) / g.Count() * 100) : 0
                })
                .OrderByDescending(b => b.TotalCars)
                .ToList();

            return new ReportsViewModel
            {
                TotalCarCount = allCars.Count,
                AvailableCarCount = allCars.Count(c => c.Status == CarStatus.Available),
                RentedCarCount = allCars.Count(c => c.Status == CarStatus.Rented),
                MaintenanceCarCount = allCars.Count(c => c.Status == CarStatus.Maintenance),
                TotalRevenue = allBookings.Where(b => b.Status != "Cancelled").Sum(b => b.TotalPrice),
                AverageDailyPrice = allCars.Any() ? allCars.Average(c => c.DailyPrice) : 0,
                BranchDistribution = branchDistribution,
                CarRows = pagedCars,
                Branches = branches,
                CurrentPage = page,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

    
    }
}
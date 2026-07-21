using Rentaly.EntityLayer.Entities;

namespace Rentaly.WebUI.Areas.Admin.Models
{
    public class ReportsViewModel
    {
        public int TotalCarCount { get; set; }
        public int AvailableCarCount { get; set; }
        public int RentedCarCount { get; set; }
        public int MaintenanceCarCount { get; set; }

        public decimal TotalRevenue { get; set; }
        public decimal AverageDailyPrice { get; set; }

        public List<BranchDistributionRow> BranchDistribution { get; set; } = new();
        public List<CarReportRow> CarRows { get; set; } = new();
        public List<Branch> Branches { get; set; } = new();

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }

    public class BranchDistributionRow
    {
        public string BranchName { get; set; }
        public int TotalCars { get; set; }
        public int AvailableCount { get; set; }
        public int RentedCount { get; set; }
        public int MaintenanceCount { get; set; }
        public int OccupancyPercent { get; set; }
    }

    public class CarReportRow
    {
        public int CarId { get; set; }
        public string PlateNumber { get; set; }
        public string CarName { get; set; }
        public int Year { get; set; }
        public string ImageUrl { get; set; }
        public string BranchName { get; set; }
        public string StatusText { get; set; }
        public string StatusCssClass { get; set; }
        public decimal DailyPrice { get; set; }
        public int OccupancyPercent { get; set; }
        public decimal TotalEarnings { get; set; }
    }
}
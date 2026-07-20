namespace Rentaly.WebUI.Areas.Admin.Models
{
    public class DashboardViewModel
    {
        public int TotalCarCount { get; set; }
        public int TotalBookingCount { get; set; }
        public int TotalBranchCount { get; set; }
        public List<string> FleetGrowthLabels { get; set; } = new();
        public List<int> FleetGrowthData { get; set; } = new();
    }
}

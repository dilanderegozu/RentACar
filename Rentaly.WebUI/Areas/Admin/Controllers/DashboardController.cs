using Microsoft.AspNetCore.Mvc;
using Rentaly.Businesslayer.Abstract;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.EntityLayer.Entities;
using Rentaly.WebUI.Areas.Admin.Models;

namespace Rentaly.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly ICarService _carService;
        private readonly IBookingService _bookingService;
        private readonly IBranchService _branchService;

        public DashboardController(ICarService carService, IBookingService bookingService, IBranchService branchService)
        {
            _carService = carService;
            _bookingService = bookingService;
            _branchService = branchService;
        }


        public async Task<IActionResult> Index()
        {
            var cars = await _carService.TGetListAsync();
            var bookings = await _bookingService.TGetListAsync();
            var branches = await _branchService.TGetListAsync();

            var sixMonthsAgo = DateTime.Now.AddMonths(-5);
            var monthlyLabels = new List<string>();
            var monthlyFleetCounts = new List<int>();

            for (int i = 0; i < 6; i++)
            {
                var month = sixMonthsAgo.AddMonths(i);
                var endOfMonth = new DateTime(month.Year, month.Month, DateTime.DaysInMonth(month.Year, month.Month));

                var cumulativeCount = cars.Count(c => c.CreatedDate <= endOfMonth);

                monthlyLabels.Add(month.ToString("MMMM", new System.Globalization.CultureInfo("tr-TR")).ToUpper());
                monthlyFleetCounts.Add(cumulativeCount);
            }

            var model = new DashboardViewModel
            {
                TotalCarCount = cars.Count,
                TotalBookingCount = bookings.Count,
                TotalBranchCount = branches.Count,
                FleetGrowthLabels = monthlyLabels,
                FleetGrowthData = monthlyFleetCounts
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> FleetGrowth(int months = 6)
        {
            var cars = await _carService.TGetListAsync();

            var start = DateTime.Now.AddMonths(-(months - 1));
            var labels = new List<string>();
            var data = new List<int>();

            for (int i = 0; i < months; i++)
            {
                var month = start.AddMonths(i);
                var endOfMonth = new DateTime(month.Year, month.Month, DateTime.DaysInMonth(month.Year, month.Month));

                data.Add(cars.Count(c => c.CreatedDate <= endOfMonth));
                labels.Add(month.ToString("MMMM", new System.Globalization.CultureInfo("tr-TR")).ToUpper());
            }

            return Json(new { labels, data });
        }
    }
}

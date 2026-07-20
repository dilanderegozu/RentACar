using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;

namespace Rentaly.WebUI.Areas.Admin.ViewComponents
{
    public class RecentActivitiesComponentPartial:ViewComponent
    {
        private readonly IActivityService _activityService;

        public RecentActivitiesComponentPartial(IActivityService activityService)
        {
            _activityService = activityService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var recentActivities = await _activityService.GetRecentAsync(5);
            return View(recentActivities);
        }
    }
}

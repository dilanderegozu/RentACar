using Rentaly.EntityLayer.Entities;

namespace Rentaly.WebUI.Helpers
{
    public static class ActivityDisplayHelper
    {
        public static (string Icon, string BgClass, string IconColorClass) GetDisplay(ActivityType type)
        {
            return type switch
            {
                ActivityType.CarAdded => ("add_circle", "bg-surface-container", "text-primary"),
                ActivityType.CustomerCreated => ("person_add", "bg-secondary-fixed/40", "text-secondary"),
                ActivityType.RentalCompleted => ("task_alt", "bg-surface-container", "text-primary"),
                ActivityType.CarReturned => ("assignment_return", "bg-error-container/30", "text-error"),
                ActivityType.BranchAdded => ("location_on", "bg-surface-container", "text-primary"),
                _ => ("circle", "bg-surface-container", "text-on-surface-variant")
            };
        }
    }
}
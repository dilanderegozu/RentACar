namespace Rentaly.WebUI.Helpers
{
    public static class TimeAgoHelper
    {
        public static string ToTurkishTimeAgo(this DateTime date)
        {
            var span = DateTime.Now - date;

            if (span.TotalMinutes < 1)
                return "Az Önce";
            if (span.TotalMinutes < 60)
                return $"{(int)span.TotalMinutes} Dakika Önce";
            if (span.TotalHours < 24)
                return $"{(int)span.TotalHours} Saat Önce";
            if (span.TotalDays < 2)
                return "Dün";
            if (span.TotalDays < 7)
                return $"{(int)span.TotalDays} Gün Önce";

            return date.ToString("dd.MM.yyyy");
        }
    }
}
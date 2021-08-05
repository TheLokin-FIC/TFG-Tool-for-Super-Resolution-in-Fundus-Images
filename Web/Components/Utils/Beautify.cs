using System;

namespace Web.Components.Utils
{
    public static class Beautify
    {
        public static string DateToText(DateTime date)
        {
            DateTimeSpan dateSpan = DateTimeSpan.CompareDates(date, DateTime.Now);

            if (dateSpan.Years == 1)
            {
                return $"{dateSpan.Years} year";
            }
            else if (dateSpan.Years > 1)
            {
                return $"{dateSpan.Years} years";
            }
            else if (dateSpan.Months == 1)
            {
                return $"{dateSpan.Months} month";
            }
            else if (dateSpan.Months > 1)
            {
                return $"{dateSpan.Months} months";
            }
            else if (dateSpan.Days == 1)
            {
                return $"{dateSpan.Days} day";
            }
            else
            {
                return $"{dateSpan.Days} days";
            }
        }

        public static string BytesToText(double size)
        {
            int order = 0;
            size = Math.Round(size, 2);
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            while (size >= 1024f && order < sizes.Length - 1)
            {
                order++;
                size = Math.Round(size / 1024f, 2);
            }

            return $"{size} {sizes[order]}";
        }
    }
}
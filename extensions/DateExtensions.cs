using System;

namespace Marti.Core.Extensions;

public static class DateExtensions
{
    public static string ToFormattedDateTime(this DateTime dateTime)
    {
        return dateTime.ToString("dd.MM.yyyy HH:mm");
    }

    public static string ToFormattedDate(this DateTime dateTime)
    {
        return dateTime.ToString("dd.MM.yyyy");
    }

    public static string ToFormattedDateTime(this DateTime? dateTime)
    {
        return dateTime.HasValue ? dateTime.Value.ToFormattedDateTime() : "--";
    }

    public static string ToFormattedTime(this DateTime dateTime)
    {
        return dateTime.ToString("HH:mm");
    }

    public static string ToFormattedTime(this DateTime? dateTime)
    {
        return dateTime.HasValue ? dateTime.Value.ToFormattedTime() : "--";
    }

    public static DateTime StartOfWeek(this DateTime datetime)
    {
        var diff = (7 + (datetime.DayOfWeek - DayOfWeek.Monday)) % 7;
        return datetime.AddDays(-1 * diff).Date;
    }

    public static bool CompareHours(this DateTime endDate, DateTime startDate, int hoursToCompare)
    {
        if (endDate <= startDate)
        {
            return false;
        }
        var hours = endDate.Subtract(startDate).TotalHours;
        return hours <= hoursToCompare;
    }

    public static bool CompareHours(this DateTime? endDate, DateTime? startDate, int hoursToCompare)
    {
        if (!endDate.HasValue || !startDate.HasValue)
        {
            return false;
        }

        return CompareHours(endDate.Value, startDate.Value, hoursToCompare);
    }
}
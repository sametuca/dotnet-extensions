using System;
using System.Text;

namespace Marti.Core.Extensions;

public static class TimeSpanExtensions
{
	public static string ToHumanize(this TimeSpan ts)
	{
		int years = ts.Days / 365; //no leap year accounting
		int months = ts.Days % 365 / 30; //naive guess at month size
		int weeks = ts.Days % 365 % 30 / 7;
		int days = ts.Days % 365 % 30 % 7;

		StringBuilder sb = new StringBuilder();
		if (years > 0)
		{
			sb.Append(years + " yıl, ");
		}

		if (months > 0)
		{
			sb.Append(months + " ay, ");
		}

		if (weeks > 0)
		{
			sb.Append(weeks + " hafta, ");
		}

		if (days > 0)
		{
			sb.Append(days + " gün.");
		}

		string formattedTimeSpan = sb.ToString();
		return formattedTimeSpan;
	}
	
	public static string ToPrettyFormat(this TimeSpan span) {

		if (span == TimeSpan.Zero) return "0 dakika";

		var sb = new StringBuilder();
		if (span.Days > 0)
			sb.AppendFormat("{0} gün ", span.Days);
		if (span.Hours > 0)
			sb.AppendFormat("{0} saat ", span.Hours);
		if (span.Minutes > 0)
			sb.AppendFormat("{0} dakika ", span.Minutes);
		if (span.Seconds > 0)
			sb.AppendFormat("{0} saniye ", span.Seconds);
		return sb.ToString();

	}
}
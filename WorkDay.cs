using System;
using System.Collections.Generic;
using System.Linq;

namespace woorkito
{
	public class WorkDay
	{
		List<DateTimeOffset> hours;

		public WorkDay(List<DateTimeOffset> hours)
		{
			this.hours = new List<DateTimeOffset>(hours);
			if (this.hours.Count() == 0)
				throw new Exception("Hours list is empty"); 
		}

		public TimeSpan TimeWorked()
		{
			if (this.hours.Count == 1)
				return new TimeSpan();
			
			var periods = new List<TimeSpan>();
			var hoursNormalized = this.getHoursNormalized();
			for (var index = 0; index < hoursNormalized.Count - 1; index += 2)
			{
				var diff = hoursNormalized[index + 1].ToLocalTime().TimeOfDay.Subtract(
					hoursNormalized[index].ToLocalTime().TimeOfDay);
				periods.Add(diff);
			}

			var timeWorked = new TimeSpan(periods[0].Hours, periods[0].Minutes, periods[0].Seconds);
			for (var index = 1; index < periods.Count; index++)
				timeWorked += periods[index];

			return timeWorked.Duration();
		}

		public DateTime Day()
		{
			return this.hours[0].Date;
		}

		public string ToStringByPeriods()
		{
			if (this.hours.Count == 1)
				return "";
			
			var hoursNormalized = getHoursNormalized();

			string periods = "";
			for (var index = 0; index < hoursNormalized.Count - 1; index += 2)
			{
				var diff = hoursNormalized[index + 1].ToLocalTime().TimeOfDay.Subtract(
					hoursNormalized[index].ToLocalTime().TimeOfDay);
				periods += string.Format("{0} - {1} = {2}\n",
										 hoursNormalized[index].LocalDateTime.ToString("t"),
										 hoursNormalized[index + 1].LocalDateTime.ToString("t"),
										 diff.ToString(@"hh\:mm"));
			}
			return periods;
		}

		private List<DateTimeOffset> getHoursNormalized()
		{
			var hoursNormalized = new List<DateTimeOffset>(this.hours);
			hoursNormalized = hoursNormalized.OrderBy((arg) => arg.DateTime).ToList();
			if (hoursNormalized.Count == 4)
			{
				var first = hoursNormalized[0];
				var min = new DateTimeOffset(new DateTime(first.Year, first.Month, first.Day, 7, 55, 0));
				var max = new DateTimeOffset(new DateTime(first.Year, first.Month, first.Day, 8, 5, 59));
				if (first >= min && first <= max)
					hoursNormalized[0] = new DateTimeOffset(new DateTime(first.Year, first.Month, first.Day, 8, 0, 0));

				var last = hoursNormalized[3];
				min = new DateTimeOffset(new DateTime(first.Year, first.Month, first.Day, 16, 55, 0));
				max = new DateTimeOffset(new DateTime(first.Year, first.Month, first.Day, 17, 5, 59));
				if (last >= min && last <= max)
					hoursNormalized[3] = new DateTimeOffset(new DateTime(last.Year, last.Month, last.Day, 17, 0, 0));
			}

			for (var index = 0; index < hoursNormalized.Count; index++)
			{
				var dateTime = hoursNormalized[index].ToLocalTime();
				hoursNormalized[index] = new DateTimeOffset(new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 
				                                                         dateTime.Hour, dateTime.Minute, 0));
			}

			return hoursNormalized;
		}
}
}

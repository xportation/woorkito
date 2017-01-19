using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Realms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace woorkito
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ReportPage : ContentPage
	{
		ObservableCollection<string> daysHours;

		public ReportPage()
		{
			InitializeComponent();
			daysHours = new ObservableCollection<string>();

			listView.SetBinding(ItemsView<Cell>.ItemsSourceProperty, new Binding("."));
			listView.BindingContext = daysHours;
			loadRegisters();
		}

		void loadRegisters()
		{
			var realm = Realm.GetInstance();
			Dictionary<DateTime, List<DateTimeOffset>> hoursGrouped = new Dictionary<DateTime, List<DateTimeOffset>>();
			var hours = realm.All<TimeRegister>().OrderByDescending(t => t.Time);
			foreach (var hour in hours)
			{
				if (!hoursGrouped.ContainsKey(hour.Time.Date))
					hoursGrouped.Add(hour.Time.Date, new List<DateTimeOffset>());

				hoursGrouped[hour.Time.Date].Add(hour.Time);
			}

			foreach (KeyValuePair<DateTime, List<DateTimeOffset>> dayHours in hoursGrouped)
			{
				var workDay = new WorkDay(dayHours.Value);
				string display = string.Format("{0} - {1}", workDay.Day().ToString("d"), 
				                               workDay.TimeWorked().ToString(@"hh\:mm"));
				display = string.Format("{0}\n{1}", display, workDay.ToStringByPeriods());
				daysHours.Add(display);
			}
		}
	}
}

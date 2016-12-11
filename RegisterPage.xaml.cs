using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Realms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace woorkito
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage : ContentPage
	{
		ObservableCollection<string> dayHours;

		public RegisterPage()
		{
			InitializeComponent();

			dayHours = new ObservableCollection<string>();

			listView.SetBinding(ItemsView<Cell>.ItemsSourceProperty, new Binding("."));
			listView.BindingContext = dayHours;
			loadTodayRegisters();
		}

		void loadTodayRegisters()
		{
			var realm = Realm.GetInstance();
			var today = DateTimeOffset.Now;
			var min = new DateTimeOffset(new DateTime(today.Year, today.Month, today.Day, 0, 0, 0));
			var max = new DateTimeOffset(new DateTime(today.Year, today.Month, today.Day, 23, 59, 59));
			var hours = realm.All<TimeRegister>().Where(t => t.Time >= min && t.Time <= max).ToList();
			foreach (var hour in hours)
			{
				dayHours.Add(hour.Time.ToLocalTime().ToString("t"));
			}
		}

		void Register_Clicked(object sender, System.EventArgs e)
		{
			//TODO: Should be injected
			var realm = Realm.GetInstance();
			realm.Write(() =>
			{
				var register = realm.CreateObject<TimeRegister>();
				register.Time = DateTimeOffset.Now;
				dayHours.Add(getDateTimeFormated(register.Time));
			});
		}

		string getDateTimeFormated(DateTimeOffset dateTime)
		{
			var dateTimeFormated = dateTime.ToLocalTime().ToString("t");
			if (dateTimeFormated.IndexOf(':') == 1)
			{
				dateTimeFormated = string.Format(" {0}", dateTimeFormated);
			}
			return dateTimeFormated;
		}

		async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem == null)
				return;

			var action = await DisplayActionSheet(null, "Cancel", "Delete", "Edit");
			Debug.WriteLine(string.Format("Action: {0}; Item: {1}", action, e.SelectedItem));
			((ListView)sender).SelectedItem = null;
		}
	}
}

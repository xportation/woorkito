using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using Realms;
using Xamarin.Forms;

namespace woorkito
{
	public partial class RegisterPage : ContentPage
	{
		ObservableCollection<string> dayHours;

		public RegisterPage()
		{
			InitializeComponent();

			dayHours = new ObservableCollection<string>();

			listView.SetBinding(ListView.ItemsSourceProperty, new Binding("."));
			listView.BindingContext = dayHours;
		}

		void Register_Clicked(object sender, System.EventArgs e)
		{
			//TODO: Should be injected
			var realm = Realm.GetInstance();
			realm.Write(() =>
			{
				var register = realm.CreateObject<TimeRegister>();
				register.Time = DateTimeOffset.Now;
				dayHours.Add(register.Time.ToLocalTime().ToString("t"));
			});
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

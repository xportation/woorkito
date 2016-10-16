using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace woorkito
{
	public partial class RegisterPage : ContentPage
	{
		ObservableCollection<string> dayHours;

		public RegisterPage()
		{
			InitializeComponent();

			dayHours = new ObservableCollection<string> { "07:55", "11:59", "14:02" };

			listView.SetBinding(ListView.ItemsSourceProperty, new Binding("."));
			listView.BindingContext = dayHours;
		}

		void Register_Clicked(object sender, System.EventArgs e)
		{
			//throw new NotImplementedException();
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

using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace woorkito
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public class MainPage : TabbedPage
	{
		public MainPage()
		{
			Children.Add(buildNavigationPage(new RegisterPage(), "Register", "register.png"));
			Children.Add(buildNavigationPage(new ReportPage(), "Reports", "reports.png"));
			Children.Add(buildNavigationPage(new SettingsPage(), "Settings", "settings.png"));
		}

		private NavigationPage buildNavigationPage(ContentPage page, String title, String iconPath)
		{
			var navigationPage = new NavigationPage(page);
			navigationPage.Title = title;
			navigationPage.Icon = iconPath;
			return navigationPage;
		}
	}
}


using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace woorkito
{
	public class RegisterPageViewModel : INotifyPropertyChanged
	{
		DateTimeOffset dateTime;

		public event PropertyChangedEventHandler PropertyChanged;

		public RegisterPageViewModel()
		{
			this.DateTime = DateTimeOffset.Now;

			Device.StartTimer(TimeSpan.FromSeconds(1), () =>
				{
					this.DateTime = DateTimeOffset.Now;
					return true;
				});
		}

		public DateTimeOffset DateTime
		{
			set
			{
				if (dateTime != value)
				{
					dateTime = value;
					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("DateTime"));
					}
				}
			}
			get
			{
				return dateTime;
			}
		}
	}
}

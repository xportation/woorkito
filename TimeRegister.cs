using System;
using Realms;

namespace woorkito
{
	public class TimeRegister : RealmObject
	{
		public DateTimeOffset Time { get; set; }
	}
}

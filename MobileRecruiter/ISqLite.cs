using System;
using SQLite.Net;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;

namespace FormSample
{
	public interface ISqLite
	{
		SQLiteConnection GetConnection();
	}
}


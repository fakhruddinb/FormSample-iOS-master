using System;
using Xamarin.Forms;
using Xamarin.Forms.Labs.Controls;
using System.Globalization;

namespace FormSample
{
	using FormSample.Helpers;
	using FormSample.Views;
	using System.Threading.Tasks;
	using Xamarin.Forms.Labs;
	using Xamarin.Forms.Labs.Services;

	public class App
	{

		public static INavigation Navigation { get; private set; }
		public static Color NavTint
		{
			get
			{
				return Color.FromHex("3498DB"); // Xamarin Blue
			}
		}
		public static Color HeaderTint
		{
			get
			{
				return Color.FromHex("2C3E50"); // Xamarin DarkBlue
			}
		}

		public static MainPage RootPage{ get; set;}

		static ILoginManager loginManager;
		public static Page GetLoginPage (ILoginManager ilm)
		{	
			loginManager = ilm;
			return new LoginModalPage (ilm);
		}

		public static Page GetMainPage (ILoginManager ilm)
		{	
			loginManager = ilm;
			RootPage = new MainPage ();
			return RootPage;
		}

		public static void Logout ()
		{
			loginManager.ShowLoginPage();
		}

	}

}


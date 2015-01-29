using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using FormSample.Helpers;
using Xamarin.Forms;
using BugSense;
using BugSense.Model;

namespace FormSample.Droid
{
	using global::Android.Content.PM;

	using Xamarin.Forms.Platform.Android;

	//[Activity(Label = "Mobile Recruiter", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize, ScreenOrientation = ScreenOrientation.Portrait)]
	[Activity( MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize, ScreenOrientation = ScreenOrientation.Portrait)]
	public class MainActivity : AndroidActivity,ILoginManager
	{
		protected override void OnCreate(Bundle bundle)
		{

			base.OnCreate(bundle);

			BugSenseHandler.Instance.InitAndStartSession (new ExceptionManager(), ApplicationContext, "000a57a1");
			this.ActionBar.SetDisplayUseLogoEnabled (true);
			this.ActionBar.SetDisplayShowHomeEnabled (false);
			Xamarin.Forms.Forms.Init(this, bundle);
			ActionBar.SetIcon(Android.Resource.Color.Transparent);
			// SetPage(App.GetMainPage());

			var metrics = Resources.DisplayMetrics;

			int widthInDp = ConvertPixelsToDp(metrics.WidthPixels);
			int heightInDp = ConvertPixelsToDp(metrics.HeightPixels);

//			double wi=(double)metrics.WidthPixels/(double)metrics.Density;
//			double hi=(double)metrics.HeightPixels/(double)metrics.Density;
//			double x = Math.Pow(wi,2);
//			double y = Math.Pow(hi,2);
//			double screenInches = Math.Sqrt(x+y);

			double x = Math.Pow(metrics.WidthPixels/metrics.Xdpi,2);
				double y = Math.Pow(metrics.HeightPixels/metrics.Ydpi,2);
			double screenInches = Math.Sqrt(x+y);

			Utility.DEVICEHEIGHT = heightInDp;
			Utility.DEVICEWIDTH = widthInDp;

//			if (string.IsNullOrWhiteSpace (Settings.GeneralSettings)) {
//				SetPage (App.GetLoginPage (this));
//			} else {
				SetPage(App.GetMainPage(this));
			//}
		}

		public override ActionBar ActionBar {
			get {
				base.ActionBar.SetDisplayUseLogoEnabled (true);
				return base.ActionBar;
			}
		}

		private int ConvertPixelsToDp(float pixelValue)
		{
			var dp = (int) ((pixelValue)/Resources.DisplayMetrics.Density);
			return dp;
		}

		public override void OnBackPressed ()
		{
			//			if (string.IsNullOrWhiteSpace (Settings.GeneralSettings)) {
			//				return;
			//			}
			base.OnBackPressed ();
		}

		public void ShowMainPage ()
		{
			SetPage (App.GetMainPage (this)); 
		}

		public void ShowLoginPage() 
		{
			SetPage (App.GetLoginPage (this)); 
		}

	}
}


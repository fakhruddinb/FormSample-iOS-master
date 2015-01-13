using System;
using Android.Content;
using FormSample.Droid;
using Xamarin.Forms;
using Android.Widget;

[assembly: Dependency(typeof(MapService))]
namespace FormSample.Droid
{
	public class MapService : FormSample.Helpers.Utility.IMapService
	{

		public void OpenMap ()
		{
			var geoUri = Android.Net.Uri.Parse ("geo:51.5000,0.1167");
			var mapIntent = new Intent (Intent.ActionView, geoUri);
			try
			{
				Forms.Context.StartActivity (mapIntent);
			}
			catch(Exception) {
				Toast.MakeText(Xamarin.Forms.Forms.Context, "No Application Available to View Map", ToastLength.Short).Show();
			}
		}
	}
}


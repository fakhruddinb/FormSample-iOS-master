using Android.Content;
using FormSample.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(DeviceService))]
namespace FormSample.Droid
{
	/// <summary>
	/// The device service.
	/// </summary>
	public class DeviceService : FormSample.Helpers.Utility.IDeviceService
	{
		/// <summary>
		/// The call.
		/// </summary>
		/// <param name="phoneNumber"> The phone number. </param>
		public void Call(string phoneNumber)
		{
			var uri = Android.Net.Uri.Parse("tel:" + phoneNumber);
			var intent = new Intent(Intent.ActionDial, uri);
			Forms.Context.StartActivity(intent);
		}

	}
}
using System;
using Xamarin.Forms;
using MobileRecruiter.iOS;
using MonoTouch.UIKit;
using Xamarin.Forms.Labs.iOS.Services;
using MonoTouch.Foundation;

[assembly: Dependency(typeof(DeviceService))]
namespace MobileRecruiter.iOS
{
	public class DeviceService : FormSample.Helpers.Utility.IDeviceService
	{
		/// <summary>
		/// The call.
		/// </summary>
		/// <param name="phoneNumber"> The phone number. </param>
		public void Call(string phoneNumber)
		{
			UIApplication.SharedApplication.OpenUrl (NSUrl.FromString("tel:"+ phoneNumber));
		}

	}
}


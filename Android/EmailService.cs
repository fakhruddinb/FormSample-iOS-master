using Android.Content;
using FormSample.Droid;
using Xamarin.Forms;
using Android.Net;
using System;

[assembly: Dependency(typeof(EmailService))]
namespace FormSample.Droid
{
	public class EmailService :FormSample.Helpers.Utility.IEmailService
	{
		public void OpenEmail(string email)
		{
			try
			{
				var intent = new Intent (Intent.ActionView);
				Android.Net.Uri data = Android.Net.Uri.Parse("mailto:"+ email);
				intent.SetData(data);
				Forms.Context.StartActivity(intent);
			}
			catch(Exception) {
			}
		}
	}
}


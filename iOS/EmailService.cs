using System;
using Xamarin.Forms;
using MobileRecruiter.iOS;
using MonoTouch.MessageUI;

[assembly: Dependency(typeof(EmailService))]
namespace MobileRecruiter.iOS
{
	public class EmailService : FormSample.Helpers.Utility.IEmailService
	{
		public void OpenEmail(string email)
		{
			MFMailComposeViewController _mailController;
			_mailController = new MFMailComposeViewController ();
			_mailController.SetToRecipients (new string[]{ email});
			_mailController.Finished += (object sender, MFComposeResultEventArgs e) => {
				Console.WriteLine(e.Result.ToString());
				e.Controller.DismissViewController(true,null);
			};
		}
	}
}


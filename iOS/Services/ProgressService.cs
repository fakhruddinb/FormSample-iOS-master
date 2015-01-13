using System;
using BigTed;
using Xamarin.Forms;
using FormSample;
using MobileRecruiter.iOS;

[assembly: Dependency(typeof(ProgressService))]
namespace MobileRecruiter.iOS
{
	public class ProgressService : IProgressService
	{
		public ProgressService ()
		{
		}

		public void Show() {
			BTProgressHUD.Show ();
		}

		public void Show(string message) {
			// show wtih message
		}

		public void Dismiss() {
			BTProgressHUD.Dismiss ();
		}
	}
}


using System;
using AndroidHUD;
using Xamarin.Forms;
using FormSample.Droid;

[assembly: Dependency(typeof(ProgressService))]
namespace FormSample.Droid
{
	public class ProgressService : IProgressService
	{
		public ProgressService ()
		{
		}

		public void Show() {
			AndHUD.Shared.Show(Forms.Context, null, -1, MaskType.Black, TimeSpan.FromSeconds(3));
		}

		public void Show(string message) {
			// show wtih message
		}

		public void Dismiss() {
			AndHUD.Shared.Dismiss();
		}
	}
}


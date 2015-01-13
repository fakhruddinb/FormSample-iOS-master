using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using FormSample.ViewModel;

namespace FormSample
{
	public class HomeViewModel:BaseViewModel
	{
		public HomeViewModel ()
		{
		}
		private Command gotoContactUsCommand;
		public const string GotoContactUsCommandPropertyName = "GotoContactUsCommand";
		public Command GotoContactUsCommand
		{
			get{ 
				return gotoContactUsCommand ?? (gotoContactUsCommand = new Command(async()=> await ExecuteContactUsCommand()));
			}
		}

		protected async Task ExecuteContactUsCommand()
		{
			try{
				App.RootPage.NavigateTo("Contact us");
			}
			catch {
			}
		}
	}
}


using System;
using FormSample.Helpers;
using FormSample.Views;
using Xamarin.Forms;

namespace FormSample
{
	public class MainPage : MasterDetailPage
	{
		bool isFirstTime=true;
		MenuPage menuPage;
		public MainPage()
		{

			//var menuPage = new MenuPage ();
			menuPage = new MenuPage ();

			Master = menuPage;
			this.Detail = new NavigationPage (new HomePage ());//  s.NavigateTo ("Home");

			menuPage.Menu.ItemSelected += (sender, e) => {
				if(isFirstTime && (e.SelectedItem as string) == "Home")
				{
				}
				else
				{
					NavigateTo (e.SelectedItem as string);
				}
			};
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
		}

		public void NavigateTo(string item)
		{
			Page page = new Page();
			menuPage.Menu.SelectedItem = item;

			switch (item)
			{

			case "Refer a contractor":
				page = new ContractorPage ();
				break;
			case "My contractors":
				page = new MyContractorPage ();

				break;
			case "Amend my details":
				page = new AmendDetailsPage ();
				break;

			case "Terms and conditions":
				DependencyService.Get<FormSample.Helpers.Utility.IUrlService> ().OpenUrl (Utility.PDFURL);
				page = new HomePage ();
				menuPage.Menu.SelectedItem = "Home";
				break;

			case "About us":
				page = new AboutusPage ();
				break;
			case "Contact us":
				page = new ContactUsPage ();
				break;
			case "Take home pay calculator":
				page = new CalculatorPage ();
				break;
			case "Weekly pay chart":
				page = new ChartPage();
				break;
			case "Log out":
				Settings.GeneralSettings = string.Empty;
				App.Logout ();
				break;
			default:
				//menuPage.Menu.SelectedItem = item;
				page = new HomePage();
				break;

			}
			 this.Detail = new NavigationPage(page);
			//Xamarin.Forms.NavigationPage.SetHasBackButton(page, false);
			//((NavigationPage)this.Detail).PushAsync(page);
			this.IsPresented = false;
			isFirstTime = false;
		}
	}

}


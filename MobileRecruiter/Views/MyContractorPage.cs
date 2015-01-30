using FormSample.Helpers;
using MobileRecruiter;

namespace FormSample
{
	using FormSample.ViewModel;
	using System;
	using Xamarin.Forms;
	using Xamarin.Forms.Labs.Controls;

	public class MyContractorPage : ContentPage
	{
		public static int counter { get; set; }
		private ContractorViewModel contractorViewModel;
		private ContractorDataService dataService = new ContractorDataService();
		private ListView listView;
		//private IProgressService progressService;
		Button btnClearAllContractor;
		public MyContractorPage()
		{
			BindingContext = new ContractorViewModel ();
			//progressService = DependencyService.Get<IProgressService> ();
			contractorViewModel = new ContractorViewModel();
			counter = 1;

			var label = new Label{ Text = "My contractor1",
				BackgroundColor = Color.FromHex("#000000"),
				TextColor = Color.White,
				VerticalOptions = LayoutOptions.Center,
				XAlign = TextAlignment.Center, // Center the text in the blue box.
				YAlign = TextAlignment.Center,
				Font = StyleConstant.GlobalFont
			};

			listView = new ListView
			{
				RowHeight = 40
			};
			var grid = new Grid
			{
				// ColumnSpacing = 100,
				ColumnDefinitions = 
				{
					new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)},
					new ColumnDefinition { Width =  new GridLength(1, GridUnitType.Star) },
				}
			};
			grid.Children.Add(new Label { Text = "Contractor", WidthRequest = Utility.DEVICEWIDTH/2, BackgroundColor=Color.FromHex("#eef2f3"), TextColor=Color.FromHex("#000000") ,Font = StyleConstant.GenerelLabelAndButtonText}, 0, 0); // Left, First element
			grid.Children.Add(new Label { Text = "Date refered" ,WidthRequest = Utility.DEVICEWIDTH/2,BackgroundColor=Color.FromHex("#eef2f3"), TextColor=Color.FromHex("#000000"),Font = StyleConstant.GenerelLabelAndButtonText}, 1, 0);

			btnClearAllContractor = new Button { Text = "Clear all contractor", BackgroundColor = Color.FromHex("3b73b9"), TextColor = Color.White,Font = StyleConstant.GenerelLabelAndButtonText };
            btnClearAllContractor.Clicked += async (object sender, EventArgs e) =>
            {
				try
				{
					var answer =  await DisplayAlert("Confirm", "Do you wish to clear all item?", "Yes", "No");
						if(answer)
							{
								//progressService.Show();
								var result =  dataService.DeleteAllContractor(Settings.GeneralSettings);
								if(result != null)
								{
									//progressService.Dismiss();
									listView.ItemsSource = this.contractorViewModel.contractorList;
								}
							}
//					if (answer)
//					{
//						progressService.Show();
//						var result = await dataService.DeleteAllContractor(Settings.GeneralSettings);
//						if(result != null)
//						{
//							progressService.Dismiss();
//							listView.ItemsSource = this.contractorViewModel.contractorList;
//						}
//					}
				}
				catch
				{
					//progressService.Dismiss();
					DisplayAlert("Message", Utility.SERVERERRORMESSAGE, "OK");
				}
			};
			var contactUsButton = new Button { Text = "Contact Us", BackgroundColor = Color.FromHex("0d9c00"), TextColor = Color.White,Font = StyleConstant.GenerelLabelAndButtonText };
			contactUsButton.Clicked += delegate
			{
				App.RootPage.NavigateTo("Contact us");
			};

			var labelStakeLayout = new StackLayout (){ 
				Padding = new Thickness(Device.OnPlatform(5, 5, 5),0 , Device.OnPlatform(5, 5, 5), 0),
				Children = {label}
			};

			var controlStakeLayout = new StackLayout (){ 
				Orientation = StackOrientation.Vertical,
				Padding = new Thickness(Device.OnPlatform(5, 5, 5),0 , Device.OnPlatform(5, 5, 5), 5), //new Thickness(5,0,5,0),
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = { grid, listView,btnClearAllContractor, contactUsButton}

			};

//			var buttonLayout = new StackLayout (){ 
//				Orientation = StackOrientation.Vertical,
//				Padding = new Thickness(Device.OnPlatform(5, 5, 5),0 , Device.OnPlatform(5, 5, 5), 0), //new Thickness(5,0,5,0),
//				Children= {btnClearAllContractor, contactUsButton}
//			};

			var nameLayOut = new StackLayout
			{
				Orientation = StackOrientation.Vertical,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = {labelStakeLayout,controlStakeLayout}
			};

			Content = new StackLayout
			{
				Children ={ nameLayOut}
			};

			listView.ItemTapped += async (sender, args) =>
			{
				var contractor = args.Item as Contractor;
				if (contractor == null)
				{
					return;
				}
				var answer = await DisplayAlert("Confirm", "Do you wish to clear this item?", "Yes", "No");
				if (answer)
				{
					//progressService.Show();
					var result = await dataService.DeleteContractor(contractor.Id, Settings.GeneralSettings);
					if(result != null)
					{
						await this.contractorViewModel.DeleteContractor(contractor.Id);
						//progressService.Dismiss();
						listView.ItemsSource = this.contractorViewModel.contractorList;
                        if (this.contractorViewModel.contractorList.Count <= 0)
                        {
                            this.btnClearAllContractor.IsVisible = false;
                        }
                        else
                        {
                            this.btnClearAllContractor.IsVisible = true;
                        }
					}
				}

				listView.SelectedItem = null;
			};

		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			MessagingCenter.Subscribe<ContractorViewModel,string> (this, "msg", async(sender, args) => await this.DisplayAlert ("Confirm", args, "Yes", "No"));
			//progressService.Show ();
			try
			{
//				var x = DependencyService.Get<FormSample.Helpers.Utility.INetworkService>().IsReachable();
//				if (!x) {
//					//progressService.Dismiss ();
//					await DisplayAlert ("Message", Utility.NOINTERNETMESSAGE, "OK");
//				} else {
					await this.contractorViewModel.BindContractor ();
					listView.ItemTemplate = new DataTemplate (typeof(ContractorCell));
					listView.ItemsSource = this.contractorViewModel.contractorList;
                    if (this.contractorViewModel.contractorList.Count <= 0)
                    {
                        this.btnClearAllContractor.IsVisible = false;
                    }
                    else
                    {
                        this.btnClearAllContractor.IsVisible = true;
                    }
				//}
			}
			catch(Exception) {
				//progressService.Dismiss ();
				DisplayAlert ("Message", Utility.SERVERERRORMESSAGE, "OK");
			}
		}

		protected override async void OnDisappearing()
		{
			base.OnDisappearing ();
			//progressService.Dismiss ();
			MessagingCenter.Unsubscribe<ContractorViewModel, string>(this, "msg");
		}

	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormSample.Helpers;

namespace FormSample.Views
{
	using Xamarin.Forms;
	using Xamarin.Forms.Labs.Controls;

	public class HomePage : ContentPage
	{
		//private IProgressService progressService;
		Image imgReferContractor,imgMyContractor,imgAboutUs,imgAmendDetail,imgPayChart,imgPayCalc;


		public HomePage()
		{
			double width = 175;
			double height = 150;

			//progressService = DependencyService.Get<IProgressService> ();

			imgReferContractor = new Image () {
				WidthRequest = width,
				HeightRequest = height,
				Aspect = Aspect.AspectFill
			};

			imgMyContractor = new Image (){ 
				WidthRequest = width,
				HeightRequest = height,
				Aspect = Aspect.AspectFill
			};

			imgAboutUs = new Image (){ 
				WidthRequest = width,
				HeightRequest = height,
				Aspect = Aspect.AspectFill
			};

			imgAmendDetail = new Image (){ 
				WidthRequest = width,
				HeightRequest = height,
				Aspect = Aspect.AspectFill
			};

			imgPayChart = new Image (){ 
				WidthRequest = width,
				HeightRequest = height,
				Aspect = Aspect.AspectFill
			};

			imgPayCalc = new Image (){ 
				WidthRequest = width,
				HeightRequest = height,
				Aspect = Aspect.AspectFill
			};

			BindingContext = new HomeViewModel();
			var Layout = this.AssignValues();
			this.Content = Layout;

		}

		public StackLayout AssignValues()
		{
			Label lblTitle = new Label(){Text = "Home",BackgroundColor = Color.Black, Font = Font.SystemFontOfSize(NamedSize.Large),
				TextColor = Color.White,
				VerticalOptions = LayoutOptions.Center,
				XAlign = TextAlignment.Center, // Center the text in the blue box.
				YAlign = TextAlignment.Center
			};
			var grid = new Grid
			{
				RowSpacing = 10,
				ColumnSpacing = 10,
				RowDefinitions = 
				{
					new RowDefinition { Height = GridLength.Auto },
					new RowDefinition { Height = GridLength.Auto },
					new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
				},
				ColumnDefinitions = 
				{
					new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)},
					new ColumnDefinition { Width =  new GridLength(1, GridUnitType.Star) },
				}
				};

			Button referContractorButton = new Button()
			{
				Text = "Refer a contractor",
				TextColor = Color.Black,
				BackgroundColor = new Color(255, 255, 255, 0.5),// Color.Transparent,
				VerticalOptions = LayoutOptions.End
			};

			Button myContractorButton = new Button()
			{
				Text = "My contractors",
				TextColor = Color.Black,
				BackgroundColor = new Color(255, 255, 255, 0.5),// Color.Transparent,
				VerticalOptions = LayoutOptions.End
			};

			Button aboutUsButton = new Button()
			{
				Text = "About us",
				TextColor = Color.Black,
				BackgroundColor = new Color(255, 255, 255, 0.5),// Color.Transparent,
				VerticalOptions = LayoutOptions.End
			};

			Button amendDetailButton = new Button()
			{
				Text = "Amend details",
				TextColor = Color.Black,
				BackgroundColor = new Color(255, 255, 255, 0.5),// Color.Transparent,
				VerticalOptions = LayoutOptions.End
			};

			Button payChartButton = new Button()
			{
				Text = "Pay chart",
				TextColor = Color.Black,
				BackgroundColor = new Color(255, 255, 255, 0.5),// Color.Transparent,
				VerticalOptions = LayoutOptions.End
			};

			Button payCalcButton = new Button()
			{
				Text = "Pay calculator",
				TextColor = Color.Black,
				BackgroundColor = new Color(255, 255, 255, 0.5),// Color.Transparent,
				VerticalOptions = LayoutOptions.End
			};
			grid.Children.Add(imgReferContractor, 0, 0); // Left, First element
			grid.Children.Add(referContractorButton, 0, 0);
			grid.Children.Add(imgMyContractor, 1, 0); // Right, First element new Label { Text = "My Contractors" }
			grid.Children.Add(myContractorButton, 1, 0);
			grid.Children.Add(imgAboutUs, 0, 1); // Left, Second element new Label { Text = "About us" }
			grid.Children.Add(aboutUsButton, 0, 1);
			grid.Children.Add(imgAmendDetail, 1, 1); // Right, Second element new Label { Text = "Amend detail" }
			grid.Children.Add(amendDetailButton, 1, 1);
			grid.Children.Add(imgPayChart, 0, 2); // Left, Thrid element
			grid.Children.Add(payChartButton, 0, 2);
			grid.Children.Add(imgPayCalc, 1, 2); // Right, Thrid element
			grid.Children.Add(payCalcButton, 1, 2);


			referContractorButton.Clicked += async (object sender, EventArgs e) => {
				App.RootPage.NavigateTo("Refer a contractor");
			};

			myContractorButton.Clicked += async (object sender, EventArgs e) => {
				App.RootPage.NavigateTo("My contractors");
			};

			aboutUsButton.Clicked += async (object sender, EventArgs e) => {
				App.RootPage.NavigateTo("About us");
			};

			amendDetailButton.Clicked += async (object sender, EventArgs e) => {
				App.RootPage.NavigateTo("Amend my details");
			};

			payCalcButton.Clicked += async (object sender, EventArgs e) => {
				App.RootPage.NavigateTo("Take home pay calculator");
			};

			payChartButton.Clicked += async (object sender, EventArgs e) => {
				App.RootPage.NavigateTo("Weekly pay chart");
			};

			var downloadButton = new Button { Text = "Download terms and condition", BackgroundColor = Color.FromHex("f7941d"), TextColor = Color.White};
			downloadButton.Clicked +=  (object sender, EventArgs e) => 
			{
				DependencyService.Get<FormSample.Helpers.Utility.IUrlService>().OpenUrl(Utility.PDFURL);
			};

			var contactUsButton = new Button { Text = "Contact us", BackgroundColor = Color.FromHex("0d9c00"), TextColor = Color.White };
			contactUsButton.SetBinding (Button.CommandProperty, HomeViewModel.GotoContactUsCommandPropertyName);

			var labelStakeLayout = new StackLayout (){ 
				Children = {lblTitle},
				Orientation = StackOrientation.Vertical
			};

			var controlStakeLayout = new ScrollView () {
				Padding = new Thickness(Device.OnPlatform(5, 5, 5),0 , Device.OnPlatform(5, 5, 5), 0), //new Thickness(5,0,5,0),
				VerticalOptions = LayoutOptions.FillAndExpand, 
				HorizontalOptions = LayoutOptions.Fill,
				Orientation = ScrollOrientation.Vertical,
				Content = grid
			};

			var buttonLayout = new StackLayout (){ 
				Padding = new Thickness(Device.OnPlatform(5, 5, 5),0 , Device.OnPlatform(5, 5, 5), 0), //new Thickness(5,0,5,0),
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.FillAndExpand, 
				Orientation = StackOrientation.Vertical,
				Children= {downloadButton,contactUsButton }
			};

			var layout = new StackLayout
			{
				Children = {labelStakeLayout,controlStakeLayout,buttonLayout},
				Orientation = StackOrientation.Vertical
			};
			//progressService.Dismiss ();
			return new StackLayout{ Children= {layout}};
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing ();
			//progressService.Show ();
			imgReferContractor.Source = ImageSource.FromResource("MobileRecruiter.Images.homeheader.jpg");
			imgAboutUs.Source = ImageSource.FromResource("MobileRecruiter.Images.aboutus.jpg");
			imgMyContractor.Source = ImageSource.FromResource("MobileRecruiter.Images.MyContractors.jpg");
			imgPayChart.Source = ImageSource.FromResource("MobileRecruiter.Images.PayChart.jpg");
			imgAmendDetail.Source = ImageSource.FromResource("MobileRecruiter.Images.AmendDetail.jpg");
			imgPayCalc.Source = ImageSource.FromResource ("MobileRecruiter.Images.PayCalculator.jpg");
		}

		protected override async void OnDisappearing()
		{
			base.OnDisappearing ();
			//progressService.Dismiss ();
			imgReferContractor.Source = null;
			imgMyContractor.Source = null;
			imgAmendDetail.Source  = null;
			imgAboutUs.Source = null;
			imgPayChart.Source = null;
			imgPayCalc.Source = null;
		}
	}
}

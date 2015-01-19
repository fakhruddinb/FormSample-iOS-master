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
		double width;
		double height;
		// public IList<WeakReference> WeakRefs = new List<WeakReference>();

		public HomePage()
		{

			width= (Utility.DEVICEWIDTH)*25/ 100;
			height = (Utility.DEVICEHEIGHT)*23/ 100;
			ToolbarItems.Add(new ToolbarItem("logo","logo.png",()=>
				{
					DependencyService.Get<FormSample.Helpers.Utility.IUrlService>().OpenUrl(Utility.CHURCHILKNIGHTURL);},
				ToolbarItemOrder.Primary
				,0));

			ToolbarItems.Add(new ToolbarItem("logo2","Icon.png",()=>
				{
					DependencyService.Get<FormSample.Helpers.Utility.IUrlService>().OpenUrl(Utility.CHURCHILKNIGHTURL);},
				ToolbarItemOrder.Secondary
				,1));
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

			// BindingContext = new HomeViewModel();
			var Layout = this.AssignValues();
			this.Content = Layout;

		}

		public StackLayout AssignValues()
		{
			//double imagewidth = (Utility.DEVICEWIDTH-10)*50/ 100;
			//double imageHeight = Utility.DEVICEHEIGHT;
			//
			//			//double padding = Utility.DEVICEWIDTH*2.5/ 100;
			//
			//			var grid = new Grid
			//			{
			//				RowSpacing = 10,
			//				ColumnSpacing = 10
			//				//ColumnSpacing = Utility.DEVICEWIDTH*2.4 / 100
			//			};
			//
			//double width = imagewidth;

			Label lblTitle = new Label(){
				Text = "Home",
				BackgroundColor = Color.Blue,
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

			var tapGestureRecognizer = new TapGestureRecognizer();
			tapGestureRecognizer.Tapped +=   (object sender, EventArgs e) =>
			{
				App.RootPage.NavigateTo("Refer a contractor");
			};
			imgReferContractor.GestureRecognizers.Add(tapGestureRecognizer);

			referContractorButton.Clicked +=   (object sender, EventArgs e) => {
				App.RootPage.NavigateTo("Refer a contractor");
			};

			var myContractorGestureRecognizer = new TapGestureRecognizer();
			myContractorGestureRecognizer.Tapped +=   (object sender, EventArgs e) => 
			{
				App.RootPage.NavigateTo("My contractors");
			};
			imgMyContractor.GestureRecognizers.Add(myContractorGestureRecognizer);

			myContractorButton.Clicked +=   (object sender, EventArgs e) => {
				App.RootPage.NavigateTo("My contractors");
			};

			var aboutUsGestureRecognizer = new TapGestureRecognizer ();
			aboutUsGestureRecognizer.Tapped +=  (object sender, EventArgs e) => {
				App.RootPage.NavigateTo("About us");
			};
			imgAboutUs.GestureRecognizers.Add (aboutUsGestureRecognizer);

			aboutUsButton.Clicked +=   (object sender, EventArgs e) => {
				App.RootPage.NavigateTo("About us");
			};

			var amendDetailsGestureRecognizer = new TapGestureRecognizer ();
			amendDetailsGestureRecognizer.Tapped +=  (object sender, EventArgs e) => {
				App.RootPage.NavigateTo("Amend my details");
			};
			imgAmendDetail.GestureRecognizers.Add (amendDetailsGestureRecognizer);

			amendDetailButton.Clicked +=   (object sender, EventArgs e) => {
				App.RootPage.NavigateTo("Amend my details");
			};

			var payCalculatorGestureRecognizer = new TapGestureRecognizer ();
			payCalculatorGestureRecognizer.Tapped +=  (object sender, EventArgs e) => {
				App.RootPage.NavigateTo("Take home pay calculator");
			};
			imgPayCalc.GestureRecognizers.Add (payCalculatorGestureRecognizer);

			payCalcButton.Clicked +=   (object sender, EventArgs e) => {
				App.RootPage.NavigateTo("Take home pay calculator");
			};

			var payChartGestureReconizer = new TapGestureRecognizer ();
			payChartGestureReconizer.Tapped +=  (object sender, EventArgs e) => {
				App.RootPage.NavigateTo("Weekly pay chart");
			};
			imgPayChart.GestureRecognizers.Add (payChartGestureReconizer);

			payChartButton.Clicked +=   (object sender, EventArgs e) => {
				App.RootPage.NavigateTo("Weekly pay chart");
			};

			var contactUsButton = new Button { Text = "Contact us", BackgroundColor = Color.FromHex("0d9c00"), TextColor = Color.White };
			// contactUsButton.SetBinding (Button.CommandProperty, HomeViewModel.GotoContactUsCommandPropertyName);

			contactUsButton.Clicked += (object sender, EventArgs e) => {
				App.RootPage.NavigateTo("Contact us");
			};
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
				Children= {contactUsButton }
			};

			var layout = new StackLayout
			{
				Children = {labelStakeLayout,controlStakeLayout,buttonLayout},
				Orientation = StackOrientation.Vertical
			};
			//progressService.Dismiss ();
			return new StackLayout{ Children= {layout}};
		}

		protected override  void OnAppearing()
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

		protected override  void OnDisappearing()
		{
			base.OnDisappearing ();
			//progressService.Dismiss ();
			imgReferContractor.Source = null;
			imgMyContractor.Source = null;
			imgAmendDetail.Source  = null;
			imgAboutUs.Source = null;
			imgPayChart.Source = null;
			imgPayCalc.Source = null;
			GC.Collect ();
		}
	}
}

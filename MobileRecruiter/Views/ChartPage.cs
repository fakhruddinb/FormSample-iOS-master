using System.Collections.Generic;
using System.Collections.ObjectModel;
using Syncfusion.SfChart.XForms;
using FormSample.Helpers;
using System;
using MobileRecruiter;
using System.Linq;

namespace FormSample
{

	using Xamarin.Forms;
	public class ChartPage : ContentPage
	{
		SfChart chart1; 
		List<DailyRateCalcuationTable> dailyRate;
		DailyRateDataModel model;

		public ChartPage ()
		{
			ToolbarItems.Add(new ToolbarItem("logo","logo_72x72.png",()=>
				{
					DependencyService.Get<FormSample.Helpers.Utility.IUrlService>().OpenUrl(Utility.CHURCHILKNIGHTURL);},
				ToolbarItemOrder.Primary
				,0));

			dailyRate = new List<DailyRateCalcuationTable>();
			model = new DailyRateDataModel();

			Label header = new Label
			{
				Text = "Pay Chart", 
				BackgroundColor = Color.FromHex("#000000"),
				TextColor = Color.White,
				VerticalOptions = LayoutOptions.Center,
				XAlign = TextAlignment.Center, // Center the text in the blue box.
				YAlign = TextAlignment.Center,
				Font = StyleConstant.GlobalFont
			};

			Label description = new Label
			{
				Text="Please find the helpful guide below to show how much difference a Limited company option could make to " +
					"your contractor's take home pay.",
				TextColor = Color.Black,
				HorizontalOptions = LayoutOptions.FillAndExpand, //VerticalOptions = LayoutOptions.FillAndExpand,
				Font = StyleConstant.GenerelLabelAndButtonText
			};

			var grid = new Grid
			{
				ColumnSpacing = 10,
//				RowDefinitions = 
//				{
//					new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
//				},
//				ColumnDefinitions = 
//				{
//					new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)},
//					new ColumnDefinition { Width =  new GridLength(1, GridUnitType.Star) },
//					new ColumnDefinition { Width =  new GridLength(1, GridUnitType.Star) },
//				},
				Padding= new Thickness(Device.OnPlatform(5, 5, 5),0 , Device.OnPlatform(5, 5, 5), 0)
			};
			grid.Children.Add(new Label {WidthRequest = Utility.DEVICEWIDTH * 32 / 100, Text = "Daily Rate", BackgroundColor=Color.FromHex("#eef2f3"), TextColor=Color.Black,Font = StyleConstant.GenerelLabelAndButtonText}, 0, 0); // Left, First element
			grid.Children.Add(new Label {WidthRequest = Utility.DEVICEWIDTH * 32 / 100, Text = "Limited Company" , BackgroundColor=Color.FromHex("#eef2f3"), TextColor=Color.Black,Font = StyleConstant.GenerelLabelAndButtonText}, 1, 0);
			grid.Children.Add(new Label {WidthRequest = Utility.DEVICEWIDTH * 32 / 100, Text = "Umbrella Company" , BackgroundColor=Color.FromHex("#eef2f3"), TextColor=Color.Black,Font = StyleConstant.GenerelLabelAndButtonText}, 2, 0);

			ListView list= new ListView{
			};
			//list.VerticalOptions  = LayoutOptions.EndAndExpand;
			//list.MinimumHeightRequest = 170;
			list.WidthRequest = Utility.DEVICEWIDTH-10;
			list.ItemTemplate = new DataTemplate(typeof(DailyRateCell));
			list.ItemsSource = GenerateDailyRateTable();
			chart1= new SfChart();

			var contactUsButton = new Button { Text = "Contact us",BackgroundColor = Color.FromHex("0d9c00"), TextColor = Color.White,Font = StyleConstant.GenerelLabelAndButtonText };
			contactUsButton.Clicked +=  (object sender, EventArgs e) => 
			{
				App.RootPage.NavigateTo("Contact us");
			};

			GenerateChart();

			var headerStackLayout = new StackLayout (){ 
				Padding = new Thickness(Device.OnPlatform(5, 5, 5),0 , Device.OnPlatform(5, 5, 5), 0),
				Children = {header},
				Orientation = StackOrientation.Vertical
			};

			var descriptionLayout = new StackLayout () {
				Orientation = StackOrientation.Vertical,
				Padding = new Thickness(Device.OnPlatform(5, 5, 5),0 , Device.OnPlatform(5, 5, 5), 0), //new Thickness(5,0,5,0),
				//VerticalOptions = LayoutOptions.FillAndExpand, 
				HorizontalOptions = LayoutOptions.Fill,
				Children= {description}
			};

//			var buttonLayout = new StackLayout (){ 
//				Orientation = StackOrientation.Vertical,
//				Padding = new Thickness(Device.OnPlatform(5, 5, 5),0 , Device.OnPlatform(5, 5, 5), 0), //new Thickness(5,0,5,0),
//				VerticalOptions = LayoutOptions.FillAndExpand, 
//				HorizontalOptions = LayoutOptions.Fill,
//				Children= {contactUsButton}
//			};

			var contentStackLayout = new StackLayout{ 
				Padding = new Thickness(Device.OnPlatform(5, 5, 5),0, Device.OnPlatform(5, 5, 5), 5),
				Children = {descriptionLayout,grid, list, chart1,contactUsButton},
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.Fill,
			};

			var layout = new ScrollView
			{
				Content = contentStackLayout,
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.Fill,
				Orientation = ScrollOrientation.Vertical
			};
			//Content = new ScrollView(){Content= layout};
			Content = new StackLayout(){Children= {headerStackLayout,layout}};
		}

		List<PayTable> payTableData = new List<PayTable> ();

		private List<DailyRateCalcuationTable> GenerateDailyRateTable()
		{
			getTempData ();

			if (!payTableData.Any ()) {
				FormSample.PayTableDatabase d = new  FormSample.PayTableDatabase ();
				payTableData = d.GetPayTables ().ToList ();
			}
			for (double rate = 100; rate <= 500; rate += 50)
			{
				double weeklyExpense = 50;
				var grossPay = rate * 5;
				var taxablePay = grossPay - weeklyExpense;
				double takeHomePayLimited = 0;
				var payData = payTableData.FirstOrDefault (p => p.TaxablePay == taxablePay); //  d.GetPayTableTaxablePay(taxablePay); //TODO: taxable pay
				if (payData != null)
				{
					var netPay = payData.TakeHomeLimited;
					takeHomePayLimited = netPay + weeklyExpense;
					var percentLimited = (takeHomePayLimited / grossPay) * 100;
				}

				double takeHomeUmbrella = 0;
				payData = payTableData.FirstOrDefault (p => p.TaxablePay == grossPay); // d.GetPayTableTaxablePay(grossPay);
				if (payData != null)
				{
					takeHomeUmbrella = payData.TakeHomeUmbrella;
					var percentUmbrella = (takeHomeUmbrella / grossPay) * 100;

				}

				dailyRate.Add(new DailyRateCalcuationTable(){ 
					DailyRate = rate,
					LimitedCompany = takeHomePayLimited,
					UmbrellaCompany= takeHomeUmbrella
				});
				model.SetLimitedCompanyData(rate.ToString(), takeHomePayLimited);
				model.SetUmbrellaCompanyData(rate.ToString(), takeHomeUmbrella);
			}
			return dailyRate;
		}
		private void GenerateChart()
		{
			chart1.Title=new ChartTitle(){Text="Your weekly pay"};
			chart1.Title.Font =Font.OfSize("Arial",20);
//			chart1.WidthRequest = 200;
//			chart1.HeightRequest = 200;

			chart1.WidthRequest = (Utility.DEVICEWIDTH * 15) / 100;
			chart1.HeightRequest = (Utility.DEVICEHEIGHT*35)/100;

			//Initializing Primary Axis
			Syncfusion.SfChart.XForms.CategoryAxis primaryAxis =new Syncfusion.SfChart.XForms.CategoryAxis();
			primaryAxis.Title = new ChartAxisTitle(){Text= "Daily Rate",Font = StyleConstant.GenerelLabelAndButtonText};
			primaryAxis.LabelStyle.Font = Font.OfSize("Arial", 30);
			//primaryAxis.LabelStyle.TextColor = Color.Red;
			chart1.PrimaryAxis = primaryAxis;

			//			//Initializing Secondary Axis
			Syncfusion.SfChart.XForms.NumericalAxis secondaryAxis=new Syncfusion.SfChart.XForms.NumericalAxis();
			secondaryAxis.Title= new ChartAxisTitle(){Text="Take Home Pay",Font = StyleConstant.GenerelLabelAndButtonText};
			secondaryAxis.LabelStyle.Font = Font.OfSize("Arial", 30);
			//secondaryAxis.LabelStyle.TextColor = Color.Red;
			chart1.SecondaryAxis=secondaryAxis;

			List<Color> brushes = new List<Color> ();
			brushes.Add(Color.FromHex("FF9900"));
			brushes.Add(Color.FromHex("4F4838"));

			chart1.Series.Add(new Syncfusion.SfChart.XForms.ColumnSeries()
				{
					ItemsSource = model.limitedCompanyTax,
					//YAxis=new NumericalAxis(){IsVisible=true},
					IsVisibleOnLegend =true  ,
					Label="Limited",
				});

			chart1.Series.Add(new Syncfusion.SfChart.XForms.ColumnSeries()
				{
					ItemsSource = model.umbrallaCompanyTax,
					//YAxis=new NumericalAxis(){IsVisible=false },
					IsVisibleOnLegend =true,
					Label="Umbrella"
				});

			chart1.ColorModel = new ChartColorModel{ CustomBrushes = brushes, Palette = ChartColorPalette.Custom };
			//Adding Chart Legend for the Chart
			chart1.Legend = new ChartLegend() 
			{ 
				IsVisible = true, 
				DockPosition= Syncfusion.SfChart.XForms.LegendPlacement.Bottom ,
				LabelStyle = new ChartLegendLabelStyle(){ Font = Font.OfSize("Arial",22) }
			};
		}

		void  getTempData ()
		{
			payTableData.Add (new PayTable () {
				TakeHomeLimited = 100,
				TakeHomeUmbrella = 250,
				TaxablePay = 1000
			});payTableData.Add (new PayTable () {
				TakeHomeLimited = 100,
				TakeHomeUmbrella = 25,
				TaxablePay = 450
			});
			payTableData.Add (new PayTable () {
				TakeHomeLimited = 100,
				TakeHomeUmbrella = 250,
				TaxablePay = 950
			});
			payTableData.Add (new PayTable () {
				TakeHomeLimited = 100,
				TakeHomeUmbrella = 250,
				TaxablePay = 350
			});
			payTableData.Add (new PayTable () {
				TakeHomeLimited = 100,
				TakeHomeUmbrella = 250,
				TaxablePay = 400
			});

		}
	}

	public class DailyRateCalcuationTable
	{
		public double DailyRate {get;set;}
		public double LimitedCompany {get;set;}
		public double UmbrellaCompany { get; set; }
	}

	public class DailyRateDataModel
	{
		// public ObservableCollection<ChartDataPoint> dailyRate;
		public ObservableCollection<ChartDataPoint> limitedCompanyTax;
		public ObservableCollection<ChartDataPoint> umbrallaCompanyTax;

		public DailyRateDataModel()
		{
			// dailyRate =new ObservableCollection<ChartDataPoint>();
			limitedCompanyTax = new ObservableCollection<ChartDataPoint>();
			umbrallaCompanyTax = new ObservableCollection<ChartDataPoint>();

		}
		public void SetLimitedCompanyData(string title, double value)
		{
			this.limitedCompanyTax.Add(new ChartDataPoint(title,value));
		}

		public void SetUmbrellaCompanyData(string title, double value)
		{
			this.umbrallaCompanyTax.Add(new ChartDataPoint(title, value));
		}
	}

	public class DailyRateCell : ViewCell
	{
		public DailyRateCell()
		{

			var nameLayout = CreateLayout();
			var viewLayout = new StackLayout()
			{
				Orientation = StackOrientation.Horizontal,
				Children = { nameLayout },
				Padding = new Thickness(Device.OnPlatform(5,5,5),0 ,Device.OnPlatform(5,5,5),0)
			};
			viewLayout.BackgroundColor = MyContractorPage.counter % 2 == 0 ? Color.FromHex ("#ccc") : Color.FromHex ("#eef2f3");
			MyContractorPage.counter++;
			View = viewLayout;
		}
		private StackLayout CreateLayout()
		{
			var nameLabel = new Label { HorizontalOptions = LayoutOptions.FillAndExpand ,Font = StyleConstant.GenerelLabelAndButtonText};
			nameLabel.SetBinding(Label.TextProperty, new Binding("DailyRate"));
			//nameLabel.WidthRequest = 130;
			nameLabel.WidthRequest = Utility.DEVICEWIDTH * 32 / 100;
			nameLabel.TextColor = Color.Black;

			var limitedCompanyLabel = new Label { HorizontalOptions = LayoutOptions.FillAndExpand ,Font = StyleConstant.GenerelLabelAndButtonText};
			limitedCompanyLabel.SetBinding(Label.TextProperty, new Binding("LimitedCompany"));
			//limitedCompanyLabel.WidthRequest = 130;
			limitedCompanyLabel.WidthRequest =Utility.DEVICEWIDTH * 32 / 100;
			limitedCompanyLabel.TextColor = Color.Black;

			var UmbrellaCompanyLabel = new Label { HorizontalOptions = LayoutOptions.FillAndExpand,Font = StyleConstant.GenerelLabelAndButtonText };
			UmbrellaCompanyLabel.SetBinding(Label.TextProperty, new Binding("UmbrellaCompany"));
			//UmbrellaCompanyLabel.WidthRequest = 80;
			UmbrellaCompanyLabel.WidthRequest =Utility.DEVICEWIDTH * 32 / 100;
			UmbrellaCompanyLabel.TextColor = Color.Black;

			var nameLayout = new StackLayout()
			{
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Orientation = StackOrientation.Horizontal,
				Children = { nameLabel, limitedCompanyLabel, UmbrellaCompanyLabel }
			};
			return nameLayout;
		}

	}

}


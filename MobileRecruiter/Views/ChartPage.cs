using System.Collections.Generic;
using System.Collections.ObjectModel;
using Syncfusion.SfChart.XForms;
using FormSample.Helpers;
using System;

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
			dailyRate = new List<DailyRateCalcuationTable>();
			model = new DailyRateDataModel();

			Label header = new Label
			{
				Text = "Pay Chart", BackgroundColor = Color.Black, Font = Font.SystemFontOfSize(NamedSize.Large),
				TextColor = Color.White,
				VerticalOptions = LayoutOptions.Center,
				XAlign = TextAlignment.Center, // Center the text in the blue box.
				YAlign = TextAlignment.Center,
				HeightRequest = 30
			};

			Label description = new Label
			{
				Text="Please find the helpful guide below to show how much difference a Limited company option could make to " +
					"your contractor's take home pay.",
				TextColor = Color.Black,
				HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand
			};

			var grid = new Grid
			{
				ColumnSpacing = 20
			};
			grid.Children.Add(new Label { Text = "Daily Rate", BackgroundColor=Color.Maroon, TextColor=Color.White }, 0, 0); // Left, First element
			grid.Children.Add(new Label { Text = "Limited Company" , BackgroundColor=Color.Maroon, TextColor=Color.White }, 1, 0);
			grid.Children.Add(new Label { Text = "Umbrella Company" , BackgroundColor=Color.Maroon, TextColor=Color.White }, 2, 0);

			ListView list= new ListView{};

			list.ItemTemplate = new DataTemplate(typeof(DailyRateCell));
			list.ItemsSource = GenerateDailyRateTable();
			chart1= new SfChart();

			var downloadButton = new Button { Text = "Download terms and condition", BackgroundColor = Color.FromHex("f7941d"), TextColor = Color.White};
			downloadButton.Clicked += (object sender, EventArgs e) => 
			{
				DependencyService.Get<FormSample.Helpers.Utility.IUrlService>().OpenUrl(Utility.PDFURL);
			};

			var contactUsButton = new Button { Text = "Contact us",BackgroundColor = Color.FromHex("0d9c00"), TextColor = Color.White };
			contactUsButton.Clicked +=  (object sender, EventArgs e) => 
			{
				App.RootPage.NavigateTo("Contact us");
			};

			GenerateChart();

			var headerStackLayout = new StackLayout (){ 
				Children = {header},
				Orientation = StackOrientation.Vertical
			};

			var buttonLayout = new StackLayout (){ 
				Orientation = StackOrientation.Vertical,
				Padding = new Thickness(Device.OnPlatform(5, 5, 5),0 , Device.OnPlatform(5, 5, 5), 0), //new Thickness(5,0,5,0),
				VerticalOptions = LayoutOptions.FillAndExpand, 
				HorizontalOptions = LayoutOptions.Fill,
				Children= { downloadButton,contactUsButton}
			};

			var layout = new StackLayout
			{
				Children = {headerStackLayout, description,grid, list, chart1,buttonLayout},
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.Fill,
			};
			Content = layout;
		}

		private List<DailyRateCalcuationTable> GenerateDailyRateTable()
		{

			FormSample.PayTableDatabase d = new  FormSample.PayTableDatabase();
			for (double rate = 100; rate <= 500; rate += 50)
			{
				double weeklyExpense = 50;
				var grossPay = rate * 5;
				var taxablePay = grossPay - weeklyExpense;
				double takeHomePayLimited = 0;
				var payData = d.GetPayTableTaxablePay(taxablePay); //TODO: taxable pay
				if (payData != null)
				{
					var netPay = payData.TakeHomeLimited;
					takeHomePayLimited = netPay + weeklyExpense;
					var percentLimited = (takeHomePayLimited / grossPay) * 100;
				}

				double takeHomeUmbrella = 0;
				payData = d.GetPayTableTaxablePay(grossPay);
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
			chart1.Title.Font = Font.OfSize("Arial", 10);
			chart1.WidthRequest = 200;
			chart1.HeightRequest = 200;

			//Initializing Primary Axis
			Syncfusion.SfChart.XForms.CategoryAxis primaryAxis=new Syncfusion.SfChart.XForms.CategoryAxis();
			primaryAxis.Title = new ChartAxisTitle(){Text= "Daily Rate"};;
			chart1.PrimaryAxis=primaryAxis;

			//			//Initializing Secondary Axis
			//			Syncfusion.SfChart.XForms.NumericalAxis secondaryAxis=new Syncfusion.SfChart.XForms.NumericalAxis();
			//			secondaryAxis.Title= new ChartAxisTitle(){Text="Temperature"};
			//			chart1.SecondaryAxis=secondaryAxis;

			chart1.Series.Add(new Syncfusion.SfChart.XForms.ColumnSeries()
				{
					ItemsSource = model.limitedCompanyTax,
					YAxis=new NumericalAxis(){IsVisible=false },
					IsVisibleOnLegend =true  ,
					Label="Limited"
				});
			chart1.Series.Add(new Syncfusion.SfChart.XForms.ColumnSeries()
				{
					ItemsSource = model.umbrallaCompanyTax,
					YAxis=new NumericalAxis(){IsVisible=false },
					IsVisibleOnLegend =true,
					Label="Umbrella"
				});
			//Adding Chart Legend for the Chart
			chart1.Legend = new ChartLegend() 
			{ 
				IsVisible = true, 
				DockPosition= Syncfusion.SfChart.XForms.LegendPlacement.Bottom ,
				LabelStyle = new ChartLegendLabelStyle(){Font = Font.OfSize("Arial", 10) }
			};
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
			};
			viewLayout.BackgroundColor = MyContractorPage.counter % 2 == 0 ? Color.Silver: Color.Gray ;
			MyContractorPage.counter++;
			View = viewLayout;
		}
		private StackLayout CreateLayout()
		{
			var nameLabel = new Label { HorizontalOptions = LayoutOptions.FillAndExpand };
			nameLabel.SetBinding(Label.TextProperty, new Binding("DailyRate"));
			nameLabel.WidthRequest = 130;
			nameLabel.TextColor = Color.Black;

			var limitedCompanyLabel = new Label { HorizontalOptions = LayoutOptions.FillAndExpand };
			limitedCompanyLabel.SetBinding(Label.TextProperty, new Binding("LimitedCompany"));
			limitedCompanyLabel.WidthRequest = 130;
			limitedCompanyLabel.TextColor = Color.Black;

			var UmbrellaCompanyLabel = new Label { HorizontalOptions = LayoutOptions.FillAndExpand };
			UmbrellaCompanyLabel.SetBinding(Label.TextProperty, new Binding("UmbrellaCompany"));
			UmbrellaCompanyLabel.WidthRequest = 80;
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


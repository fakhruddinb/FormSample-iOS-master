using System;
using FormSample.Helpers;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Syncfusion.SfChart.XForms;
using System.Collections.ObjectModel;
using MobileRecruiter;


namespace FormSample.Views
{
	using Xamarin.Forms;

	public class CalculatorPage: ContentPage
	{
		SfChart chart1;
		SfChart chart2;
		Entry txtDailyRate;
		Entry txtWeeklyExpense;
		//Grid chartGrid;
		Grid takeHomeGridBelowChart;
		Label takeHomePayLimitedLabel;
		Label percentageLimitedLabel;

		Label takeHomePayumbrellaLabel;
		Label percentageumbrellaLabel;

		Label labelAfterChart;
		DataModel limitedCompanyModel;
		DataModel umbrellaCompanyModel;
		//IProgressService progressiveService;
		double chartwidth;
		double chartHeight;
		double diffLimitedAndUmbrell;
		Label lblText = new Label(){TextColor = Color.Black,
			HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand,
			Font= StyleConstant.GenerelLabelAndButtonText
		};
		public CalculatorPage()
		{
			chartwidth= (Utility.DEVICEWIDTH)*75/ 100;
			chartHeight = Utility.DEVICEHEIGHT*60/ 100;
			//progressiveService = DependencyService.Get<IProgressService> ();
			chart1 = new SfChart();
			chart2 = new SfChart();
			takeHomePayLimitedLabel = new Label(){ XAlign = TextAlignment.Center,Font = StyleConstant.GenerelLabelAndButtonText};
			percentageLimitedLabel = new Label(){ BackgroundColor = Color.Gray, XAlign = TextAlignment.Center,Font = StyleConstant.GenerelLabelAndButtonText };

			takeHomePayumbrellaLabel = new Label{XAlign = TextAlignment.Center,Font = StyleConstant.GenerelLabelAndButtonText};
			percentageumbrellaLabel =  new Label{BackgroundColor = Color.Gray, XAlign = TextAlignment.Center,Font = StyleConstant.GenerelLabelAndButtonText };

			labelAfterChart = new Label(){ TextColor = Color.Black,Font = StyleConstant.GenerelLabelAndButtonText};

			var label = new Label  
			{ 
				Text = "Take home pay calculator", 
				BackgroundColor = Color.FromHex("#000000"),
				TextColor = Color.White,
				VerticalOptions = LayoutOptions.Center,
				XAlign = TextAlignment.Center, // Center the text in the blue box.
				YAlign = TextAlignment.Center,
				Font = StyleConstant.GlobalFont
			};

			var grid = SetDailyGrid();

//			var lblText = new Label
//			{
//				Text = "Your contractor would be a 64.00 better off with a limited company set through us than through an " +
//					"umbrella company click refer a contractor.",
//				TextColor = Color.Black,
//				HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand
//			};

			//			this.chartGrid = new Grid
			//			{ 
			//				RowSpacing = 0,
			//				ColumnSpacing = 0,
			//				RowDefinitions = 
			//				{
			//					new RowDefinition { Height = GridLength.Auto },
			//					new RowDefinition { Height = new GridLength(1, GridUnitType.Star)},
			//				},
			//				ColumnDefinitions = 
			//				{
			//					new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)},
			//				}
			//			};

			this.takeHomeGridBelowChart = new Grid
			{
				RowSpacing=0,
				ColumnSpacing=0,
				WidthRequest = Utility.DEVICEWIDTH,
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
					new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)},
				}

				};

			var contactUsButton = new Button { Text = "Contact us",BackgroundColor = Color.FromHex("0d9c00"), TextColor = Color.White,Font= StyleConstant.GenerelLabelAndButtonText };
			contactUsButton.Clicked +=  (object sender, EventArgs e) => 
			{
				App.RootPage.NavigateTo("Contact us");
			};

			var layout = new StackLayout
			{
				Padding = new Thickness(Device.OnPlatform(5, 5, 5),0 , Device.OnPlatform(5, 5, 5), 0),
				Orientation = StackOrientation.Vertical,
				VerticalOptions = LayoutOptions.FillAndExpand,
			};
			//
			layout.Children.Add(label);

			var controlLayout = new StackLayout (){ 
				Orientation = StackOrientation.Vertical,
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				Children = {grid,lblText,chart1,chart2,this.takeHomeGridBelowChart,labelAfterChart,contactUsButton}
			};
			//layout.Children.Add (controlLayout);
			layout.Children.Add (new ScrollView {
				Content=controlLayout,
				Orientation = ScrollOrientation.Vertical,
				IsClippedToBounds=true
			});
			//
//			layout.Children.Add(grid);
//			layout.Children.Add(new StackLayout
//				{
//					Padding = new Thickness(Device.OnPlatform(5, 5, 5),0 , Device.OnPlatform(5, 5, 5), 0), //new Thickness(5,0,5,0),
//					Orientation = StackOrientation.Vertical,
//					HorizontalOptions = LayoutOptions.Fill,
//					VerticalOptions = LayoutOptions.Fill,
//					Children = {lblText}
//				});
//
//			layout.Children.Add (new StackLayout
//				{
//					Orientation = StackOrientation.Vertical,
//					HorizontalOptions = LayoutOptions.Fill,
//					VerticalOptions = LayoutOptions.Fill,
//					Children = {chart1,chart2}
//				});
//			layout.Children.Add(this.takeHomeGridBelowChart);
//			layout.Children.Add(labelAfterChart);
//			layout.Children.Add (new StackLayout
//				{
//					Padding = new Thickness(Device.OnPlatform(5, 5, 5),0 , Device.OnPlatform(5, 5, 5), 0), //new Thickness(5,0,5,0),
//					Orientation = StackOrientation.Vertical,
//					HorizontalOptions = LayoutOptions.Fill,
//					VerticalOptions = LayoutOptions.Fill,
//					Children = {contactUsButton}
//				});
		
			Content = new StackLayout { Children = {layout}};
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			CalculatePayTableData();
		}

		private Grid SetDailyGrid()
		{
			var grid = new Grid
			{
				RowSpacing = 5,
				//ColumnSpacing = 50,
//				ColumnDefinitions = 
//				{
//					new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)}
//				 
//				},
				Padding = new Thickness(Device.OnPlatform(5, 5, 5),0 , Device.OnPlatform(5, 5, 5), 0),
			};

			var lblDailyRate = new Label
			{
				Text = "Daily Rate",
				TextColor = Color.Black,
				HorizontalOptions = LayoutOptions.Start,
				VerticalOptions = LayoutOptions.Center,
				Font= StyleConstant.GenerelLabelAndButtonText,
				WidthRequest = (Utility.DEVICEWIDTH * 25)/100
			};
			this.txtDailyRate = new Entry
			{
				Text = "200",
				TextColor = Color.White,
				BackgroundColor = Color.Green,
				HorizontalOptions = LayoutOptions.End,
				WidthRequest = 100,
				IsEnabled=false
			};
			var lblWeeklyExpense = new Label
			{
				Text = "Weekly Expenses",
				TextColor = Color.Black,
				HorizontalOptions = LayoutOptions.Start,
				VerticalOptions = LayoutOptions.Center,
				Font= StyleConstant.GenerelLabelAndButtonText,
				WidthRequest = (Utility.DEVICEWIDTH * 25)/100
			};
			this.txtWeeklyExpense = new Entry
			{
				Text = "0",
				TextColor = Color.White,
				BackgroundColor = Color.Green,
				WidthRequest = 100,
				IsEnabled=false
			};
			var upButton = new Button()
			{
				Text = "+",
				TextColor = Color.White,
				BackgroundColor = Color.Gray,
				HeightRequest = 20,
				WidthRequest = 40
			};
			var downButton = new Button()
			{
				Text = "-",
				TextColor = Color.White,
				BackgroundColor = Color.Gray,
				HeightRequest = 20,
				WidthRequest = 40
			};
			upButton.Clicked += async (object sender, EventArgs e) =>   
			{
				if (Convert.ToInt32(txtDailyRate.Text) >= 100 && Convert.ToInt32(txtDailyRate.Text) < 1200)
				{
					txtDailyRate.Text = (Convert.ToInt32(txtDailyRate.Text) + 100).ToString();
					CalculatePayTableData();
				}
			};
			downButton.Clicked += async (object sender, EventArgs e) =>  
			{
				if (Convert.ToInt32(txtDailyRate.Text) > 100)
				{
					txtDailyRate.Text = (Convert.ToInt32(txtDailyRate.Text) - 100).ToString();
					CalculatePayTableData();
				}
			};
			var upWeeklyButton = new Button()
			{
				Text = "+",
				TextColor = Color.White,
				BackgroundColor = Color.Gray,
				HeightRequest = 20,
				WidthRequest = 40
			};
			var downWeeklyButton = new Button()
			{
				Text = "-",
				TextColor = Color.White,

				BackgroundColor = Color.Gray,
				HeightRequest = 20,
				WidthRequest = 40
			};
			upWeeklyButton.Clicked += async (object sender, EventArgs e) =>  
			{
				if (Convert.ToInt32(txtWeeklyExpense.Text) >= 0 && Convert.ToInt32(txtWeeklyExpense.Text) < 750)
				{
					txtWeeklyExpense.Text = (Convert.ToInt32(txtWeeklyExpense.Text) + 25).ToString();
					try
					{
						await CalculatePayTableData();
					}
					catch(Exception ex)
					{
						DisplayAlert("Message",ex.Message,"OK");
					}

				}
			};
			downWeeklyButton.Clicked += async (object sender, EventArgs e) => 
			{
				if (Convert.ToInt32(txtWeeklyExpense.Text) > 25)
				{
					txtWeeklyExpense.Text = (Convert.ToInt32(txtWeeklyExpense.Text) - 25).ToString();
					await CalculatePayTableData();
				}
			};
			var layout1 = new StackLayout()
			{
				Children = {
					lblDailyRate,
					txtDailyRate,
					upButton,
					downButton
				},
				HorizontalOptions = LayoutOptions.End,
				Orientation = StackOrientation.Horizontal
			};
			grid.Children.Add (layout1, 0, 0);

			var weeklyLayout = new StackLayout () {
				Children =  {
					lblWeeklyExpense,
					txtWeeklyExpense,
					upWeeklyButton,
					downWeeklyButton
				},
				HorizontalOptions = LayoutOptions.End,
				Orientation = StackOrientation.Horizontal
			};
			grid.Children.Add (weeklyLayout, 0, 2);


			return grid;
		}

		private List<PayTable> payTableData = new List<PayTable> ();

		void  getTempData ()
		{
			payTableData.Add (new PayTable () {
				TakeHomeLimited = 100,
				TakeHomeUmbrella = 250,
				TaxablePay = 1000
			});payTableData.Add (new PayTable () {
				TakeHomeLimited = 100,
				TakeHomeUmbrella = 250,
				TaxablePay = 300
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

		private async Task CalculatePayTableData()
		{
			//this.progressiveService.Show();
			getTempData ();
			if (!payTableData.Any ()) {
				FormSample.PayTableDatabase d = new PayTableDatabase();
				payTableData = d.GetPayTables().ToList();

			}

			var dailyRate = Convert.ToInt32(this.txtDailyRate.Text);
			var weeklyExpense = Convert.ToInt32(this.txtWeeklyExpense.Text);
			var grossPay = dailyRate * 5;
			var taxablePay = grossPay - weeklyExpense;
			var allData = payTableData;
			var payData = payTableData.FirstOrDefault (d => d.TaxablePay == taxablePay); // d.GetPayTableTaxablePay(taxablePay); //TODO: replace it with taxablePay variable.
			var takeHomePayLimited=0.00;
			if (payData != null)
			{
				var netPay = payData.TakeHomeLimited;
				//var takeHomePayLimited = netPay + weeklyExpense;
				takeHomePayLimited = netPay + weeklyExpense;
				var percentLimited = (takeHomePayLimited / grossPay) * 100;
				var expense = 100 - percentLimited;
				limitedCompanyModel = new DataModel();
				limitedCompanyModel.SetLimitedCompanyData("Take home", percentLimited);
				limitedCompanyModel.SetLimitedCompanyData("Deductions", expense);
				GenerateSyncFusionchartLimited("Pay break down - Limited Company");


				Label limitedCompany = new Label()
				{ Text = "Limited company", BackgroundColor = Color.Gray,Font= StyleConstant.GenerelLabelAndButtonText };

				takeHomePayLimitedLabel.Text = takeHomePayLimited.ToString("C");
				percentageLimitedLabel.Text = percentLimited.ToString("F") + " %";

				var takeHomePayLabel = new Label{ Text = "Take Home Pay",Font= StyleConstant.GenerelLabelAndButtonText };
				var percentPayLabel = new Label{ Text = "Percentage", BackgroundColor = Color.Gray ,Font= StyleConstant.GenerelLabelAndButtonText};

				this.takeHomeGridBelowChart.Children.Add(limitedCompany, 0, 1);
				this.takeHomeGridBelowChart.Children.Add(takeHomePayLimitedLabel, 0, 2);
				this.takeHomeGridBelowChart.Children.Add(percentageLimitedLabel, 0, 3);
				Label dummyLabel = new Label()
				{ Text = "         ", BackgroundColor = Color.Gray, XAlign = TextAlignment.Start };

				this.takeHomeGridBelowChart.Children.Add(dummyLabel, 1, 1);
				this.takeHomeGridBelowChart.Children.Add(takeHomePayLabel, 1, 2);
				this.takeHomeGridBelowChart.Children.Add(percentPayLabel, 1, 3);
			}

			payData = payTableData.FirstOrDefault (d => d.TaxablePay == grossPay);
			var takeHomeUmbrella = 0.00;
			if (payData != null)
			{
				//var takeHomeUmbrella = payData.TakeHomeUmbrella;
				takeHomeUmbrella = payData.TakeHomeUmbrella;
				var percentUmbrella = (takeHomeUmbrella / grossPay) * 100;
				var expense = 100 - percentUmbrella;

				umbrellaCompanyModel = new DataModel();
				umbrellaCompanyModel.SetUmbrellaCompanyData("Take home", percentUmbrella);
				umbrellaCompanyModel.SetUmbrellaCompanyData("Deductions", expense);
				GenerateSyncFusionchartUmbrella("Pay break down - Umbrella Company");

				Label umbrellaCompany = new Label()
				{ Text = "Umbrella company", BackgroundColor = Color.Gray, XAlign = TextAlignment.Start,Font= StyleConstant.GenerelLabelAndButtonText };
				takeHomePayumbrellaLabel.Text = takeHomeUmbrella.ToString("C");
				percentageumbrellaLabel.Text = percentUmbrella.ToString("F") + " %";

				this.takeHomeGridBelowChart.Children.Add(umbrellaCompany, 2, 1);
				this.takeHomeGridBelowChart.Children.Add(takeHomePayumbrellaLabel, 2, 2);
				this.takeHomeGridBelowChart.Children.Add(percentageumbrellaLabel, 2, 3);
				labelAfterChart.Text = "These figures are based on average contracting terms. They ALL include " + weeklyExpense.ToString("C") + " expenses as you specified.";
			}

			diffLimitedAndUmbrell = takeHomePayLimited - takeHomeUmbrella;
			lblText.Text = "Your contractor would be a "+ diffLimitedAndUmbrell.ToString("C") +" better off with a limited company set through us than through an " +
			"umbrella company click refer a contractor.";
		}

		private void GenerateSyncFusionchartLimited(string title)
		{
			// SfChart  chart=new SfChart();
			chart1.Title=new ChartTitle(){Text=title};
			chart1.Title.Font = Font.OfSize("Arial",20);
			chart1.WidthRequest = chartwidth;
			chart1.HeightRequest = chartHeight;
			List<Color> brushes = new List<Color> ();
			brushes.Add(Color.FromHex("FF9900"));
			brushes.Add(Color.FromHex("4F4838"));
			//Adding ColumnSeries to the chart for percipitation
			chart1.Series.Add(new Syncfusion.SfChart.XForms.PieSeries()
				{
					ItemsSource = limitedCompanyModel.limitedCompanyTax,
					DataMarker = new ChartDataMarker (){ShowLabel = true,LabelStyle=new DataMarkerLabelStyle(){TextColor=Color.Black, Font= Font.OfSize("Arial",40)}},
					IsVisibleOnLegend =true ,
					//Color = Color.FromHex("FF9900"),
					ColorModel = new ChartColorModel{CustomBrushes=brushes, Palette=ChartColorPalette.Custom}
				});
			// chart1.ColorModel.Palette = ChartColorPalette.Metro;

			//Adding Chart Legend for the Chart
			chart1.Legend = new ChartLegend() 
			{ 
				IsVisible = true, 
				DockPosition= Syncfusion.SfChart.XForms.LegendPlacement.Bottom ,
				LabelStyle = new ChartLegendLabelStyle(){Font = Font.OfSize("Arial", 18) }
			};
			//this.chartGrid.Children.Add(chart1, 0, 0);
		}

		private void GenerateSyncFusionchartUmbrella(string title)
		{

			chart2.Title=new ChartTitle() { Text=title};
			chart2.Title.Font = Font.OfSize ("Arial", 20);
			chart2.WidthRequest = chartwidth;
			chart2.HeightRequest = chartHeight;
			List<Color> brushes = new List<Color> ();
			brushes.Add(Color.FromHex("FF9900"));
			brushes.Add(Color.FromHex("4F4838"));
			//Adding Series to the chart 
			chart2.Series.Add(new Syncfusion.SfChart.XForms.PieSeries()
				{
					ItemsSource = umbrellaCompanyModel.umbrallaCompanyTax,
					DataMarker = new ChartDataMarker (){ShowLabel = true,LabelStyle=new DataMarkerLabelStyle(){TextColor=Color.Black, Font= Font.OfSize("Arial",40)}},
					IsVisibleOnLegend =true,
					ColorModel = new ChartColorModel{CustomBrushes=brushes, Palette=ChartColorPalette.Custom}
					//Color = Color.FromHex("FF9900")
				});
			//Adding Chart Legend for the Chart
			chart2.Legend = new ChartLegend() 
			{ 
				IsVisible = true, 
				DockPosition= Syncfusion.SfChart.XForms.LegendPlacement.Bottom ,
				LabelStyle = new ChartLegendLabelStyle(){TextColor=Color.Black,Font = Font.OfSize("Arial", 18) }
			};
			//this.chartGrid.Children.Add(chart2,0, 1);
		}

	}

	public class DataModel
	{
		public ObservableCollection<ChartDataPoint> limitedCompanyTax;

		public ObservableCollection<ChartDataPoint> umbrallaCompanyTax;

		public DataModel()
		{
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


}



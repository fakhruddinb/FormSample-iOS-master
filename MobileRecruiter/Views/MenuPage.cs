using System;
using Xamarin.Forms;
using FormSample.Views;
using System.Collections.Generic;

namespace FormSample
{
	/// <summary>
	/// Required for PlatformRenderer
	/// </summary>
	public class MenuTableView : TableView
	{
	}
	public class MenuPage : ContentPage
	{

		public ListView Menu { get; set; }

		public MenuPage()
		{
			Title = "Mobile Recruiter";
			Icon = "menu_icon";
//			var itemList = new List<string> 
//			{ "Home", "Refer a contractor", "My contractors","Amend my details","Terms and conditions",
//				"About us","Contact us","Take home pay calculator","Weekly pay chart"
//				//,"Log out"
//			};
			var itemList = new List<MenuData> {
				new MenuData()
				{
					Title="Home",
					IsSelected= true,
				},
				new MenuData()
				{
					Title="Refer a contractor",
					IsSelected= false,
				},
				new MenuData()
				{
					Title="My contractors",
					IsSelected= false,
				},
				new MenuData()
				{
					Title="Amend my details",
					IsSelected= false,
				},
				new MenuData()
				{
					Title="Terms and conditions",
					IsSelected= false,
				},
				new MenuData()
				{
					Title="About us",
					IsSelected= false,
				},
				new MenuData()
				{
					Title="Contact us",
					IsSelected= false,
				},
				new MenuData()
				{
					Title="Take home pay calculator",
					IsSelected= false,
				},
				new MenuData()
				{
					Title="Weekly pay chart",
					IsSelected= false,
				},
				new MenuData()
				{
					Title="Log out",
					IsSelected= false,
				},

			};
			Menu = new ListView() { ItemsSource = itemList,ItemTemplate=new DataTemplate(typeof(MenuCell))};

//			var headerImage = new Image
//			{
//				Source = ImageSource.FromFile("logo_large_c9y13k30.png")
//			};
//			var headerContentView = new ContentView
//			{
//				Content = headerImage,
//			};

			Content = new StackLayout
			{
				//BackgroundColor = Color.Gray,
				BackgroundColor = Color.White,
				VerticalOptions = LayoutOptions.FillAndExpand,
				//Children = { headerContentView, Menu }
					Children = { Menu }
			};
		}

	}

	public class MenuCell : ViewCell
	{
		public MenuPage Host { get; set; }
		public MenuCell()
		{
			var nameLabel = new Label { HorizontalOptions = LayoutOptions.FillAndExpand };
			nameLabel.SetBinding(Label.TextProperty, new Binding("Title"));
			nameLabel.TextColor = Color.FromHex ("#22498a");
			var layout = new StackLayout
			{
				//BackgroundColor = App.HeaderTint,
				Padding = new Thickness(20, 0, 0, 0),
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Children = { nameLabel }
			};
			View = layout;
		}
	}

	public class MenuData
	{
		public string Title { get; set; }
		public bool IsSelected { get; set; }

	}
}
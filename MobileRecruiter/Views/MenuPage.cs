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
			var itemList = new List<string> 
			{ "Home", "Refer a contractor", "My contractors","Amend my details","Terms and conditions",
				"About us","Contact us","Take home pay calculator","Weekly pay chart"
				//,"Log out"
			};
			Menu = new ListView() { ItemsSource = itemList };

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
}
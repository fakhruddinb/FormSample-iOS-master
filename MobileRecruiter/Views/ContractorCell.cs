using Xamarin.Forms;
using Xamarin.Forms.Labs.Controls;
using MobileRecruiter;
using FormSample.Helpers;


namespace FormSample
{
	public class ContractorCell : ViewCell
	{
		public ContractorCell()
		{
			var nameLayout = CreateLayout();
			var viewLayout = new StackLayout()
			{
				Orientation = StackOrientation.Horizontal,
				Children = { nameLayout }
			};
			viewLayout.BackgroundColor = MyContractorPage.counter % 2 == 0 ? Color.FromHex ("#ccc") : Color.FromHex ("#eef2f3"); ;
			MyContractorPage.counter++;
			View = viewLayout;
		}
		private StackLayout CreateLayout()
		{
			var nameLabel = new Label { HorizontalOptions = LayoutOptions.FillAndExpand };
			nameLabel.SetBinding(Label.TextProperty, new Binding("FirstName"));
			nameLabel.WidthRequest = Utility.DEVICEWIDTH/2;
			nameLabel.TextColor = Color.Black;
			nameLabel.Font = StyleConstant.ListItemFontStyle;

			var referDateLabel = new Label { HorizontalOptions = LayoutOptions.FillAndExpand };
			referDateLabel.SetBinding(Label.TextProperty, new Binding("InsertDate"));
			referDateLabel.WidthRequest = Utility.DEVICEWIDTH/2;
			referDateLabel.TextColor = Color.Black;
			referDateLabel.Font = StyleConstant.ListItemFontStyle;

			var contractorIdLabel = new Label ();
			contractorIdLabel.SetBinding(Label.TextProperty, new Binding("Id"));
			contractorIdLabel.WidthRequest = 0;
			contractorIdLabel.IsVisible = false;

			var nameLayout = new StackLayout()
			{
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Orientation = StackOrientation.Horizontal,
				Padding = new Thickness(Device.OnPlatform(5, 5, 5),0 , Device.OnPlatform(5, 5, 5), 0), 
				Children = { nameLabel, referDateLabel, contractorIdLabel }
			};
			return nameLayout;
		}
	}
}
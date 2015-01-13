using Xamarin.Forms;
using Xamarin.Forms.Labs.Controls;
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
			viewLayout.BackgroundColor = MyContractorPage.counter % 2 == 0 ? Color.Silver: Color.Gray ;
			MyContractorPage.counter++;
			View = viewLayout;
		}
		private StackLayout CreateLayout()
		{
			var nameLabel = new Label { HorizontalOptions = LayoutOptions.FillAndExpand };
			nameLabel.SetBinding(Label.TextProperty, new Binding("FirstName"));
			nameLabel.WidthRequest = 200;
			nameLabel.TextColor = Color.Black;

			var referDateLabel = new Label { HorizontalOptions = LayoutOptions.FillAndExpand };
			referDateLabel.SetBinding(Label.TextProperty, new Binding("InsertDate"));
			referDateLabel.WidthRequest = 200;
			referDateLabel.TextColor = Color.Black;

			var contractorIdLabel = new Label ();
			contractorIdLabel.SetBinding(Label.TextProperty, new Binding("Id"));
			contractorIdLabel.WidthRequest = 0;
			contractorIdLabel.IsVisible = false;

			var nameLayout = new StackLayout()
			{
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Orientation = StackOrientation.Horizontal,
				Children = { nameLabel, referDateLabel, contractorIdLabel }
			};
			return nameLayout;
		}
	}
}
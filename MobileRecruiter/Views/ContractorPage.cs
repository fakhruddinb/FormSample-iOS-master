using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormSample.Helpers;
using MobileRecruiter;

namespace FormSample.Views
{
	using System;

	using Xamarin.Forms;

	using Xamarin.Forms.Labs.Controls;

	using FormSample.ViewModel;

	public class ContractorPage : ContentPage
	{
		DataService service = new DataService();

		public ContractorPage()
		{
			//Icon = "menu_icon.png";
			var Layout = this.AssignValues();
			this.Content = Layout;
		}

		public StackLayout AssignValues()
		{
			BindingContext = new ContractorViewModel();
			var label = new Label
			{
				Text = "Refer a contractor",
				BackgroundColor = Color.FromHex("#000000"),
				TextColor = Color.White,
				VerticalOptions = LayoutOptions.Center,
				XAlign = TextAlignment.Center, // Center the text in the blue box.
				YAlign = TextAlignment.Center,//in the blue box.
				Font = StyleConstant.GlobalFont
			};

//			var firstNameLabel = new Label { HorizontalOptions = LayoutOptions.Fill, Font = Font.SystemFontOfSize (NamedSize.Medium)
//					.WithAttributes (FontAttributes.Bold)};
			var firstNameLabel = new Label { HorizontalOptions = LayoutOptions.Fill};
			firstNameLabel.Text = "First Name";

			var firstName = new MyEntry() { HorizontalOptions = LayoutOptions.FillAndExpand };
			firstName.SetBinding (MyEntry.TextProperty, ContractorViewModel.ContractorFirstNamePropertyName);

			var lastNameLabel = new Label { HorizontalOptions = LayoutOptions.Fill};
			lastNameLabel.Text = "Last Name";

			var lastName = new MyEntry() { HorizontalOptions = LayoutOptions.FillAndExpand };
			lastName.SetBinding (MyEntry.TextProperty, ContractorViewModel.ContractorLastNamePropertyName);
			firstName.Completed +=  (object sender, EventArgs e) => {
				lastName.Focus();
			};

			var phoneNoLabel = new Label { HorizontalOptions = LayoutOptions.Fill};
			phoneNoLabel.Text = "Phone";

			var phoneNo = new Entry() { HorizontalOptions = LayoutOptions.FillAndExpand};
			phoneNo.SetBinding (Entry.TextProperty, ContractorViewModel.ContractorPhonePropertyName);
			phoneNo.Keyboard = Keyboard.Telephone;
			lastName.Completed +=  (object sender, EventArgs e) => {
				phoneNo.Focus();
			};

			var emailLabel = new Label { HorizontalOptions = LayoutOptions.Fill};
			emailLabel.Text = "Email";

			var email = new Entry() { HorizontalOptions = LayoutOptions.FillAndExpand };
			email.SetBinding (Entry.TextProperty, ContractorViewModel.ContractorEmailPropertyName);
			email.Keyboard = Keyboard.Email;
			phoneNo.Completed +=  (object sender, EventArgs e) => {
				email.Focus();
			};

			var additionalInfoLabel = new Label { HorizontalOptions = LayoutOptions.Fill};
			additionalInfoLabel.Text = "Additional Information";

			var additionalInfo = new MyEntry() { HorizontalOptions = LayoutOptions.FillAndExpand};
			additionalInfo.SetBinding (MyEntry.TextProperty, ContractorViewModel.ContractorAdditionalInfoPropertyName);
			email.Completed += (object sender, EventArgs e) => {
				additionalInfo.Focus();
			};

			var chkInvite = new CheckBox();
			chkInvite.SetBinding(CheckBox.CheckedProperty,ContractorViewModel.isCheckedPropertyName,BindingMode.TwoWay);
			chkInvite.DefaultText = "I Agree to the terms and condition";

			Button btnSubmitContractor = new Button
			{
				HorizontalOptions = LayoutOptions.Fill,
				BackgroundColor = Color.FromHex("#22498a"),
				TextColor = Color.White,
				Text = "Submit"
			};
			btnSubmitContractor.SetBinding(Button.CommandProperty,ContractorViewModel.SubmitCommandPropertyName);

			var contactUsButton = new Button { Text = "Contact Us", BackgroundColor = Color.FromHex("0d9c00"), TextColor = Color.White };
			contactUsButton.Clicked +=  (object sender, EventArgs e) =>
			{
				App.RootPage.NavigateTo("Contact us");
			};

			var labelStakeLayout = new StackLayout () {
				Padding = new Thickness(Device.OnPlatform(5, 5, 5),0 , Device.OnPlatform(5, 5, 5), 0),
				Children = { label },
				Orientation = StackOrientation.Vertical
			};

			var cotrolStakeLayout = new StackLayout () {
				Padding = new Thickness(Device.OnPlatform(5, 5, 5),0 , Device.OnPlatform(5, 5, 5), 0), //new Thickness(5,0,5,0),
				VerticalOptions = LayoutOptions.FillAndExpand, 
				HorizontalOptions = LayoutOptions.Fill,
				Orientation = StackOrientation.Vertical,
				Children = { firstNameLabel, firstName, lastNameLabel, lastName, phoneNoLabel, phoneNo, emailLabel, email, additionalInfoLabel, additionalInfo, chkInvite,btnSubmitContractor, contactUsButton}
			};

			var scrollableContentLayout = new ScrollView (){ 
				//Children = {cotrolStakeLayout},
				Content = cotrolStakeLayout,
				//Orientation = StackOrientation.Vertical,
				Orientation = ScrollOrientation.Vertical,
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

//			var buttonLayout = new StackLayout (){ 
//				Padding = new Thickness(Device.OnPlatform(5, 5, 5),0, Device.OnPlatform(5, 5, 5), 0), //new Thickness(5,0,5,0),
//				HorizontalOptions = LayoutOptions.Fill,
//				VerticalOptions = LayoutOptions.End, 
//				Orientation = StackOrientation.Vertical,
//				Children= {btnSubmitContractor, contactUsButton}
//			};

			var nameLayout = new StackLayout()
			{
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.FillAndExpand, 
				Orientation = StackOrientation.Vertical,
				Children = {labelStakeLayout,scrollableContentLayout}
			};
			return new StackLayout{Children= {nameLayout}};
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			MessagingCenter.Subscribe<ContractorViewModel, string>(this, "msg", (sender, args) => this.DisplayAlert("Message", args, "OK"));
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			MessagingCenter.Unsubscribe<ContractorViewModel, string>(this, "msg");
		}
	}

}

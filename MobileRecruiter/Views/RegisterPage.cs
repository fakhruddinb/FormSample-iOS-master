using FormSample.Helpers;

namespace FormSample.Views
{
	using FormSample.ViewModel;
	using System;
	using Xamarin.Forms;
	using Xamarin.Forms.Labs.Controls;

	public class RegisterPage : ContentPage
	{
		DataService service = new DataService();
		private ILoginManager ilm;

		public RegisterPage(ILoginManager ilm)
		{
			this.ilm = ilm;
			var layout = this.AssignValues();
			this.Content = layout;
		}

		public StackLayout AssignValues()
		{
			BindingContext = new AgentViewModel(Navigation,this.ilm);

			var label = new Label
			{
				Text = "Registration",
				BackgroundColor = Color.Black,
				Font = Font.SystemFontOfSize(NamedSize.Large),
				TextColor = Color.White,
				VerticalOptions = LayoutOptions.Center,
				XAlign = TextAlignment.Center, // Center the text in the blue box.
				YAlign = TextAlignment.Center // Center the text in the blue box.
			};

			var emailLabel = new Label { HorizontalOptions = LayoutOptions.Fill};
			emailLabel.Text = "Email";

			var emailText = new Entry() { HorizontalOptions = LayoutOptions.FillAndExpand};
			emailText.SetBinding(Entry.TextProperty, AgentViewModel.AgentEmailPropertyName);
			emailText.Keyboard = Keyboard.Email;

			var firstNameLabel = new Label { HorizontalOptions = LayoutOptions.Fill};
			firstNameLabel.Text = "First Name";

			var firstName = new MyEntry(){HorizontalOptions = LayoutOptions.FillAndExpand};
			firstName.SetBinding(MyEntry.TextProperty, AgentViewModel.FirstNamePropertyName);
			emailText.Completed += async (object sender, EventArgs e) => {
				firstName.Focus();
			};

			var lastNameLabel = new Label { HorizontalOptions = LayoutOptions.Fill};
			lastNameLabel.Text = "Last Name";

			var lastName = new MyEntry() { HorizontalOptions = LayoutOptions.FillAndExpand};
			lastName.SetBinding(MyEntry.TextProperty, AgentViewModel.LastNamePropertyName);
			firstName.Completed +=  (object sender, EventArgs e) => {
				lastName.Focus();
			};

			var agencyLabel = new Label { HorizontalOptions = LayoutOptions.Fill};
			agencyLabel.Text = "Agency";

			var agencyText = new MyEntry() { HorizontalOptions = LayoutOptions.FillAndExpand };
			agencyText.SetBinding(MyEntry.TextProperty, AgentViewModel.AgencyNamePropertyName);
			lastName.Completed += (object sender, EventArgs e) => {
				agencyText.Focus();
			};

			var phoneLabel = new Label { HorizontalOptions = LayoutOptions.Fill};
			phoneLabel.Text = "Phone number";

			var phoneText = new Entry() { HorizontalOptions = LayoutOptions.FillAndExpand};
			phoneText.SetBinding(Entry.TextProperty, AgentViewModel.PhonePropertyName);
			phoneText.Keyboard = Keyboard.Telephone;
			agencyText.Completed +=  (object sender, EventArgs e) => {
				phoneText.Focus();
			};

			var chkInvite = new CheckBox();
			chkInvite.SetBinding(CheckBox.CheckedProperty, AgentViewModel.isCheckedPropertyName,BindingMode.TwoWay);
			chkInvite.DefaultText = "I Agree to the terms and condition";
			chkInvite.IsVisible = true;

			Button btnRegister = new Button
			{
				HorizontalOptions = LayoutOptions.Fill,
				BackgroundColor = Color.FromHex("#22498a"),
				TextColor=Color.White,
				Text = "Register"
			};
			btnRegister.SetBinding(Button.CommandProperty, AgentViewModel.SubmitCommandPropertyName);

			var loginButton = new Button {Text="I already have a recruiter account...", BackgroundColor=Color.FromHex("3b73b9"), TextColor= Color.White };
			loginButton.SetBinding(Button.CommandProperty,AgentViewModel.GotoLoginCommandPropertyName);

			var downloadButton = new Button { Text = "Download Terms and Conditions", BackgroundColor = Color.FromHex("f7941d"), TextColor = Color.White };
			downloadButton.Clicked += (object sender, EventArgs e) => {
				DependencyService.Get<FormSample.Helpers.Utility.IUrlService>().OpenUrl(Utility.PDFURL);
			};

			var lableStakelayout = new StackLayout()
			{
				Children = {label},
				Orientation = StackOrientation.Vertical
			};

			var controlLayout = new StackLayout () {
				Padding = new Thickness(Device.OnPlatform(5, 5, 5),0 , Device.OnPlatform(5, 5, 5), 0), //new Thickness(5,0,5,0),
				VerticalOptions = LayoutOptions.FillAndExpand, 
				HorizontalOptions = LayoutOptions.Fill,
				Orientation = StackOrientation.Vertical,
				Children = {emailLabel, emailText, firstNameLabel, firstName, lastNameLabel, lastName, agencyLabel, agencyText, phoneLabel, phoneText, chkInvite}
			};

			var scrollableContentLayout = new ScrollView (){ 
				Content = controlLayout,
				Orientation = ScrollOrientation.Vertical,
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			var buttonLayout = new StackLayout (){ 
				Padding = new Thickness(Device.OnPlatform(5, 5, 5),0 , Device.OnPlatform(5, 5, 5), 0), //new Thickness(5,0,5,0),
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.FillAndExpand, 
				Orientation = StackOrientation.Vertical,
				Children= {btnRegister, loginButton, downloadButton}
			};

			var nameLayout = new StackLayout()
			{
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Orientation = StackOrientation.Vertical,
				Children = {lableStakelayout,scrollableContentLayout,buttonLayout}
			};
			return new StackLayout{Children= {nameLayout}};
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			MessagingCenter.Subscribe<AgentViewModel,string>(this,"msg",async (sender, args)=> await this.DisplayAlert("Message",args,"OK"));
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			MessagingCenter.Unsubscribe<AgentViewModel, string>(this, "msg");
		}
	}

	public class MyEntry : Entry{

	}
}
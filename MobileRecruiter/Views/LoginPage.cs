using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormSample.Helpers;

namespace FormSample
{
	using FormSample.ViewModel;
	using Xamarin.Forms;

	public class LoginPage : ContentPage
	{

		ILoginManager ilm;
		public LoginPage(ILoginManager ilm)
		{
			this.ilm = ilm;
			BindingContext = new LoginViewModel(Navigation,ilm);

			var layout = new StackLayout { };

			var label = new Label
			{
				Text = "Sign in",
				BackgroundColor = Color.Black,
				Font = Font.SystemFontOfSize(NamedSize.Large),
				TextColor = Color.White,
				VerticalOptions = LayoutOptions.Center,
				XAlign = TextAlignment.Center, // Center the text in the blue box.
				YAlign = TextAlignment.Center, // Center the text in the blue box.
			};

			var labelStakeLayout = new StackLayout (){ 
				Children = {label}
			};

			var userNameLabel = new Label { HorizontalOptions = LayoutOptions.Fill};
			userNameLabel.Text = "Email";

			var username = new Entry() { HorizontalOptions = LayoutOptions.FillAndExpand};
			username.SetBinding(Entry.TextProperty, LoginViewModel.UsernamePropertyName);
			username.Keyboard = Keyboard.Email;

			var passwordLabel = new Label { HorizontalOptions = LayoutOptions.Fill};
			passwordLabel.Text = "Password";

			var password = new Entry() {HorizontalOptions = LayoutOptions.FillAndExpand };
			password.SetBinding(Entry.TextProperty, LoginViewModel.PasswordPropertyName);
			password.IsPassword = true;

			username.Completed +=  (object sender, EventArgs e) => {
				password.Focus();
			};

			var forgotPassword = new Button { Text = "I have forgotton my password", BackgroundColor=Color.FromHex("3b73b9")};
			forgotPassword.SetBinding (Button.CommandProperty, LoginViewModel.ForgotPasswordCommandPropertyName);

			var loginButton = new Button { Text = "Sign In",BackgroundColor = Color.FromHex("22498a")};
			loginButton.SetBinding(Button.CommandProperty, LoginViewModel.LoginCommandPropertyName);

			var registerButton = new Button { Text = "I don't have a recruiter account..", BackgroundColor=Color.FromHex("3b73b9")};
			registerButton.SetBinding(Button.CommandProperty, LoginViewModel.GoToRegisterCommandPropertyName);

			var downloadButton = new Button { Text = "Download Terms and Conditions", BackgroundColor = Color.FromHex("f7941d")};
			downloadButton.Clicked += (object sender, EventArgs e) => {
				DependencyService.Get<FormSample.Helpers.Utility.IUrlService> ().OpenUrl (Utility.PDFURL);
			};

			var controlStakeLayout = new StackLayout (){ 
				Padding = new Thickness(Device.OnPlatform(5, 5, 5),0 , Device.OnPlatform(5, 5, 5), 0), //new Thickness(5,0,5,0),
				VerticalOptions = LayoutOptions.FillAndExpand, 
				HorizontalOptions = LayoutOptions.Fill,
				Orientation = StackOrientation.Vertical,
				Children = {userNameLabel,username,passwordLabel,password}
			};

			var buttonStakeLayout = new StackLayout (){ 

				Orientation = StackOrientation.Vertical,
				Padding = new Thickness(Device.OnPlatform(5, 5, 5),0, Device.OnPlatform(5, 5, 5), 0) ,//new Thickness(5,0, 5,0)
				Children= {forgotPassword,loginButton,registerButton,downloadButton}
			};

			layout.Children.Add(labelStakeLayout);
			layout.Children.Add (controlStakeLayout);
			layout.Children.Add (buttonStakeLayout);

			Content = new StackLayout { Children = {layout} };
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			MessagingCenter.Subscribe<LoginViewModel, string>(this, "msg", async (sender, args) => await this.DisplayAlert("Message", args, "OK"));
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			MessagingCenter.Unsubscribe<LoginViewModel, string>(this, "msg");
		}
	}


}

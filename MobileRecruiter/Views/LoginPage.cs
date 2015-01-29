using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormSample.Helpers;
using MobileRecruiter;

namespace FormSample
{
	using FormSample.ViewModel;
	using Xamarin.Forms;
    using Xamarin.Forms.Labs.Controls;

	public class LoginPage : ContentPage
	{

		ILoginManager ilm;
		private IProgressService progressService;
		private DataService dataService;

		public LoginPage(ILoginManager ilm)
		{
			this.ilm = ilm;
			BindingContext = new LoginViewModel(Navigation,ilm);
//			var topPadding = Utility.DEVICEHEIGHT * 30 / 100;
			var topPadding = 0;
			this.dataService = new DataService();
			progressService = DependencyService.Get<IProgressService>();

			var layout = new StackLayout { };

			var label = new Label
			{
				Text = "Sign in",
				BackgroundColor = Color.FromHex("#000000"),
				TextColor = Color.White,
				VerticalOptions = LayoutOptions.Center,
				XAlign = TextAlignment.Center, // Center the text in the blue box.
				YAlign = TextAlignment.Center,
				Font = StyleConstant.GlobalFont
			};

			var labelStakeLayout = new StackLayout (){ 
				Padding = new Thickness(Device.OnPlatform(5, 5, 5),0 , Device.OnPlatform(5, 5, 5), 0),
				Children = {label}
			};

			var userNameLabel = new Label { HorizontalOptions = LayoutOptions.Fill,Font = Font.SystemFontOfSize (NamedSize.Medium)
					.WithAttributes (FontAttributes.Bold)};
			userNameLabel.Text = "Email";

			var username = new Entry() { HorizontalOptions = LayoutOptions.FillAndExpand};
			username.SetBinding(Entry.TextProperty, LoginViewModel.UsernamePropertyName);
			username.Keyboard = Keyboard.Email;

			var passwordLabel = new Label { HorizontalOptions = LayoutOptions.Fill,Font = Font.SystemFontOfSize (NamedSize.Medium)
					.WithAttributes (FontAttributes.Bold)};
			passwordLabel.Text = "Password";

			var password = new Entry() {HorizontalOptions = LayoutOptions.FillAndExpand };
			password.SetBinding(Entry.TextProperty, LoginViewModel.PasswordPropertyName);
			password.IsPassword = true;

			username.Completed +=  (object sender, EventArgs e) => {
				password.Focus();
			};

            //var forgotPassword = new Button { Text = "I have forgotton my password", BackgroundColor=Color.Transparent,TextColor = Color.Blue,};
            //forgotPassword.SetBinding (Button.CommandProperty, LoginViewModel.ForgotPasswordCommandPropertyName);
            //forgotPassword.BorderWidth = 30;

            ExtendedLabel forgotPassword = new ExtendedLabel();
            forgotPassword.IsUnderline = true;
            forgotPassword.Text = "I have forgot my password";
			//forgotPassword.BackgroundColor = Color.FromHex ("#22498a");
			//forgotPassword.TextColor = Color.White;
			forgotPassword.Font = Font.SystemFontOfSize (NamedSize.Large).WithAttributes(FontAttributes.Bold);

			var forgotPasswordGestureRecognizer = new TapGestureRecognizer ();
			forgotPassword.GestureRecognizers.Add(forgotPasswordGestureRecognizer);

			forgotPasswordGestureRecognizer.Tapped += async (object sender, EventArgs e) => 
			{
				try
				{
					string errorMessage = string.Empty;
					progressService.Show();

					if(string.IsNullOrWhiteSpace(username.Text))
					{

						errorMessage = errorMessage + Utility.EAMAILMESSAGE;
					}
					else if (!Utility.IsValidEmailAddress(username.Text))
					{
						errorMessage = errorMessage + Utility.INVALIDEMAILMESSAGE;
					}
					if(!string.IsNullOrEmpty(errorMessage))
					{
						progressService.Dismiss();
						await DisplayAlert("Message",errorMessage,"OK");
					}
					else
					{
						var x = DependencyService.Get<FormSample.Helpers.Utility.INetworkService>().IsReachable();
						if (!x)
						{
							progressService.Dismiss();
							await DisplayAlert("Message",Utility.NOINTERNETMESSAGE,"OK");
						}
						else if (await this.dataService.GetAgent(username.Text) == null)
						{
							progressService.Dismiss();
							await DisplayAlert("Message",Utility.INVALIDUSERMESSAGE,"OK");
						}
						else
						{
							var result = await dataService.ForgotPassword(username.Text);
							if(result != null)
							{
								progressService.Dismiss();
								await DisplayAlert("Message","Password has been sent to your email...","OK");
							}
						}
					}
				}
				catch(Exception) {
					progressService.Dismiss();
					DisplayAlert("Message",Utility.SERVERERRORMESSAGE,"OK");
					//MessagingCenter.Send(this,"msg",Utility.SERVERERRORMESSAGE);
				}
			};

			var loginButton = new Button { Text = "Sign In",BackgroundColor = Color.FromRgb(34,73,138),
				TextColor=Color.White};
			loginButton.SetBinding(Button.CommandProperty, LoginViewModel.LoginCommandPropertyName);

			var registerButton = new Button { Text = "I don't have a recruiter account..", BackgroundColor=Color.FromHex("3b73b9"),TextColor = Color.White};
			registerButton.SetBinding(Button.CommandProperty, LoginViewModel.GoToRegisterCommandPropertyName);

			var downloadButton = new Button { Text = "Download Terms and Conditions", BackgroundColor = Color.FromHex("f7941d"),TextColor = Color.White};
			downloadButton.Clicked += (object sender, EventArgs e) => {
				DependencyService.Get<FormSample.Helpers.Utility.IUrlService> ().OpenUrl (Utility.PDFURL);
			};

			var controlStakeLayout = new StackLayout (){ 
				Padding = new Thickness(Device.OnPlatform(5, 5, 5),0 , Device.OnPlatform(5, 5, 5), 0), //new Thickness(5,0,5,0),
				VerticalOptions = LayoutOptions.FillAndExpand, 
				HorizontalOptions = LayoutOptions.Fill,
				Orientation = StackOrientation.Vertical,
				Children = {userNameLabel,username,passwordLabel,password,forgotPassword}
			};

			var buttonStakeLayout = new StackLayout (){ 
				Orientation = StackOrientation.Vertical,
				Padding = new Thickness(Device.OnPlatform(5, 5, 5),Device.OnPlatform(topPadding,topPadding,topPadding), Device.OnPlatform(5, 5, 5),0) ,//new Thickness(5,0, 5,0)
				HorizontalOptions = LayoutOptions.Fill,
				Children= {loginButton,registerButton,downloadButton}
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
			GC.Collect ();
		}
	}


}

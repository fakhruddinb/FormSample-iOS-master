using System;
using Xamarin.Forms;
using FormSample.ViewModel;
using FormSample.Helpers;
using System.Threading.Tasks;

namespace FormSample.Views
{
	public class AmendDetailsPage : ContentPage
	{
		private IProgressService progressService;
		private DataService dataService;
		Entry firstName ;
		Entry lastName;
		Entry email;
		Entry phone;
		Entry agencyName;
		int id;

		public AmendDetailsPage ()
		{
			dataService = new DataService ();
			progressService = DependencyService.Get<IProgressService> ();
			var layout = this.AssignValues();
			this.Content = layout;
		}

		public StackLayout AssignValues()
		{
			//var buttonHeight = Utility.DEVICEHEIGHT * 18/ 100;
			var controlHeight = Utility.DEVICEHEIGHT * 65 / 100;
			var label = new Label
			{
				Text = "Amend Details",
				BackgroundColor = Color.Blue,
				TextColor = Color.White,
				VerticalOptions = LayoutOptions.Center,
				XAlign = TextAlignment.Center, // Center the text in the blue box.
				YAlign = TextAlignment.Center,
				Font = Font.SystemFontOfSize (NamedSize.Medium)
					.WithAttributes (FontAttributes.Bold)
			};

			var emailLabel = new Label { HorizontalOptions = LayoutOptions.Fill,Font = Font.SystemFontOfSize (NamedSize.Medium)
									.WithAttributes (FontAttributes.Bold)};
			emailLabel.Text = "Email";

			this.email= new Entry() { HorizontalOptions = LayoutOptions.FillAndExpand};
			this.email.IsEnabled = false;

			var firstNameLabel = new Label { HorizontalOptions = LayoutOptions.Fill,Font = Font.SystemFontOfSize (NamedSize.Medium)
					.WithAttributes (FontAttributes.Bold)};
			firstNameLabel.Text = "First Name";

			var lastNameLabel = new Label { HorizontalOptions = LayoutOptions.Fill,Font = Font.SystemFontOfSize (NamedSize.Medium)
					.WithAttributes (FontAttributes.Bold)};
			lastNameLabel.Text = "Last Name";

			this.firstName = new Entry() { HorizontalOptions = LayoutOptions.FillAndExpand };

			this.lastName = new Entry() { HorizontalOptions = LayoutOptions.FillAndExpand };
			firstName.Completed += (object sender, EventArgs e) => {
				lastName.Focus();
			};

			var agencyLabel = new Label { HorizontalOptions = LayoutOptions.Fill,Font = Font.SystemFontOfSize (NamedSize.Medium)
					.WithAttributes (FontAttributes.Bold)};
			agencyLabel.Text = "Agency";

			this.agencyName = new Entry() { HorizontalOptions = LayoutOptions.FillAndExpand };
			lastName.Completed += (object sender, EventArgs e) => {
				agencyName.Focus();
			};

			var phoneLabel = new Label { HorizontalOptions = LayoutOptions.Fill,Font = Font.SystemFontOfSize (NamedSize.Medium)
					.WithAttributes (FontAttributes.Bold)};
			phoneLabel.Text = "Phone number";

			this.phone = new Entry() { HorizontalOptions = LayoutOptions.FillAndExpand};
			this.phone.Keyboard = Keyboard.Telephone;
			agencyName.Completed += (object sender, EventArgs e) => {
				phone.Focus();
			};

			this.BindAgent();

			Button btnUpdate = new Button
			{
				HorizontalOptions = LayoutOptions.Fill,
				BackgroundColor = Color.FromHex("#22498a"),
				TextColor = Color.White,
				Text = "Update"
			};
			btnUpdate.Clicked += async (object sender, EventArgs e) => 
			{
				await  ExecuteUpdateCommand();
			};

			var contactUsButton = new Button { Text = "Contact Us", BackgroundColor = Color.FromHex("0d9c00"), TextColor = Color.White };
			contactUsButton.Clicked += delegate
			{
				App.RootPage.NavigateTo("Contact us");
			};

			var labelStakeLayout = new StackLayout (){ 
				Children= {label}
			};

			var controlStakeLayout = new StackLayout (){ 
				Padding = new Thickness(Device.OnPlatform(5, 5, 5), 0, Device.OnPlatform(5, 5, 5), 0), //new Thickness(5,0,5,0),
				VerticalOptions = LayoutOptions.FillAndExpand, 
				HorizontalOptions = LayoutOptions.Fill,
				Orientation = StackOrientation.Vertical,
				Children = 
				{emailLabel, email, firstNameLabel, firstName, lastNameLabel, lastName, agencyLabel, 
					agencyName, phoneLabel, phone}
				};

			var scrollableContentLayout = new StackLayout (){ 
				//Content = controlStakeLayout,
				Children = {controlStakeLayout},
				Orientation = StackOrientation.Vertical,
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.End,
				HeightRequest = controlHeight
			};

			var buttonLayout = new StackLayout (){ 
				Padding = new Thickness(Device.OnPlatform(5, 5, 5),0, Device.OnPlatform(5, 5, 5), 0), //new Thickness(5,0,5,0),
				VerticalOptions = LayoutOptions.End, 
				Orientation = StackOrientation.Vertical,
				//HeightRequest = buttonHeight,
				Children= {btnUpdate, contactUsButton}
			};

			var nameLayout = new StackLayout()
			{
				Children = 
				{ labelStakeLayout, scrollableContentLayout,buttonLayout}
				};
			return new StackLayout{Children= {nameLayout}};
		}

		private void BindAgent()
		{
			AgentDatabase d = new AgentDatabase();
			var agentToUpdate = d.GetAgentByEmail(Settings.GeneralSettings);
			if (agentToUpdate != null)
			{
				id = agentToUpdate.Id;
				firstName.Text = agentToUpdate.FirstName;
				lastName.Text = agentToUpdate.LastName;
				email.Text = agentToUpdate.Email;
				agencyName.Text = agentToUpdate.AgencyName;
				phone.Text = agentToUpdate.Phone;
			}
		}

		private async Task ExecuteUpdateCommand()
		{
			try{

				progressService.Show();
				string errorMessage = string.Empty;

				if (string.IsNullOrWhiteSpace(this.firstName.Text))
				{
					errorMessage = errorMessage + Utility.FIRSTNAMEMESSAGE;
				}

				if (string.IsNullOrWhiteSpace(this.lastName.Text))
				{
					errorMessage = errorMessage + Utility.LASTNAMEMESSAGE;
				}

				if (string.IsNullOrWhiteSpace(this.agencyName.Text))
				{
					errorMessage = errorMessage + Utility.AGENCYMESSAGE;
				}

				if (!string.IsNullOrEmpty(errorMessage))
				{
					progressService.Dismiss();
					await this.DisplayAlert("Message", errorMessage, "OK");
				}
				else
				{
					var a = new Agent()
					{
						Id = this.id,
						Email = this.email.Text,
						FirstName = this.firstName.Text,
						LastName = this.lastName.Text,
						Phone = this.phone.Text,
						AgencyName = this.agencyName.Text
					};

					var networkService = DependencyService.Get<FormSample.Helpers.Utility.INetworkService>().IsReachable();
					if (networkService)
					{
						await dataService.UpdateAgent(a);
					}

					UpdateAgent(a);
					progressService.Dismiss();
					App.RootPage.NavigateTo("Home");

				}
			}
			catch(Exception) {

				this.DisplayAlert("Message", Utility.SERVERERRORMESSAGE, "OK");
			}
		}
		private void UpdateAgent(Agent agentToUpdate)
		{
			AgentDatabase agent = new AgentDatabase();
			agent.SaveItem(agentToUpdate);
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			GC.Collect ();
		}
	}

}


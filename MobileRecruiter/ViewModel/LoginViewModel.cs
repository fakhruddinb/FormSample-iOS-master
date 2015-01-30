using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormSample.Helpers;

namespace FormSample.ViewModel
{
	using System.Diagnostics;

	using FormSample.Views;

	using Xamarin.Forms;

	public class LoginViewModel : BaseViewModel
	{
		private IProgressService progressService;
		private DataService dataService;
		private UploadService uploadService;
		ILoginManager ilm;
		private INavigation navigation;

		public LoginViewModel(INavigation navigation,ILoginManager ilm)
		{
			this.navigation = navigation;
			this.dataService = new DataService();
			this.uploadService = new UploadService ();
			progressService = DependencyService.Get<IProgressService>();
			this.ilm = ilm;
		}

		public const string UsernamePropertyName = "Username";
		private string username = string.Empty;
		public string Username
		{
			get { return username; }
			set { this.ChangeAndNotify(ref username, value, UsernamePropertyName); }
		}

		public const string PasswordPropertyName = "Password";
		private string password = string.Empty;
		public string Password
		{
			get { return password; }
			set { this.ChangeAndNotify(ref password, value, PasswordPropertyName); }
		}

		private Command forgotPasswordCommand;
		public const string ForgotPasswordCommandPropertyName = "ForgotPasswordCommand";
		public Command ForgotPasswordCommand
		{
			get{ 
				return forgotPasswordCommand ?? (forgotPasswordCommand = new Command (async () => await ExecuteForgotPasswordCommand ()));
			}
		}

		protected async Task ExecuteForgotPasswordCommand()
		{
			try
			{
				string errorMessage = string.Empty;
				progressService.Show();
			
				if(string.IsNullOrWhiteSpace(this.Username))
				{

					errorMessage = errorMessage + Utility.EAMAILMESSAGE;
				}
				else if (!Utility.IsValidEmailAddress(this.Username))
				{
					errorMessage = errorMessage + Utility.INVALIDEMAILMESSAGE;
				}
				if(!string.IsNullOrEmpty(errorMessage))
				{
					progressService.Dismiss();
					MessagingCenter.Send(this,"msg",errorMessage);
				}
				else
				{
				var x = DependencyService.Get<FormSample.Helpers.Utility.INetworkService>().IsReachable();
				if (!x)
				{
					progressService.Dismiss();
					MessagingCenter.Send(this,"msg",Utility.NOINTERNETMESSAGE);
				}
				else if (await this.dataService.GetAgent(this.Username) == null)
				{
					progressService.Dismiss();
					MessagingCenter.Send(this,"msg",Utility.INVALIDUSERMESSAGE);
				}
				else
				{
						var result = await dataService.ForgotPassword(this.Username);
						if(result != null)
						{
							progressService.Dismiss();
							MessagingCenter.Send(this,"msg","Password has been sent to your email..");
						}
				}
				}
			}
			catch(Exception) {
				progressService.Dismiss();
				MessagingCenter.Send(this,"msg",Utility.SERVERERRORMESSAGE);
			}
		}

		private Command loginCommand;
		public const string LoginCommandPropertyName = "LoginCommand";
		public Command LoginCommand
		{
			get
			{
				return loginCommand ?? (loginCommand = new Command(async () => await ExecuteLoginCommand()));
			}
		}

		protected async Task ExecuteLoginCommand()
		{
			try
			{
				string errorMessage = string.Empty;
				this.progressService.Show();
				if(string.IsNullOrWhiteSpace(this.Username))
				{
					errorMessage = errorMessage + Utility.USERNAMEMESSAGE;
				}

				else if (!Utility.IsValidEmailAddress(this.Username))
				{
					errorMessage = errorMessage + Utility.INVALIDEMAILMESSAGE;
				}

				if(string.IsNullOrWhiteSpace(this.Password))
				{
					errorMessage = errorMessage + Utility.PASSWORDMESSAGE;

				}
				if(string.IsNullOrEmpty(errorMessage))
				{
					this.progressService.Show();
					var x = DependencyService.Get<FormSample.Helpers.Utility.INetworkService>().IsReachable();
					if (!x)
					{
						progressService.Dismiss();
						MessagingCenter.Send(this, "msg", Utility.NOINTERNETMESSAGE);
					}
					else
					{
						var encryptedPassword = DependencyService.Get<FormSample.Helpers.Utility.IpasswordConverter>().ConvertPasswordIntoMd5(this.Password);
						var response = await this.dataService.IsValidUser(this.Username, encryptedPassword);// just emailid and password return kare 6
						//var response = await this.dataService.GetAgent(this.Username);
						if(response != null && response.Email != null )
						{
							//this.progressService.Show();
							Settings.GeneralSettings = this.Username;
							this.AddAgentToLocalDatabase(response);
							await uploadService.UpdatePaytableDataFromService();
							ilm.ShowMainPage();
						}
						else 
						{
							progressService.Dismiss();
							MessagingCenter.Send(this, "msg", Utility.INCORRECTUSERNAMEORPASSWORD);
						}
					}
				}
				else
				{
					progressService.Dismiss();
					MessagingCenter.Send(this,"msg",errorMessage);
				}

			}
			catch (Exception )
			{
				progressService.Dismiss();
				MessagingCenter.Send(this,"msg",Utility.SERVERERRORMESSAGE);
			}
		}

		private Command goToRegisterCommand;
		public const string GoToRegisterCommandPropertyName = "GoToRegisterCommand";
		public Command GoToRegisterCommand
		{
			get
			{
				return goToRegisterCommand ?? (goToRegisterCommand = new Command(async () => await ExecuteGoToRegisterCommand()));
			}
		}

		protected async Task ExecuteGoToRegisterCommand()
		{
			try
			{
				MessagingCenter.Send(this, "Create");
			}
			catch (Exception)
			{
			}
		}

		private void AddAgentToLocalDatabase(Agent responseFromServer)
		{
			FormSample.AgentDatabase d = new AgentDatabase();
			var existingAgent = d.GetAgentByEmail(responseFromServer.Email);
			if(existingAgent==null)
			{ 
				d.SaveItem(responseFromServer); 
			}
		}

	}
}

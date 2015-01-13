using System;
using FormSample.ViewModel;
using System.Threading.Tasks;
using FormSample.Helpers;
using System.Linq;
using System.Collections.Generic;

namespace FormSample.ViewModel
{
	using FormSample.Views;

	using Xamarin.Forms;

	public class AgentViewModel : BaseViewModel
	{
		private IProgressService progressService;
		private DataService dataService;
		private UploadService uploadService;
		private INavigation navigation;
		public ILoginManager ilm { get; set; }

		public AgentViewModel(INavigation navigation, ILoginManager ilm )
		{
			this.navigation = navigation;
			this.dataService = new DataService();
			this.uploadService = new UploadService ();
			progressService = DependencyService.Get<IProgressService> ();
			this.ilm = ilm;
		}

		public const string IdPropertyName = "Id";
		private int id = 0;
		public int Id
		{
			get { return this.id; }
			set { this.ChangeAndNotify(ref this.id, value, IdPropertyName); }
		}

		public const string AgentEmailPropertyName = "Email";
		private string email = string.Empty;
		public string Email
		{
			get { return this.email; }
			set { this.ChangeAndNotify(ref this.email, value, AgentEmailPropertyName); }
		}

		public const string FirstNamePropertyName = "FirstName";

		private string firstName = string.Empty;
		public string FirstName
		{
			get { return this.firstName; }
			set { this.ChangeAndNotify(ref this.firstName, value, FirstNamePropertyName); }
		}

		public const string LastNamePropertyName = "LastName";
		private string lastName = string.Empty;
		public string LastName
		{
			get { return this.lastName; }
			set { this.ChangeAndNotify(ref this.lastName, value, LastNamePropertyName); }
		}

		public const string PhonePropertyName = "Phone";
		private string phone = string.Empty;
		public string Phone
		{
			get { return this.phone; }
			set { this.ChangeAndNotify(ref this.phone, value, PhonePropertyName); }
		}

		public const string AgencyNamePropertyName = "AgencyName";
		private string agencyName = string.Empty;
		public string AgencyName
		{
			get { return this.agencyName; }
			set { this.ChangeAndNotify(ref this.agencyName, value, AgencyNamePropertyName); }
		}

		public const string isCheckedPropertyName = "IsChecked";
		private bool isChecked = false;
		public bool IsChecked
		{
			get { return this.isChecked; }
			set { this.ChangeAndNotify(ref this.isChecked, value, isCheckedPropertyName); }
		}

		private Command submitCommand;
		public const string SubmitCommandPropertyName = "SubmitCommand";
		public Command SubmitCommand
		{
			get
			{
				return submitCommand ?? (submitCommand = new Command(async () => await ExecuteSubmitCommand()));
			}
		}

		protected async Task ExecuteSubmitCommand()
		{
			try
			{
				this.progressService.Show();
				string errorMessage = string.Empty;

				if (string.IsNullOrWhiteSpace(this.Email))
				{
					errorMessage = errorMessage + Utility.EAMAILMESSAGE;
				}
				else if (!Utility.IsValidEmailAddress(this.Email))
				{
					errorMessage = errorMessage + Utility.INVALIDEMAILMESSAGE;
				}

				if (string.IsNullOrWhiteSpace(this.FirstName))
				{
					errorMessage = errorMessage + Utility.FIRSTNAMEMESSAGE;
				}

				if (string.IsNullOrWhiteSpace(this.LastName))
				{
					errorMessage = errorMessage + Utility.LASTNAMEMESSAGE;
				}

				if (string.IsNullOrWhiteSpace(this.AgencyName))
				{
					errorMessage = errorMessage + Utility.AGENCYMESSAGE;
				}
				if (!this.IsChecked)
				{
					errorMessage = errorMessage + Utility.TERMSANDCONDITIONMESSAGE;
				}

				if (!string.IsNullOrEmpty(errorMessage))
				{
					this.progressService.Dismiss();
					MessagingCenter.Send(this, "msg", errorMessage);
				}
				else
				{
					var x = DependencyService.Get<FormSample.Helpers.Utility.INetworkService>().IsReachable();
					if(!x)
					{
						this.progressService.Dismiss();
						MessagingCenter.Send(this, "msg", Utility.NOINTERNETMESSAGE);
					}
					else if (await this.dataService.GetAgent(this.Email)!=null)
					{
						this.progressService.Dismiss();
						MessagingCenter.Send(this, "msg", Utility.EMAILALREADYEXISTMESSAGE);
					}
					else
					{
						var a = new Agent()
						{
							Id = this.Id,
							Email = this.Email,
							FirstName = this.FirstName,
							LastName = this.LastName,
							Phone = this.Phone,
							AgencyName = this.AgencyName
						};

						var result = await dataService.AddAgent(a);
						if (result != null && !string.IsNullOrWhiteSpace(this.Email))
						{
							Settings.GeneralSettings = this.Email;
							this.AddAgentToLocalDatabase(result);
							await uploadService.UpdatePaytableDataFromService();
						}
						ilm.ShowMainPage();
					}
				}
			}
			catch (Exception)
			{
				progressService.Dismiss();
				MessagingCenter.Send(this, "msg", Utility.SERVERERRORMESSAGE);
			}

		}

		private Command gotoLoginCommand;
		public const string GotoLoginCommandPropertyName = "GotoLoginCommand";
		public Command GotoLoginCommand
		{
			get
			{
				return gotoLoginCommand ?? (gotoLoginCommand = new Command(async () => await ExecuteGotoLoginCommand()));
			}
		}

		protected async Task ExecuteGotoLoginCommand()
		{
			try
			{
				MessagingCenter.Send (this, "Login");
			}
			catch { }
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

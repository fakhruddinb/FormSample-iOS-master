using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms.Labs;
using Xamarin.Forms.Labs.Services;

namespace FormSample.Helpers
{
	public static class Utility
	{
		public static string PHONENO = "08082717377";
		public static string EMAIL = "agency@churchill-knight.co.uk";
		public static string GOOGLEPLUSURL = "https://plus.google.com/u/0/+Churchill-Knight/about";
		public static string LINKEDINURL = "http://www.linkedin.com/company/churchill-knight-&-associates-ltd?trk=hb_tab_compy_id_1398435";
		public static string PDFURL = "http://www.churchill-knight.co.uk/assets/tandc.pdf";
		public static string CHURCHILKNIGHTURL="http://www.churchill-knight.co.uk/recruiters-zone/";
		public static string LATITUDE = "51.696975";
		public static string LONGITUDE = "-0.191415";
		public static int DEVICEWIDTH;
		public static int DEVICEHEIGHT;

		#region custome messages
		public static string EAMAILMESSAGE = "Email is required.\n";
		public static string INVALIDEMAILMESSAGE = "Please enter valid email address.\n";
		public static string FIRSTNAMEMESSAGE = "Firstname is required.\n";
		public static string LASTNAMEMESSAGE = "Lastname is required.\n";
		public static string AGENCYMESSAGE = "Agency name is required.\n"; 
		public static string TERMSANDCONDITIONMESSAGE = "You must agree to our Terms & Condition. Please check checkbox below to agree.";
		public static string NOINTERNETMESSAGE = "Could not connect to the internet.";
		public static string EMAILALREADYEXISTMESSAGE = "Email already exist.";
		public static string SERVERERRORMESSAGE = "Something went wrong. please try again letter...";
		public static string USERNAMEMESSAGE = "Username is required.\n";
		public static string PASSWORDMESSAGE = "Password is required.";
		public static string INVALIDUSERMESSAGE = "Invalid user.";
		public static string INCORRECTUSERNAMEORPASSWORD = "Username or password is incorrect.";

		#endregion

		public static bool IsValidEmailAddress(string email)
		{
			string pattern = "(\\w[-._\\w]*\\w@\\w[-._\\w]*\\w\\.\\w{2,3})";
			Match emailAddressMatch = Regex.Match(email, pattern);
			return emailAddressMatch.Success;
		}

		public interface INetworkService
		{
			bool IsReachable();
		}
		public interface IDeviceService
		{
			void Call(string phoneNumber);
		}

		public interface IEmailService
		{
			void OpenEmail (string email);
		}

		public interface IUrlService
		{
			void OpenUrl (string url);
		}


		public interface IMapService
		{
			void OpenMap ();
		}

		public interface IpasswordConverter
		{
			string ConvertPasswordIntoMd5(string password);
		}
	}
}

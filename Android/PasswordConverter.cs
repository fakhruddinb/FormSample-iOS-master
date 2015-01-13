using FormSample.Droid;
using System;
using System.Text;
using Xamarin.Forms;
using System.Security.Cryptography;

[assembly: Dependency(typeof(PasswordConverter))]
namespace FormSample.Droid
{
	public class PasswordConverter : FormSample.Helpers.Utility.IpasswordConverter
	{
		public String ConvertPasswordIntoMd5(string password)
		{
			//client
			byte[] passwordBuffer = System.Text.Encoding.ASCII.GetBytes(password);
			System.Security.Cryptography.MD5 md5 = MD5.Create();
			byte[] hash = md5.ComputeHash(passwordBuffer);
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < hash.Length; i++)
			{
				sb.Append(hash[i].ToString("X2"));
			}
			return sb.ToString();
		}
	}
}
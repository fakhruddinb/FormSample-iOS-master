using FormSample.Helpers;
using System;


namespace FormSample
{
	using Xamarin.Forms;

	public interface IBaseUrl { string Get(); }
	// required temporarily for iOS, due to BaseUrl bug
	public class BaseUrlWebView : WebView { }

	public class AboutusPage : ContentPage
	{
		public AboutusPage()
		{
			var lblTitle = new Label {
				Text = "About Churchill Knight & Associate Ltd.",
				BackgroundColor = Color.Black,
				Font = Font.SystemFontOfSize(NamedSize.Large),
				TextColor = Color.White,
				VerticalOptions = LayoutOptions.Center,
				XAlign = TextAlignment.Center, // Center the text in the blue box.
				YAlign = TextAlignment.Center
			};

			var browser = new BaseUrlWebView(); // temporarily use this so we can custom-render in iOS
			var htmlSource = new HtmlWebViewSource();
			htmlSource.Html = GetContent();
			if (Device.OS != TargetPlatform.iOS)
			{
				htmlSource.BaseUrl = DependencyService.Get<IBaseUrl>().Get();
			}
			browser.Source = htmlSource;
			browser.VerticalOptions = LayoutOptions.FillAndExpand;
			var downloadButton = new Button{Text = "Download terms and conditions",BackgroundColor = Color.FromHex("f7941d"), TextColor = Color.White};
			downloadButton.Clicked +=  (object sender, EventArgs e) => {
				DependencyService.Get<FormSample.Helpers.Utility.IUrlService> ().OpenUrl (Utility.PDFURL);
			};

			var contactUsButton = new Button{Text = "Contact us",BackgroundColor = Color.FromHex("0d9c00"), TextColor = Color.White};

			contactUsButton.Clicked += (object sender, EventArgs e)=>
			{
				App.RootPage.NavigateTo("Contact us");
			};

			var labelStakeLayout = new StackLayout (){ 
				Children = {lblTitle},
				Orientation = StackOrientation.Vertical,
			};

			var buttonLayout = new StackLayout (){ 
				Orientation = StackOrientation.Vertical,
				Padding = new Thickness(Device.OnPlatform(5, 5, 5),0 , Device.OnPlatform(5, 5, 5), 0), //new Thickness(5,0,5,0),
				Children= { downloadButton, contactUsButton}
			};

			var layout = new StackLayout()
			{
				VerticalOptions = LayoutOptions.Fill,
				HorizontalOptions = LayoutOptions.Fill,
				Orientation = StackOrientation.Vertical,
				Children = { labelStakeLayout,browser,buttonLayout},
				WidthRequest = 200,
				HeightRequest = 200
			};

			Content = layout;
		}

		private string GetContent()
		{
			return "We have a fantastic Agency Sales Team dedicated to building long-lasting, responsive relationships with recruitment agencies. " +
				"<p>Would you like your recruitment agency and your candidates to benefit from a seamless partnership with an expert, experienced firm of " +
				"accountants that specialise in the freelance and contractor market? </p>" +
				"<p>We are the preferred supplier of several leading recruitment specialists, including leading edge IT recruitment consultancies who are reaping" +
				" rewards from our generous incentive programme. Because we can set up contractors fast, " +
				"and they are happier with their take home pay and simple accounting processes, you get repeat contractors and easier placements. </p>" +
				"<p><b>What can we offer you? </b>" +
				"<p>A preferential service. You and your candidates are our top priority. " +
				"<p>Incentives for every successful candidate you introduce to us regardless of whether they take your contract or not. " +
				"<p> We can help you raise your contracts within 24 hours by providing your candidates with all the necessary information and support. " +
				"<p> We will keep you updated with the progress of your candidates account, so that you are always aware of their status. " +
				"<p><b> More benefits for you </b>" +
				" <ul><li>You will have your own personal account manager.</li>" +
				" <li> We offer ways to help you with new agency legislation that affects your candidates. </li>" +
				" <li>We offer a tailored service to suit the needs of you and your candidate. </li>" +
				" <li> We will always provide your candidates with all of the options available to them. </li>" +
				"<li> Face to face meetings with a member of our Agency Sales Team, wherever you are in the country. </li>" +
				" <li>Relationship building - including social events and promotions. </li></ul>" +
				" <p>We have looked after recruiters for many years, so we understand your needs." +
				" <p><b> Professional passport and APSCo affiliation </b>" +
				"<p> We know compliance is the most important thing to consider when dealing with contract workers due the current legislations enforced by HMRC. For this reason we have continually strived to improve our services and ensure that contract workers receive the most professional and compliant services. Therefore we are very proud to announce our latest accreditations:" +
				" <p> We are now accredited by Professional Passport. Professional Passport audits service providers to the flexible workers market, helping to ensure their services meet the regulatory requirements." +
				" <p> We also recently became an APSCo affiliate. APSCo is the only trade body dedicated to representing the interests of the UK Professional Staffing industry, ensuring that members offer the highest possible standards of trading practice." +
				" <table width='100%'><tr><td width='200px'> <img src='Images/Accountancy.jpg' width=150 height=150 /></td>" +
				"<td width='200px'> <img src='Images/APSCo.jpg' width=150 height=150 /></td></tr></table> "; // [Passport Logo Image] [APSCo Logo Image]
		}
	}
}

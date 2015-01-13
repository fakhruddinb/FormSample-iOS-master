using FormSample;
using MobileRecruiter.iOS;
using MonoTouch.Foundation;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BaseUrlWebView), typeof(BaseUrlWebViewRenderer))]
namespace MobileRecruiter.iOS
{
    public class BaseUrlWebViewRenderer: WebViewRenderer   
    {
        public override void LoadHtmlString(string s, NSUrl baseUrl)
        {
            baseUrl = new NSUrl(NSBundle.MainBundle.BundlePath, true);
            base.LoadHtmlString(s, baseUrl);
        }
    }
}

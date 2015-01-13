using FormSample;
using MobileRecruiter.iOS;
using MonoTouch.Foundation;
using Xamarin.Forms;
using Xamarin.Forms.Labs.iOS.Services;

[assembly: Dependency(typeof(BaseUrlTouch))]
namespace MobileRecruiter.iOS
{
    public class BaseUrlTouch : IBaseUrl
    {
        public string Get()
        { 
            return NSBundle.MainBundle.BundlePath;
        }
    }
}


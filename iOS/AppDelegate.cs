using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Xamarin.Forms;
using FormSample;
 
using Syncfusion.SfChart.XForms.iOS.Renderers;
using FormSample.Helpers;

namespace MobileRecruiter.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate,  ILoginManager
    {
        UIWindow window;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
			//new SfChartRenderer();
            Forms.Init();

            window = new UIWindow(UIScreen.MainScreen.Bounds);
			if (string.IsNullOrWhiteSpace (Settings.GeneralSettings)) {
				window.RootViewController = App.GetMainPage(this).CreateViewController();
			} else {
				window.RootViewController = App.GetLoginPage (this).CreateViewController ();
			}
			window.MakeKeyAndVisible ();
            //window.RootViewController = App.GetMainPage(this).CreateViewController();
            //window.MakeKeyAndVisible();
			
            return true;
        }

        public void ShowMainPage()
        {
            window.RootViewController = App.GetMainPage(this).CreateViewController();
            window.MakeKeyAndVisible();
        }

        public void ShowLoginPage()
        {
            window.RootViewController = App.GetLoginPage(this).CreateViewController();
            window.MakeKeyAndVisible();
        }
    }
}


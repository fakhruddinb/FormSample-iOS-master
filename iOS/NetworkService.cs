using System;
using MobileRecruiter.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Labs.iOS.Services;

[assembly:Dependency(typeof(NetworkService))]
namespace MobileRecruiter.iOS
{
    public class NetworkService : FormSample.Helpers.Utility.INetworkService
    {
        #region INetworkService implementation

        public bool IsReachable()
        {
            return Reachability.InternetConnectionStatus() != Xamarin.Forms.Labs.Services.NetworkStatus.NotReachable;
        }
        #endregion
    }
}


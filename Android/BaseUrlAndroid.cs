using System;
using Xamarin.Forms;
using FormSample.Droid;


[assembly: Dependency(typeof(BaseUrlAndroid))]
namespace FormSample.Droid
{
   public class BaseUrlAndroid : IBaseUrl
    {
        public string Get()
        {
            return "file:///android_asset/";
        }
    }
}
using FormSample;
using MonoTouch.CoreLocation;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileRecruiter.iOS.Services
{
    

   public  class LocationManager
    {
        private NetworkService networkService;

        protected CLLocationManager location;
        public CLLocationManager Location
        {
            get{
                return this.location;
            }
        }

        public LocationManager()
        {
            this.location = new CLLocationManager();
            LocationUpdated += PrintLocation;
            this.networkService = new NetworkService();
        }

        public event EventHandler<LocationUpdatedEventArgs> LocationUpdated = delegate { };

        public void StartLocationUpdates ()
		{
			if (CLLocationManager.LocationServicesEnabled) {

				Location.DesiredAccuracy = 1; // sets the accuracy that we want in meters

				// Location updates are handled differently pre-iOS 6. If we want to support older versions of iOS,
				// we want to do perform this check and let our LocationManager know how to handle location updates.

				if (UIDevice.CurrentDevice.CheckSystemVersion (6, 0)) {

					Location.LocationsUpdated += (object sender, CLLocationsUpdatedEventArgs e) => {
						// fire our custom Location Updated event
						this.LocationUpdated (this, new LocationUpdatedEventArgs (e.Locations [e.Locations.Length - 1]));
					};

				} else {

					// this won't be called on iOS 6 (deprecated). We will get a warning here when we build.
					Location.UpdatedLocation += (object sender, CLLocationUpdatedEventArgs e) => {
						this.LocationUpdated (this, new LocationUpdatedEventArgs (e.NewLocation));
					};
				}

				// Start our location updates
				Location.StartUpdatingLocation ();

				// Get some output from our manager in case of failure
				Location.Failed += (object sender, NSErrorEventArgs e) => {
					Console.WriteLine (e.Error);
				}; 

			} else {

				//Let the user know that they need to enable LocationServices
				Console.WriteLine ("Location services not enabled, please enable this in your Settings");

			}
        }

            //This will keep going in the background and the foreground
		public void PrintLocation (object sender, LocationUpdatedEventArgs e) 
		{
            if(this.networkService.IsReachable())
            {
				Console.WriteLine("Uploading Started : " + DateTime.Now.ToShortTimeString());
				UploadService uploadservice = new UploadService();
				uploadservice.UploadContractorData();
				uploadservice.UploadAgentDetail();
				Console.WriteLine("Uploading finisheds : " + DateTime.Now.ToShortTimeString());
            }
		}
		}

 

    public class LocationUpdatedEventArgs : EventArgs
	{
		CLLocation location;

		public LocationUpdatedEventArgs(CLLocation location)
		{
			this.location = location;
            
		}

		public CLLocation Location
		{
			get { return location; }
		}
	}

}



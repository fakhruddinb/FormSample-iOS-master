using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Net;
using Android.Util;
using FormSample.Helpers;


namespace FormSample.Droid.Services
{
	/// <summary>
	/// The network receiver.
	/// </summary>
	[BroadcastReceiver]
	[IntentFilter(new[] { Android.Net.ConnectivityManager.ConnectivityAction },
		Categories = new[] { Android.Content.Intent.CategoryDefault })]
	public class NetworkReceiver : BroadcastReceiver
	{
		public override void OnReceive(Context context, Intent intent)
		{
			if ((intent.Action == null) || (intent.Action != Android.Net.ConnectivityManager.ConnectivityAction))
			{
				return;
			}
			var connectivityManager = (ConnectivityManager)context.GetSystemService(Context.ConnectivityService);
			var wifiState = connectivityManager.GetNetworkInfo(ConnectivityType.Wifi);
			var mobileState = connectivityManager.GetNetworkInfo(ConnectivityType.Mobile);
			if ((wifiState != null && wifiState.IsConnected) || (mobileState != null && mobileState.IsConnected))
			{
				//// Start the service
				context.ApplicationContext.StartService(new Intent(context, typeof(BackgroundDataService)));
			}
			else
			{
				//// Stop the service
				context.ApplicationContext.StopService(new Intent(context, typeof(BackgroundDataService)));
			}
		}
	}
	[Service]
	public class BackgroundDataService : Android.App.Service
	{
		private NetworkService networkService;
		public override IBinder OnBind(Intent intent)
		{
			return null;
		}
		/// <summary>
		/// The on create.
		/// </summary>
		public override void OnCreate()
		{
			base.OnCreate();
			this.networkService = new NetworkService();
			//this.syncDownloadService = Container.Current.Resolve<ISyncDownloadDataService>();
			//this.syncUploadService = Container.Current.Resolve<ISyncUploadDataService>();
			LogHandler.LogDebug("Background Data service on create");
		}
		/// <summary>
		/// The on start command.
		/// </summary>
		/// <param name="intent"> The intent.
		/// </param> <param name="flags"> The flags.
		/// </param> <param name="startId"> The start id. </param>
		/// <returns> The <see cref="StartCommandResult"/>. </returns>
		public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
		{
			try
			{
				if (this.networkService.IsReachable())
				{
					LogHandler.LogDebug("Downloading Started : " + DateTime.Now.ToShortTimeString());
					UploadService uploadservice = new UploadService();
					uploadservice.UploadContractorData();
					uploadservice.UploadAgentDetail();
					LogHandler.LogDebug("Uploading Started : " + DateTime.Now.ToShortTimeString());
					return StartCommandResult.Sticky;
				}
			}
			catch (Exception ex)
			{
				var msg = string.Format("BackGroundDataService : {0}", ex.Message);
			}
			return base.OnStartCommand(intent, flags, startId);
		}
		/// <summary>
		/// The on low memory.
		/// </summary>
		public override void OnLowMemory()
		{
			base.OnLowMemory();
			LogHandler.LogDebug("On Low memory in service : " + DateTime.Now.ToShortTimeString());
		}
		/// <summary>
		/// The on destroy.
		/// </summary>
		public override void OnDestroy()
		{
			base.OnDestroy();
			this.networkService = null;
			LogHandler.LogDebug("Service Stopped : " + DateTime.Now.ToShortTimeString());
		}
		/// <summary>
		/// The dump.
		/// </summary>
		/// <param name="fd">
		/// The file descriptor.
		/// </param>
		/// <param name="writer">
		/// The writer.
		/// </param>
		/// <param name="args">
		/// The args.
		/// </param>
		protected override void Dump(Java.IO.FileDescriptor fd, Java.IO.PrintWriter writer, string[] args)
		{
			base.Dump(fd, writer, args);
			LogHandler.LogDebug("On dump in service : " + DateTime.Now.ToShortTimeString());
		}
	}
	/// <summary>
	/// The log handler.
	/// </summary>
	public static class LogHandler
	{
		/// <summary>
		/// The tag.
		/// </summary>
		private const string Tag = "ProjectName";
		/// <summary>
		/// The log error.
		/// </summary>
		/// <param name="message">
		/// The message.
		/// </param>
		public static void LogError(string message)
		{
			Log.Error(Tag, message);
		}
		/// <summary>
		/// The log debug.
		/// </summary>
		/// <param name="message">
		/// The message.
		/// </param>
		public static void LogDebug(string message)
		{
			Log.Debug(Tag, message);
		}
	}
}
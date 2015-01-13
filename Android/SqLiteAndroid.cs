using System;
using System.IO;
using Xamarin.Forms;
using FormSample.Droid;
using FormSample.Helpers;

[assembly: Dependency(typeof(SQLite_Android))]
namespace FormSample.Droid
{

	public class SQLite_Android : ISqLite
	{
		public SQLite_Android()
		{
		}

		#region ISQLite implementation
		public SQLite.Net.SQLiteConnection GetConnection()
		{
			var sqliteFilename = "TodoSQLite.db3";
			string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
			var finalPath = Path.Combine (documentsPath, Settings.GeneralSettings);
			if (!Directory.Exists (finalPath)) {
				Directory.CreateDirectory (finalPath);
			}
			var path = Path.Combine(finalPath, sqliteFilename);

			//			// This is where we copy in the prepopulated database
			//			Console.WriteLine (path);
			//			if (!File.Exists(path))
			//			{
			//				var s = Forms.Context.Resources.OpenRawResource(Resource.Raw.TodoSQLite);  // RESOURCE NAME ###
			//
			//				// create a write stream
			//				FileStream writeStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
			//				// write to the stream
			//				ReadWriteStream(s, writeStream);
			//			}
			var plat = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
			var conn = new SQLite.Net.SQLiteConnection(plat, path);

			// Return the database connection 
			return conn;
		}
		#endregion

		/// <summary>
		/// helper method to get the database out of /raw/ and into the user filesystem
		/// </summary>
		void ReadWriteStream(Stream readStream, Stream writeStream)
		{
			int Length = 256;
			Byte[] buffer = new Byte[Length];
			int bytesRead = readStream.Read(buffer, 0, Length);
			// write the required bytes
			while (bytesRead > 0)
			{
				writeStream.Write(buffer, 0, bytesRead);
				bytesRead = readStream.Read(buffer, 0, Length);
			}
			readStream.Close();
			writeStream.Close();
		}
	}
}


using System;
using System.Collections.Generic;
using System.Text;

using MobileRecruiter.iOS;
using Xamarin.Forms;
using FormSample;
using System.IO;
using FormSample.Helpers;

[assembly: Dependency(typeof(SQLite_iOS))]

namespace MobileRecruiter.iOS
{
    public class SQLite_iOS : ISqLite
    {
        public SQLite_iOS() { }
        public SQLite.Net.SQLiteConnection GetConnection()
        {
            var sqliteFilename = "TodoSQLite.db3";
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder

            var finalPath = Path.Combine(libraryPath, Settings.GeneralSettings);
            if (!Directory.Exists(finalPath))
            {
                Directory.CreateDirectory(finalPath);
            }

            var path = Path.Combine(finalPath, sqliteFilename);
            // Create the connection
            var plat = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
            var conn = new SQLite.Net.SQLiteConnection(plat, path);
            // Return the database connection
            return conn;
        }
    }
}

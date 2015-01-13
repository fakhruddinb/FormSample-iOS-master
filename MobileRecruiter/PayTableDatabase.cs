using System;
using SQLite.Net;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;

namespace FormSample
{
	public class PayTableDatabase
	{
		SQLiteConnection database;
		static object locker = new object ();

		public PayTableDatabase() {
			database = DependencyService.Get<ISqLite> ().GetConnection ();
			database.CreateTable<PayTable>();
		}

		public IEnumerable<PayTable> GetPayTables () {
			return (from i in database.Table<PayTable>() select i).ToList();
		}

		public PayTable GetPayTable (int id) 
		{
			return database.Table<PayTable>().FirstOrDefault(x => x.Id == id);
		}

		public PayTable GetPayTableTaxablePay (double taxablePay) 
		{
			return database.Table<PayTable>().FirstOrDefault(x => x.TaxablePay == taxablePay );
		}

		public int SaveItem (PayTable item) 
		{
			lock (locker) {
				if (item.Id != 0) {
					database.Update(item);
					return item.Id;
				} else {
					return database.Insert(item);
				}
			}
		}

		public int DeleteItem(int id)
		{
			lock (locker) {
				return database.Delete<PayTable>(id);
			}
		}
	}
}


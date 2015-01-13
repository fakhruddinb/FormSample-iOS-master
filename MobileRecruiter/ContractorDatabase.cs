using System;
using SQLite.Net;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using FormSample.Helpers;

namespace FormSample
{
	public class ContractorDatabase
	{
		SQLiteConnection database;
		static object locker = new object();
		public ContractorDatabase ()
		{
			database = DependencyService.Get<ISqLite> ().GetConnection ();
			database.CreateTable<Contractor> ();
		}

		public IEnumerable<Contractor> GetContractors (string agentEmail) {
			//return (from i in database.Table<Contractor>() select i).ToList();
			return database.Table<Contractor> ().Where (x => x.AgentId == agentEmail).ToList ();
		}

		public Contractor GetContractor (int id) 
		{
			return database.Table<Contractor>().FirstOrDefault(x => x.Id == id);
		}

		public int SaveItem (Contractor item) 
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

		public int DeleteContractor(int id)
		{
			lock (locker) {
				return database.Delete<Contractor>(id);
			}
		}

		public int DeleteAllContractor(string agentEmail)
		{
			lock (locker) {
				return database.Delete<Contractor> (agentEmail);
			}
		}


	}
}


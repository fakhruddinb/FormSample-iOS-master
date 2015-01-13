using System;
using SQLite.Net;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;

namespace FormSample
{
	public  class AgentDatabase
	{
		SQLiteConnection database;
		static object locker = new object ();

		public AgentDatabase() {
			database = DependencyService.Get<ISqLite> ().GetConnection ();
			database.CreateTable<Agent>();
		}

		public IEnumerable<Agent> GetAgents () {
			return (from i in database.Table<Agent>() select i).ToList();
		}

		public Agent GetAgent (int id) 
		{
			return database.Table<Agent>().FirstOrDefault(x => x.Id == id);
		}

		public Agent GetAgentByEmail (string emailId) 
		{
			return database.Table<Agent>().FirstOrDefault(x => x.Email == emailId);
		}

		public int SaveItem (Agent item) 
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
				return database.Delete<Agent>(id);
			}
		}
	}

}


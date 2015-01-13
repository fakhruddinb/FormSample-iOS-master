using System;
using SQLite.Net.Attributes;
using Newtonsoft.Json;

namespace FormSample
{
	public class Contractor
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public string AgentId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string AdditionalInformation { get; set; }
		public DateTime InsertDate { get; set;}
		public DateTime? DeleteDate{ get; set;}
	}
}

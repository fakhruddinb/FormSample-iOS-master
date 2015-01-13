using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FormSample
{
	public class PayTableDataService
	{
		private string payDataUrl = "http://134.213.136.240:1082/api/paydata/";

		public PayTableDataService ()
		{
		}

		public async Task<List<PayTable>> GetPayTableData(string agentId)
		{
			try {
				HttpClient client = new HttpClient ();
				var result = await client.GetAsync (payDataUrl + "?agent=" + agentId);
				var json = await result.Content.ReadAsStringAsync ();
				var response = JsonConvert.DeserializeObject<List<PayTable>> (json);
				return response;
			} catch (Exception ex) {
				throw ex;
			}
		}
	}
}


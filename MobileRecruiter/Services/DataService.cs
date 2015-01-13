using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using System.Text;

namespace FormSample
{
	public class DataService
	{

		private string agentDataUrl = "http://134.213.136.240:1082/api/agents";
		private string passwordUrl = "http://134.213.136.240:1082/api/password";

		public List<Agent> filteredCustomerList { get; set; }

		public DataService()
		{
			filteredCustomerList = new List<Agent>();

		}

		public async Task<List<Agent>> GetAgents()
		{
			HttpClient client = new HttpClient();
			var result = await client.GetAsync(agentDataUrl);
			var json = await result.Content.ReadAsStringAsync();
			var response = JsonConvert.DeserializeObject<List<Agent>>(json);
			return response;
		}

		public async Task<Agent> GetAgent(string agentId)
		{
			HttpClient client = new HttpClient();
			try
			{
				var result = await client.GetAsync(agentDataUrl + "/" + agentId);
				var json = await result.Content.ReadAsStringAsync();
				var response = JsonConvert.DeserializeObject<Agent>(json);
				return response;
			}
			catch
			{
				return null;
			}

		}

		//public async Task<Agent> IsValidUser(string agentEmail, string password)
		public async Task<Agent> IsValidUser(string agentEmail, string password)
		{
			HttpClient client = new HttpClient();
			try
			{
				var result = await client.GetAsync(passwordUrl + "?id=" + agentEmail + "&apikey=" + password);
				var json = await result.Content.ReadAsStringAsync();
				var response = JsonConvert.DeserializeObject<Agent>(json);
				return response;
			}
			catch
			{
				return null;
			}

		}

		public async Task<Agent> AddAgent(Agent cust)
		{
			var requestJson = JsonConvert.SerializeObject(cust, Formatting.None);
			HttpClient client = new HttpClient();
			var result = await client.PostAsync(agentDataUrl+"/", new StringContent(requestJson, Encoding.UTF8, "application/json"));
			var json = await result.Content.ReadAsStringAsync();
			var response = JsonConvert.DeserializeObject<Agent>(json);
			return response;

		}

		public async Task<Agent> UpdateAgent(Agent agent)
		{
			var requestJson = JsonConvert.SerializeObject(agent, Formatting.Indented);

			HttpClient client = new HttpClient();
			var result = await client.PutAsync(agentDataUrl+"/", new StringContent(requestJson, Encoding.UTF8, "application/json"));
			var json = await result.Content.ReadAsStringAsync();
			var response = JsonConvert.DeserializeObject<Agent>(json);
			return response;
		}

		public async Task<Agent> DeleteAgent(string agentId)
		{
			HttpClient client = new HttpClient();
			var result = await client.DeleteAsync(agentDataUrl + "/" + agentId);
			var json = await result.Content.ReadAsStringAsync();
			var response = JsonConvert.DeserializeObject<Agent>(json);
			return response;
		}

		public async Task<Agent> ForgotPassword(string agentEmail)
		{
			HttpClient client = new HttpClient();
			try
			{
				var result = await client.PutAsync(passwordUrl+"/" + agentEmail+"?agent="+agentEmail,null);
				var json = await result.Content.ReadAsStringAsync();
				var response = JsonConvert.DeserializeObject<Agent>(json);
				return response;
			}
			catch
			{
				return null;
			}

		}

	}
}


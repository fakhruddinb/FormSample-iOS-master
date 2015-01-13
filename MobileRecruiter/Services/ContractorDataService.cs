using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using System.Text;

namespace FormSample
{
	public class ContractorDataService
	{
		private string contractorDataUrl = "http://134.213.136.240:1082/api/contractors";
		//private string deleteAllContractorUrl = "http://134.213.136.240:1082/api/contractors/0?agent=";
		public List<Contractor> filteredContractorList { get; set; }

		public ContractorDataService()
		{
			filteredContractorList = new List<Contractor>();
		}

		public async Task<Contractor> AddContractor(Contractor obj)
		{
			var requestJson = JsonConvert.SerializeObject(obj, Formatting.Indented);

			HttpClient client = new HttpClient();
			var result = await client.PostAsync(contractorDataUrl + "/?agent=" + obj.AgentId, new StringContent(requestJson, Encoding.UTF8, "application/json"));
			var json = await result.Content.ReadAsStringAsync();
			var response = JsonConvert.DeserializeObject<Contractor>(json);
			return response;
		}

		public async Task<List<Contractor>> GetContractors(string agentId)
		{
			HttpClient client = new HttpClient();
			var result = await client.GetAsync(contractorDataUrl + "?agent=" + agentId);
			var json = await result.Content.ReadAsStringAsync();
			var response = JsonConvert.DeserializeObject<List<Contractor>>(json);
			return response;
		}

		public async Task<Contractor> GetContractor(int contractorId, string agentId)
		{
			HttpClient client = new HttpClient();
			var result = await client.GetAsync(contractorDataUrl + "/" + contractorId + "?agent=" + agentId);
			var json = await result.Content.ReadAsStringAsync();
			var response = JsonConvert.DeserializeObject<Contractor>(json);
			return response;
		}

		public async Task<Contractor> UpdateContractor(Contractor agent)
		{
			var requestJson = JsonConvert.SerializeObject(agent, Formatting.Indented);

			HttpClient client = new HttpClient();
			var result = await client.PutAsync(contractorDataUrl + "/?agent=" + agent.AgentId, new StringContent(requestJson, Encoding.UTF8, "application/json"));
			var json = await result.Content.ReadAsStringAsync();
			var response = JsonConvert.DeserializeObject<Contractor>(json);
			return response;
		}

		public async Task<Contractor> DeleteContractor(int contractorId, string agentId)
		{
			HttpClient client = new HttpClient();
			var result = await client.DeleteAsync(contractorDataUrl + "/" + contractorId + "?agent=" + agentId);
			var json = await result.Content.ReadAsStringAsync();
			var response = JsonConvert.DeserializeObject<Contractor>(json);
			return response;
		}

		public async Task<string> DeleteAllContractor(string agendId) 
		{
			HttpClient client = new HttpClient ();
			var result = await client.DeleteAsync (contractorDataUrl+"/0?agent=" + agendId);
			//			var json = await result.Content.ReadAsStringAsync ();
			//			var response = JsonConvert.DeserializeObject<Agent> (json);
			return result.ToString();
		}
	}
}

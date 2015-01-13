using System;
using System.Collections.Generic;
using FormSample.Helpers;
using System.Linq;
using System.Threading.Tasks;

namespace FormSample
{
	public sealed class UploadService
	{
		ContractorDatabase contractorDatabase;
		AgentDatabase agentDatabse;
		public UploadService ()
		{
			contractorDatabase = new ContractorDatabase ();
			this.agentDatabse = new AgentDatabase ();
		}

		public async Task UploadContractorData()
		{
			ContractorDataService contractordataservice = new ContractorDataService();

			IEnumerable<Contractor> list = contractorDatabase.GetContractors(Settings.GeneralSettings);
			if(list != null && list.Count() > 0)
			{
				foreach (Contractor item in list) {
					var result = await contractordataservice.AddContractor (item);
					if (result != null) {
						contractorDatabase.DeleteContractor (item.Id);
					}
				}
			}
		}

		public async Task UploadAgentDetail()
		{
			var agentToUpdate = agentDatabse.GetAgentByEmail (Settings.GeneralSettings);
			DataService dataService = new DataService ();
			await dataService.UpdateAgent (agentToUpdate);
		}

		public  async Task UpdatePaytableDataFromService()
		{
			// check whether data is already present in paytable data
			// If not call dataservce to get paytable data and dump into local db.
			FormSample.PayTableDatabase d = new PayTableDatabase ();
			var payTableData = d.GetPayTables ().ToList ();

			if (payTableData.Any ()) {
				d.DeleteAll ();
			}
			//if (!payTableData.Any ()) {
			var service = new PayTableDataService ();
			var result = await service.GetPayTableData (Settings.GeneralSettings);
			var p = result.ToList ();
			if (p != null) {
				AddPayData (p);
			}
			//}
		}

		private void AddPayData(List<PayTable> payDataList)
		{
			FormSample.PayTableDatabase d = new PayTableDatabase ();
			foreach (var payData in payDataList) {
				d.SaveItem (payData);
			}
		}
	}
}


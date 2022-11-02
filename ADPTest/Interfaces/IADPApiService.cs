using ADPTest.Objects;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace ADPTest.Interfaces
{
	public interface IADPApiService
	{
		Task<IncomingTask> GetTaskFromADPService();
		Task<HttpResponseMessage> SubmitTaskToADPService(ResultTask resultTask);
		Task<ContentResult> ExecuteTasks();
	}
}
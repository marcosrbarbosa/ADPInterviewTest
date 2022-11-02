using ADPTest.Interfaces;
using ADPTest.Objects;
using ADPTest.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ADPTest.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ADPTesController : ControllerBase
	{

		private readonly ILogger<ADPTesController> _logger;
		private readonly IADPApiService apiService;
		private readonly ITaskService _taskService;

		public ADPTesController(ILogger<ADPTesController> logger, IADPApiService adpApiService, ITaskService taskService)
		{
			_logger = logger;
			apiService = adpApiService;
			_taskService = taskService;
		}

		[HttpGet("executeTasksFromApi")]
		public async Task<ContentResult> ExecuteTasks()
		{
			try
			{
				return await apiService.ExecuteTasks();
			}
			catch (Exception ex)
			{
				return BadRequestMessage(ex);
			}
		}


		private ContentResult BadRequestMessage(Exception ex)
		{
			return new ContentResult()
			{
				ContentType = "text/plain",
				StatusCode = BadRequest().StatusCode,
				Content = ex.Message
			};
		}


	}
}

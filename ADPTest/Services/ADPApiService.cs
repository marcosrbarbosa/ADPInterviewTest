using ADPTest.Objects;
using ADPTest.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace ADPTest.Services
{
	public class ADPApiService : IADPApiService
	{
		private readonly HttpClient _httpClient;
		private readonly ITaskService _taskService;
		private readonly IConfiguration _configuration;

		public ADPApiService(HttpClient httpClient, ITaskService taskService, IConfiguration configurarion)
		{
			_httpClient = httpClient;
			_taskService = taskService;
			_configuration = configurarion;
		}

		public async Task<IncomingTask> GetTaskFromADPService()
		{
			try
			{
				return await _httpClient.GetFromJsonAsync<IncomingTask>(_configuration.GetValue<string>("GetTaskEndPoint"));
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<ContentResult> SubmitTaskToADPService(ResultTask resultTask)
		{
			try
			{
				var taskResponse = await _httpClient.PostAsJsonAsync(_configuration.GetValue<string>("SubmitTaskEndPoint"), resultTask);
				return TranslateResponseMessage(taskResponse);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<ContentResult> ExecuteTasks()
		{
			try
			{
				var taskToExecute = await GetTaskFromADPService();
				var taskResult = _taskService.ExecuteTask(taskToExecute);
				return await SubmitTaskToADPService(taskResult);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private ContentResult TranslateResponseMessage(HttpResponseMessage receivedMessage)
		{
			ContentResult contentResult = new ContentResult();
			contentResult.StatusCode = (int)receivedMessage.StatusCode;
			contentResult.ContentType = "text/plain";
			switch (receivedMessage.StatusCode)
			{ 
				case HttpStatusCode.OK:
					contentResult.Content = receivedMessage.ReasonPhrase;
					break;
				case HttpStatusCode.BadRequest:
					contentResult.Content = "Incorrect value in result; No ID specified; Value is invalid";
					break;
				case HttpStatusCode.NotFound:
					contentResult.Content = "Value not found for specified ID";
					break;
				case HttpStatusCode.ServiceUnavailable:
					contentResult.Content = "Error communicating with database";
					break;
				default:
					contentResult.Content = receivedMessage.ReasonPhrase;
					break;
			}
			return contentResult;
		}

	}
}

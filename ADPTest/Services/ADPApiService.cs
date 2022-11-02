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
using System.Text;

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

		public async Task<HttpResponseMessage> SubmitTaskToADPService(ResultTask resultTask)
		{
			try
			{
				return await _httpClient.PostAsJsonAsync<ResultTask>(_configuration.GetValue<string>("SubmitTaskEndPoint"), resultTask);
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
				var response = await SubmitTaskToADPService(taskResult);
				return TranslateResponseMessage(response, taskToExecute, taskResult);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private ContentResult TranslateResponseMessage(HttpResponseMessage receivedMessage, IncomingTask taskToExecute, ResultTask taskResult)
		{
			ContentResult contentResult = new ContentResult();
			contentResult.ContentType = "text/plain";

			StringBuilder sb = new StringBuilder();

			if (taskToExecute != null)
			{
				sb.AppendLine("Received task:");
				sb.AppendLine(JsonConvert.SerializeObject(taskToExecute));
				sb.AppendLine();
			}

			if(taskResult != null)
			{
				sb.AppendLine("Task Result:");
				sb.AppendLine(JsonConvert.SerializeObject(taskResult));
				sb.AppendLine();
			}

			sb.AppendLine("Return message:");

			switch (receivedMessage.StatusCode)
			{ 
				case HttpStatusCode.OK:
					sb.AppendLine($"{receivedMessage.StatusCode} - Success");
					break;
				case HttpStatusCode.BadRequest:
					sb.AppendLine($"{receivedMessage.StatusCode} - Incorrect value in result; No ID specified; Value is invalid");
					break;
				case HttpStatusCode.NotFound:
					sb.AppendLine($"{receivedMessage.StatusCode} - Value not found for specified ID");
					break;
				case HttpStatusCode.ServiceUnavailable:
					sb.AppendLine($"{receivedMessage.StatusCode} - Error communicating with database");
					break;
				default:
					sb.AppendLine($"{receivedMessage.StatusCode} - {receivedMessage.ReasonPhrase}");
					break;
			}

			contentResult.Content = sb.ToString();

			return contentResult;
		}

	}
}

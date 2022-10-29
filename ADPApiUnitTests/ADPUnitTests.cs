using ADPTest.Interfaces;
using ADPTest.Objects;
using ADPTest.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ADPApiUnitTests
{
	public class Tests
	{
		public IConfiguration configuration { get; set; }

		public ServiceCollection serviceCollection;
		[SetUp]
		public void Setup()
		{
			configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
			serviceCollection = new ServiceCollection();
			serviceCollection.AddHttpClient<IADPApiService, ADPApiService>(
				configureClient =>
				{
					configureClient.BaseAddress = configuration.GetValue<Uri>("ApiBaseAddress");
				});
			serviceCollection.AddScoped<ITaskService, TaskService>();
			serviceCollection.AddSingleton<IConfiguration>(configuration);
		}

		[Test]
		public void GetTaskFromApiTest()
		{
			try
			{
				using (var serviceProvider = serviceCollection.BuildServiceProvider())
				{
					var apiService = serviceProvider.GetService<IADPApiService>();
					Task<IncomingTask> response = apiService.GetTaskFromADPService();

					response.Wait();

					if (response.Result == null)
					{
						Assert.Fail("Api service does not returned a task");
					}
					else
					{
						Assert.IsTrue(response.Result is IncomingTask);
					}
				}
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

		[Test]
		public void GetResultFromTaskExecutionTest()
		{
			try
			{
				using (var serviceProvider = serviceCollection.BuildServiceProvider())
				{
					var apiService = serviceProvider.GetService<IADPApiService>();
					var taskService = serviceProvider.GetService<ITaskService>();

					Task<IncomingTask> response = apiService.GetTaskFromADPService();
					response.Wait();

					if (response.Result == null)
					{
						Assert.Fail("Api service does not returned a task");
					}

					IncomingTask incomingTask = response.Result;

					ResultTask resultTask = taskService.ExecuteTask(incomingTask);
					
					Assert.IsTrue(resultTask is ResultTask);

				}

			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

		[Test]
		public void SubmitTaskResultToApiTest()
		{
			try
			{
				using (var serviceProvider = serviceCollection.BuildServiceProvider())
				{
					var apiService = serviceProvider.GetService<IADPApiService>();
					var taskService = serviceProvider.GetService<ITaskService>();

					Task<IncomingTask> getMethodResponse = apiService.GetTaskFromADPService();
					getMethodResponse.Wait();

					if (getMethodResponse.Result == null)
					{
						Assert.Fail("Api service does not returned a task");
					}

					IncomingTask incomingTask = getMethodResponse.Result;

					ResultTask resultTask = taskService.ExecuteTask(incomingTask);

					if (resultTask == null)
					{
						Assert.Fail("Task service does not returned a Result for the task");
					}

					Task<ContentResult> postMethodResponse = apiService.SubmitTaskToADPService(resultTask);

					Assert.IsTrue(postMethodResponse.Result != null);

				}

			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

		[Test]
		public void ExecuteTasksTest()
		{
			try
			{
				using (var serviceProvider = serviceCollection.BuildServiceProvider())
				{
					var apiService = serviceProvider.GetService<IADPApiService>();

					Task<ContentResult> testResponse = apiService.ExecuteTasks();

					Assert.IsTrue(testResponse.Result != null);

				}

			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}


	}


}









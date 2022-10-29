using ADPTest.Interfaces;
using ADPTest.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Numerics;

namespace ADPTest.Services
{
	public class TaskService : ITaskService
	{
		public TaskService(){ }

		private bool disposedValue;

		public ResultTask ExecuteTask(IncomingTask incomingTask)
		{
			switch (incomingTask.Operation)
			{
				case "addition":
					return Add(incomingTask);
				case "subtraction":
					return Substract(incomingTask);
				case "multiplication":
					return Multiply(incomingTask);
				case "division":
					return Divide(incomingTask);
				case "remainder":
					return Remainder(incomingTask);
				default:
					throw new Exception($"Operation {incomingTask.Operation} for task Id {incomingTask.Id} not expected.");
			}
		}

		private ResultTask Add(IncomingTask incomingTask)
		{
			var resultTask = new ResultTask(incomingTask.Id);
			resultTask.Result = incomingTask.Left + incomingTask.Right;
			return resultTask;
		}

		private ResultTask Substract(IncomingTask incomingTask)
		{
			var resultTask = new ResultTask(incomingTask.Id);
			resultTask.Result = incomingTask.Left - incomingTask.Right;
			return resultTask;
		}

		private ResultTask Multiply(IncomingTask incomingTask)
		{
			var resultTask = new ResultTask(incomingTask.Id);
			resultTask.Result = Convert.ToInt64(incomingTask.Left * incomingTask.Right);
			return resultTask;
		}

		private ResultTask Divide(IncomingTask incomingTask)
		{
			if (incomingTask.Left == 0)
			{
				throw new Exception($"Left argument for task Id {incomingTask.Id} cannot be 0");
			}
			var resultTask = new ResultTask(incomingTask.Id);
			resultTask.Result = incomingTask.Left / incomingTask.Right;
			return resultTask;
		}

		private ResultTask Remainder(IncomingTask incomingTask)
		{
			var resultTask = new ResultTask(incomingTask.Id);
			resultTask.Result = incomingTask.Left % incomingTask.Right;
			return resultTask;
		}


	}
}

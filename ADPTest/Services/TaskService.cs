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
		public TaskService() { }

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
			return new ResultTask(incomingTask.Id, incomingTask.Left + incomingTask.Right);
		}

		private ResultTask Substract(IncomingTask incomingTask)
		{
			return new ResultTask(incomingTask.Id, incomingTask.Left - incomingTask.Right);
		}

		private ResultTask Multiply(IncomingTask incomingTask)
		{
			return new ResultTask(incomingTask.Id, incomingTask.Left * incomingTask.Right);
		}

		private ResultTask Divide(IncomingTask incomingTask)
		{
			if (incomingTask.Left == 0)
			{
				throw new Exception($"Left value cannot be 0.");
			}

			return new ResultTask(incomingTask.Id, incomingTask.Left / incomingTask.Right);

		}

		private ResultTask Remainder(IncomingTask incomingTask)
		{
			return new ResultTask(incomingTask.Id, incomingTask.Left % incomingTask.Right);
		}

	}
}

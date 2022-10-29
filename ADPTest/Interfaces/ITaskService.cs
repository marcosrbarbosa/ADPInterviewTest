using ADPTest.Objects;
using System;

namespace ADPTest.Interfaces
{
	public interface ITaskService
	{
		ResultTask ExecuteTask(IncomingTask incomingTask);
	}
}
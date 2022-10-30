using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace ADPTest.Objects
{

	public class ResultTask : ADPTask
	{

		public ResultTask()
		{

		}
		public ResultTask(string taskId)
		{
			this.Id = taskId;
		}

		public ResultTask(string taskId, long taskResult)
		{
			this.Id = taskId;
			this.Result = taskResult;
		}
		public long Result { get; set; }
	}
}

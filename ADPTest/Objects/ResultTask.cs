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

		public ResultTask(string taskId, double value)
		{
			this.Id = taskId;
			this.Result = value;
		}
		public double Result { get; set; }
	}
}

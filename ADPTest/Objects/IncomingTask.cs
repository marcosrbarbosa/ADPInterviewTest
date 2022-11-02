using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADPTest.Objects
{
	public class IncomingTask : ADPTask
	{
		public string Operation { get; set; }
		public double Left { get; set; }
		public double Right { get; set; }

	}

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADPTest.Objects
{
	public class IncomingTask : ADPTask
	{
		public string Operation { get; set; }
		public long Left { get; set; }
		public long Right { get; set; }

	}

}

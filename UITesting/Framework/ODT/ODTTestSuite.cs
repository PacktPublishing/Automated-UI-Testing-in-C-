using System;
namespace UITesting.Framework.ODT
{
	public class ODTTestSuite : ODTContainerRunner<ODTTestCase>
	{
		public ODTTestSuite()
		{
			this.Steps = new ODTTestCase[] { };
		}
	}
}

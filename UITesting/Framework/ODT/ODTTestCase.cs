using System;
namespace UITesting.Framework.ODT
{
	public class ODTTestCase : ODTContainerRunner<ODTTestStep>
	{
		public ODTTestCase()
		{
			this.Steps = new ODTTestStep[] { };
		}
	}
}

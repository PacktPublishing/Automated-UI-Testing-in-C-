using System;
namespace UITesting.Framework.ODT
{
	public abstract class ODTTestStep : ODTRunner
	{
		public ODTTestStep() : base()
		{
		}
		public abstract void StepBody();
		public override void Run()
		{
			try
			{
				this.BeforeRun();
				this.StepBody();
			}
			catch (Exception e)
			{
				this.PassedState = false;
				this.OnError(e);
			}
			try
			{
				this.AfterRun();
			}
			catch (Exception e)
			{
				this.PassedState = false;
				this.OnError(e);
			}
		}
	}
}

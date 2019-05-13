using System;
namespace UITesting.Framework.ODT
{
	public class ODTContainerRunner<T> : ODTRunner where T : ODTRunner
	{
		protected ODTTestStep[] BeforeSteps
		{
			get; set;
		}
		protected ODTTestStep[] AfterSteps
		{
			get; set;
		}
		protected T[] Steps
		{
			get; set;
		}
		public ODTContainerRunner() : base()
		{
		}
		public new void BeforeRun()
		{
			foreach (ODTTestStep step in this.BeforeSteps)
			{
				step.Run();
			}
		}
		public new void AfterRun()
		{
			foreach (ODTTestStep step in this.AfterSteps)
			{
				step.Run();
			}
		}
		public override void Run()
		{ 
			try
			{
				BeforeRun();
				foreach (T step in this.Steps)
				{
					step.Run();
				}
			}
			catch (Exception e)
			{
				this.PassedState = false;
				this.OnError(e);
			}
			AfterRun();
		}
	}
}

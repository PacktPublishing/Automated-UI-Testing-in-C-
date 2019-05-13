using System;
namespace UITesting.Framework.ODT
{
	public abstract class ODTRunner
	{
		protected bool PassedState
		{
			get; set;
		}

		protected ODTRunner()
		{
			PassedState = true;
		}
		public abstract void Run();
		public void BeforeRun()
		{
		}
		public void AfterRun()
		{
		}
		public virtual void OnError(Exception e)
		{
		}
	}
}

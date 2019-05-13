using NUnit.Framework;
using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using UITesting.Pages.Banking;
using UITesting.Framework.Core;
using UITesting.Framework.ODT;
using UITesting.Framework.UI;
using UITesting.Framework.UI.Controls;
using UITesting.ODTSteps;

namespace UITesting
{
	[TestFixture()]
	public class ODTTestBanking : ODTTestCase
	{
		public override void OnError(Exception e)
		{
			Assert.Fail(e.Message + e.StackTrace);
		}
		public ODTTestBanking()
		{ 
			this.BeforeSteps = new ODTTestStep[]
			{
				new SetupStep()
			};
			this.AfterSteps = new ODTTestStep[]
			{
				new AfterStep()
			};
			this.Steps = new ODTTestStep[]
			{
				new GoToBankManagerStep(),
				new GoToBankManagerTabPageStep<CustomersPage>(),
				new VerifyCustomerListNotEmpty(),
				new GetCustomersCount(),
				new GoToBankManagerTabPageStep<AddCustomerPage>(),
				new AddNewCustomer("Test", "User", "WW99"),
				new GoToBankManagerTabPageStep<CustomersPage>(),
				new VerifyCustomerCountChangedBy(1),
				new VerifyLastCustomerData("Test", "User", "WW99"),
				new GetCustomersCount(),
				new DeleteLastCustomer(),
				new VerifyCustomerCountChangedBy(-1)
			};

		}
		[Test()]
		public void ODTTestAddNewCustomer()
		{
			this.Run();
		}
	}
}

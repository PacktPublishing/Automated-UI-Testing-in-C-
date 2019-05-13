using System;
using NUnit.Framework;
using OpenQA.Selenium.Remote;
using UITesting.Framework.Core;
using UITesting.Framework.ODT;
using UITesting.Framework.UI;
using UITesting.Pages.Banking;
namespace UITesting.ODTSteps
{
	public abstract class ODTNUnitStep : ODTTestStep
	{
		protected Exception ex { get; set; }
		public override void OnError(Exception e)
		{
			ex = e;
		}
	}
	public class SetupStep : ODTNUnitStep
	{
		public override void StepBody()
		{
			DesiredCapabilities capabilities = new DesiredCapabilities();
			Driver.Add(Configuration.Platform, Configuration.DriverPath, capabilities);
			HomePage home = PageFactory.Init<HomePage>();
			home.Navigate();

		}
	}
	public class AfterStep : ODTNUnitStep
	{

		public override void StepBody()
		{
			Driver.Quit();
			if (!this.PassedState)
			{ 
				Assert.Fail(ex.Message + ex.StackTrace);
			}
		}
	}
	public class GoToBankManagerStep : ODTNUnitStep
	{



		public override void StepBody()
		{
			HomePage home = PageFactory.Init<HomePage>();
			home.buttonBankManagerLogin.ClickAndWaitFor<BankManagerCommonPage>();
		}

	}
	public class GoToBankManagerTabPageStep<T> : ODTNUnitStep where T : BankManagerCommonPage
	{

		public override void StepBody()
		{
			T page = ((T)PageFactory.Init<T>());
			typeof(T).GetMethod("Navigate", new Type[] { }).Invoke(page, new Object[] { });
		}
	}
	public class VerifyCustomerListNotEmpty : ODTNUnitStep
	{
		public override void StepBody()
		{
			CustomersPage customers = PageFactory.Init<CustomersPage>();
			Assert.True(customers.tableCustomers.IsNotEmpty());
		}
	}
	public class GetCustomersCount : ODTNUnitStep
	{
		public override void StepBody()
		{
			GoToBankManagerTabPageStep<CustomersPage> navigateStep = new GoToBankManagerTabPageStep<CustomersPage>();
			navigateStep.Run();
			CustomersPage customers = PageFactory.Init<CustomersPage>();
			Context.Put("Customers Count", customers.tableCustomers.ItemsCount);
		}
	}
	public class AddNewCustomer : ODTNUnitStep
	{

		private String firstName;
		private String lastName;
		private String postCode;

		public AddNewCustomer(String firstName, String lastName, String postCode)
		{
			this.firstName = firstName;
			this.lastName = lastName;
			this.postCode = postCode;
		}

		public override void StepBody()
		{
			System.Threading.Thread.Sleep(1000);
			AddCustomerPage addCustomer = PageFactory.Init<AddCustomerPage>();
			addCustomer.editFirstName.Text = firstName;
			addCustomer.editLastName.Text = lastName;
			addCustomer.editPostCode.Text = postCode;
			addCustomer.buttonSubmit.Click();
			addCustomer.Driver.SwitchTo().Alert().Accept();
		}

	}
	public class VerifyLastCustomerData : ODTNUnitStep
	{

		private String firstName;
		private String lastName;
		private String postCode;

		public VerifyLastCustomerData(String firstName, String lastName, String postCode)
		{
			this.firstName = firstName;
			this.lastName = lastName;
			this.postCode = postCode;
		}

		public override void StepBody()
		{
			CustomersPage customers = PageFactory.Init<CustomersPage>();
			int rows = customers.tableCustomers.ItemsCount;
			Assert.AreEqual(firstName, customers.tableCustomers["First Name", rows - 1].Text);
			Assert.AreEqual(lastName, customers.tableCustomers["Last Name", rows - 1].Text);
			Assert.AreEqual(postCode, customers.tableCustomers["Post Code", rows - 1].Text);
		}

	}
	public class VerifyCustomerCountChangedBy : ODTNUnitStep
	{

		private int shift;
		public VerifyCustomerCountChangedBy(int shift)
		{
			this.shift = shift;
		}


		public override void StepBody()
		{
			CustomersPage customers = PageFactory.Init<CustomersPage>();
			Assert.AreEqual((int)Context.Get("Customers Count") + this.shift,
					customers.tableCustomers.ItemsCount);
		}
	}
	public class DeleteLastCustomer : ODTNUnitStep
	{



		public override void StepBody()
		{
			CustomersPage customers = PageFactory.Init<CustomersPage>();
			customers.tableCustomers["Delete Customer",
					customers.tableCustomers.ItemsCount - 1].Click();
		}

	}
}

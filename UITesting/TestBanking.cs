using NUnit.Framework;
using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using UITesting.Pages.Banking;
using UITesting.Framework.Core;
using UITesting.Framework.UI;
using UITesting.Framework.UI.Controls;

namespace UITesting
{
	[TestFixture()]
	public class TestBanking
	{
		protected HomePage home;
		protected BankManagerCommonPage bankManagerMenu;
		protected AddCustomerPage addCustomer;
		protected CustomersPage customers;

		[SetUp]
		public void SetUp()
		{
			DesiredCapabilities capabilities = new DesiredCapabilities();
			Driver.Add(Configuration.Platform, Configuration.DriverPath, capabilities);
			home = PageFactory.Init<HomePage>();
			home.Navigate();
		}
		[TearDown]
		public void TearDown()
		{
			Driver.Quit();
		}
		[Test()]
		public void TestAddNewCustomer()
		{
			bankManagerMenu = home.buttonBankManagerLogin.ClickAndWaitFor<BankManagerCommonPage>();
			customers = bankManagerMenu.buttonCustomers.ClickAndWaitFor<CustomersPage>();
			Assert.True(customers.tableCustomers.IsNotEmpty());
			int rows = customers.tableCustomers.ItemsCount;
			addCustomer = customers.buttonAddCustomer.ClickAndWaitFor<AddCustomerPage>();

			Assert.True(addCustomer.AllElementsExist(
				new Control[] 
				{
					addCustomer.editFirstName,
					addCustomer.editLastName,
					addCustomer.editPostCode,
					addCustomer.buttonSubmit
				})
            );
			Assert.True(addCustomer.AnyOfElementsExist(new Control[] {
				addCustomer.editFirstName,
				addCustomer.editLastName,
				addCustomer.editPostCode,
				addCustomer.buttonSubmit
			}));
			System.Threading.Thread.Sleep(1000);
			addCustomer.editFirstName.Text = "Test";
			addCustomer.editLastName.Text = "User";
			addCustomer.editPostCode.Text = "WWW99";
			addCustomer.buttonSubmit.Click();
			addCustomer.Driver.SwitchTo().Alert().Accept();
			customers = addCustomer.buttonCustomers.ClickAndWaitFor<CustomersPage>();

			Assert.AreEqual(rows + 1, customers.tableCustomers.ItemsCount);
			Assert.AreEqual("Test", customers.tableCustomers["First Name", rows].Text);
			Assert.AreEqual("User", customers.tableCustomers["Last Name", rows].Text);
			Assert.AreEqual("WWW99", customers.tableCustomers["Post Code", rows].Text);

			customers.tableCustomers["Delete Customer", rows].Click();
			Assert.True(customers.tableCustomers.IsNotEmpty());
			Assert.AreEqual(rows, customers.tableCustomers.ItemsCount);
		}
	}
}

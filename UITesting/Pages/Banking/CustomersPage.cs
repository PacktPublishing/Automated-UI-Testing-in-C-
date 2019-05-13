using System;
using OpenQA.Selenium;
using UITesting.Framework.UI;
using UITesting.Framework.UI.Controls;

namespace UITesting.Pages.Banking
{
	[Alias("Customers")]
	public class CustomersPage : BankManagerCommonPage
	{
		[Alias("Customer List")]
		[FindBy("//table", ItemLocator = "/tbody/tr")]
		[SubItem("First Name", "/td[1]")]
		[SubItem("Last Name", "/td[2]")]
		[SubItem("Post Code", "/td[3]")]
		[SubItem("Account Number", "/td[4]")]
		[SubItem("Delete Customer", "/td[5]/button")]
		public TableView tableCustomers;

		public CustomersPage(IWebDriver driver) : base(driver)
		{
		}
		public override Page Navigate()
		{
			return this.buttonCustomers.ClickAndWaitFor<CustomersPage>();
		}
	}
}

using System;
using OpenQA.Selenium;
using UITesting.Framework.UI;
using UITesting.Framework.UI.Controls;
namespace UITesting.Pages.Banking
{
	[Alias("Banking Manager Home")]
	public class BankManagerCommonPage : Page
	{
		[Alias("Add Customer")]
	    [FindBy("//button[contains(text(),'Add Customer')]")]
		public Control buttonAddCustomer;

		[Alias("Open Account")]
		[FindBy("//button[contains(text(),'Open Account')]")]
		public Control buttonOpenAccount;

		[Alias("Customers")]
		[FindBy("//button[contains(text(),'Customers')]")]
		public Control buttonCustomers;

		public BankManagerCommonPage(IWebDriver driver) : base(driver)
		{
		}
	}
}

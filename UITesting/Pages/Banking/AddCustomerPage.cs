using System;
using OpenQA.Selenium;
using UITesting.Framework.UI;
using UITesting.Framework.UI.Controls;
namespace UITesting.Pages.Banking
{
	[Alias("Add Customer")]
	public class AddCustomerPage : BankManagerCommonPage
	{
		[Alias("First Name")]
	    [FindBy("//input[@type='text']")]
		public Edit editFirstName;

		[Alias("Last Name")]
		[FindBy("xpath=(//input[@type='text'])[2]")]
		public Edit editLastName;

		[Alias("Post Code")]
		[FindBy("xpath=(//input[@type='text'])[3]")]
		public Edit editPostCode;

		[Alias("Submit")]
		[FindBy("//button[text() = 'Add Customer']")]
		public Control buttonSubmit;

		public AddCustomerPage(IWebDriver driver) : base(driver)
		{
		}
		public override Page Navigate()
		{
			return this.buttonAddCustomer.ClickAndWaitFor<AddCustomerPage>();
		}
	}
}

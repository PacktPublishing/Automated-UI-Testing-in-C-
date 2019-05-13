using System;
using OpenQA.Selenium;
using UITesting.Framework.UI;
using UITesting.Framework.UI.Controls;
namespace UITesting.Pages.Banking
{
	[Alias("Deposit")]
	public class CustomerDepositPage : CustomerCommonPage
	{
		public CustomerDepositPage(IWebDriver driver) : base(driver)
		{
		}
	    [Alias("Deposit Amount")]
		[FindBy("//input[@type='number']")]
		public Edit editDepositAmount;

		[Alias("Submit Deposit")]
		[FindBy("//button[text() = 'Deposit' and @type = 'submit']")]
		public Control buttonSubmitDeposit;

		public override Page Navigate()
		{
	        this.buttonDeposit.Click();
	        return this;
		}
	}
}

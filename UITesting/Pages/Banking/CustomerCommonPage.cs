using System;
using OpenQA.Selenium;
using UITesting.Framework.UI;
using UITesting.Framework.UI.Controls;
namespace UITesting.Pages.Banking
{
	[Alias("Customer Home")]
	public class CustomerCommonPage : Page
	{
		public CustomerCommonPage(IWebDriver driver)  :base(driver)
		{
		}
		[Alias("Balance")]
		[FindBy("xpath=//div[@class='center']/strong[2]")]
		public Control labelBalance;


		[Alias("Account Select")]
		[FindBy("accountSelect")]
		public SelectList selectAccount;


		[Alias("Deposit")]
		[FindBy("//button[contains(text(), 'Deposit')]")]
		public Control buttonDeposit;


		[Alias("Withdraw")]
		[FindBy("//button[contains(text(), 'Withdraw')]")]
		public Control buttonWithdraw;
	}
}

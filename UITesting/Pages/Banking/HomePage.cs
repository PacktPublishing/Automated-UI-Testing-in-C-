using System;
using OpenQA.Selenium;
using UITesting.Framework.Core;
using UITesting.Framework.UI;
using UITesting.Framework.UI.Controls;
namespace UITesting.Pages.Banking
{
	[Alias("Banking Home")]
	public class HomePage : Page
	{

		[Alias("Customer Login")]
		[FindBy("//button[text() = 'Customer Login']")]
		public Control buttonCustomerLogin;

		[Alias("Banking Manager Login")]
		[FindBy("//button[text() = 'Bank Manager Login']")]
		public Control buttonBankManagerLogin;
	
		public HomePage(IWebDriver driver) : base(driver)
		{
		}
		public new Page Navigate()
		{ 
			this.Driver.Navigate()
			    .GoToUrl("http://www.globalsqa.com/angularJs-protractor/BankingProject/#/login");
			return this;
		}
		    
    	public CustomerCommonPage LoginAsCustomer(String name)
		{
			CustomerLoginPage loginPage = this.buttonCustomerLogin.ClickAndWaitFor<CustomerLoginPage>();
        	loginPage.selectUser.Text = name;
        	return loginPage.buttonLogin.ClickAndWaitFor<CustomerCommonPage>();
    	}
	}
}

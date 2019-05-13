using System;
using OpenQA.Selenium;
using UITesting.Framework.UI;
using UITesting.Framework.UI.Controls;
namespace UITesting.Pages.Banking
{
	[Alias("Customer Login")]
	public class CustomerLoginPage : Page
	{
		public CustomerLoginPage(IWebDriver driver) : base(driver)
		{
		}
	    [Alias("Select User")]
		[FindBy("userSelect")]
		public SelectList selectUser;

		[Alias("Login")]
		[FindBy("//button[text() = 'Login']", ExcludeFromSearch = true)]
		public Control buttonLogin;
	}
}

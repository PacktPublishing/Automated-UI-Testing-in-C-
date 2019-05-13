using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using UITesting.Framework.Core;
using NUnit.Framework;

         
namespace UITesting.Framework.UI.Controls
{
	public class Control
	{
		private Page page;
		private By locator;

		public Page Page
		{ 
			get 
			{ 
				return page; 
			} 
		}
		public IWebDriver Driver
		{
			get
			{
				return page.Driver;
			}
		}
		public By Locator
		{ 
			get
			{
				return locator;
			}
		}
		public String ItemLocator
		{
			get; set;
		}
		public SubItem[] SubItems
		{
			get; set;
		}
		public IWebElement Element
		{ 
			get
			{
				return this.Driver.FindElement(locator);
			}
		}
		public virtual String Text
		{ 
			get
			{ 
				Assert.True(this.Exists(), "Unable to find element: " + this.Locator);
				return this.Element.Text;
			}
			set
			{ 
			}
		}
		public bool ExcludeFromSearch 
		{ 
			get; set;
		}
		public Control(Page pageValue, By locatorValue)
		{
			this.page = pageValue;
			this.locator = locatorValue;
			this.ExcludeFromSearch = false;
		}
		public bool WaitUntil<T>(Func<IWebDriver, T> condition, int timeout)
		{
			try
			{
				new WebDriverWait(this.Driver, TimeSpan.FromSeconds(timeout))
					.Until(condition);
			}
			catch (WebDriverTimeoutException)
			{
				return false;
			}
			return true;
		}
		public bool Exists(int timeout)
		{
			return WaitUntil(ExpectedConditions.ElementExists(this.Locator), timeout);
		}
		public bool Exists()
		{ 
			return Exists(Configuration.Timeout);
		}
		public bool Clickable(int timeout)
		{
			return WaitUntil(ExpectedConditions.ElementToBeClickable(this.Locator), timeout);
		}
		public bool Clickable()
		{
			return Clickable(Configuration.Timeout);
		}

		public bool Visible(int timeout)
		{
			return WaitUntil(ExpectedConditions.ElementIsVisible(this.Locator), timeout);
		}
		public bool Visible()
		{
			return Visible(Configuration.Timeout);
		}
		public bool Invisible(int timeout)
		{
			return WaitUntil(ExpectedConditions.InvisibilityOfElementLocated(this.Locator), timeout);
		}
		public bool Invisible()
		{
			return Invisible(Configuration.Timeout);
		}
		public bool Enabled(int timeout)
		{
			return WaitUntil<bool>(c =>
			{
				return this.Element.Enabled;
			}, timeout);
		}
		public bool Enabled()
		{
			return Enabled(Configuration.Timeout);
		}
		public bool Disabled(int timeout)
		{
			return WaitUntil<bool>(c =>
			{
				return !this.Element.Enabled;
			}, timeout);
		}
		public bool Disabled()
		{
			return Disabled(Configuration.Timeout);
		}
		public void Click()
		{
			Assert.True(this.Exists(), "Unable to find element: " + this.Locator);
			Assert.True(this.Visible(), "Unable to wait for visibility of element: " + this.Locator);
			this.Element.Click();
		}
		public T ClickAndWaitFor<T>() where T : Page
		{
			this.Click();
			T pageValue = PageFactory.Init<T>();
			Assert.True(pageValue.IsCurrent(),
			            String.Format("The page '{0}' didn't appear during specified timeout", page.GetType().FullName));
			return pageValue;
		}
	}
}

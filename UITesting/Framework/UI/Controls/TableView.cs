using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using UITesting.Framework.Core;

namespace UITesting.Framework.UI.Controls
{
	public class TableView : Control
	{
		protected String FullItemLocator
		{
			get
			{
				return String.Format("{0}{1}", this.Locator.ToString().Substring("(By.XPath:".Length), this.ItemLocator);
			}
		}
		public int ItemsCount
		{
			get
			{
				return Driver.FindElements(By.XPath(FullItemLocator)).Count;
			}
		}
		public TableView(Page page, By locator) : base(page, locator)
		{
		}
		public Control this[int index]
		{
			get
			{ 
				String locator = String.Format("({0})[{1}]", this.FullItemLocator, index + 1);
				return new Control(this.Page, By.XPath(locator));
			}
		}
		public Control this[String name, int index]
		{
			get
			{
				SubItem item = this.SubItems.First(subItem => subItem.Name == name);
				String locator = String.Format("({0})[{1}]{2}", this.FullItemLocator, index + 1, item.Locator);
				return new Control(this.Page, By.XPath(locator));
			}
		}
		public bool IsNotEmpty(int timeout)
		{
			return this[0].Exists(timeout);
		}
		public bool IsNotEmpty()
		{
			return this.IsNotEmpty(Configuration.Timeout);
		}
	}
}

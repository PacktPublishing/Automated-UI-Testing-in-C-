using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using UITesting.Framework.Core;
using UITesting.Framework.UI.Controls;

namespace UITesting.Framework.UI
{
	public class PageFactory
	{
		public PageFactory()
		{
		}

		private static By toLocator(String input)
		{
			if (Regex.IsMatch(input, "^(xpath=|/)(.*)"))
			{
				return By.XPath(new Regex("^xpath=").Replace(input, ""));
			}
			else if (Regex.IsMatch(input, "^id=(.*)"))
			{
				return By.Id(input.Substring("id=".Length));
			}
			else if (Regex.IsMatch(input, "^name=(.*)"))
			{
				return By.Name(input.Substring("name=".Length));
			}
			else if (Regex.IsMatch(input, "^css=(.*)"))
			{
				return By.CssSelector(input.Substring("css=".Length));
			}
			else if (Regex.IsMatch(input, "^class=(.*)"))
			{
				return By.ClassName(input.Substring("class=".Length));
			}
			else
			{
				return By.Id(input);
			}
		}
		private static FindBy getLocatorForPlatform(FindBy[] locators, TargetPlatform platform)
		{
			foreach (FindBy locator in locators)
			{
				if (locator.Platform.Equals(platform))
				{
					return locator;
				}
			}
			return null;
		}
		public static T Init<T>()
		{
			IWebDriver driver = Driver.Current();
			T page = (T) typeof(T).GetConstructor(new Type[] { typeof(IWebDriver) })
								  .Invoke(new Object[] { driver });
			foreach (FieldInfo field in typeof(T).GetFields())
			{
				FindBy[] locators = (FindBy[]) field.GetCustomAttributes(typeof(FindBy));
				if (locators != null && locators.Length > 0)
				{
					FindBy locator = getLocatorForPlatform(locators, Configuration.Platform);
					if (locator == null)
					{ 
						locator = getLocatorForPlatform(locators, TargetPlatform.ANY_WEB);
					}
					if (locator == null)
					{
						locator = getLocatorForPlatform(locators, TargetPlatform.ANY);
					}
					if (locator != null)
					{ 
						Control control = (Control)field.FieldType
										 .GetConstructor(new Type[] { typeof(Page), typeof(By) })
										 .Invoke(new Object[] { page, toLocator(locator.Locator) });
						control.ItemLocator = locator.ItemLocator;
						SubItem[] items = (SubItem[])field.GetCustomAttributes(typeof(SubItem));
						if (items != null && items.Length > 0)
						{
							control.SubItems = items.Where<SubItem>(t =>
								   (
									  t.Platform.Equals(Configuration.Platform))
								   || t.Platform.Equals(TargetPlatform.ANY)
								   || t.Platform.Equals(TargetPlatform.ANY_WEB) && Configuration.Platform.IsWeb())
							.ToArray<SubItem>();
						}
						control.ExcludeFromSearch = locator.ExcludeFromSearch;
						field.SetValue(page, control);
					}
				}
			}
			return page;
		}
	}
}

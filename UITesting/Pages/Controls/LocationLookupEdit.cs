using System;
using OpenQA.Selenium;
using UITesting.Framework.Core;
using UITesting.Framework.UI;
using UITesting.Framework.UI.Controls;

namespace UITesting.Pages.Controls
{
	public class LocationLookupEdit : Edit
	{
		public new String Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				this.Click();
				if (Configuration.Platform.IsWeb())
				{
					this.Element.Clear();
					this.Element.SendKeys(value);
					Control lookupItem = new Control(this.Page,
													 By.XPath("(//li[contains(@class, 'autocomplete__item')])[1]"));
					lookupItem.Click();
				}
				else
				{
					LocationSearchPage search = PageFactory.Init<LocationSearchPage>();
					search.editSearch.Text = value;
					search.itemTopMostResult.Click();
				}
			}
		}
		public LocationLookupEdit(Page page, By locatorValue) : base(page, locatorValue)
		{
		}
	}
}

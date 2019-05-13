using System;
using OpenQA.Selenium;
using UITesting.Framework.UI;
using UITesting.Framework.UI.Controls;

namespace UITesting.Pages
{
	public class SearchResultsPage : Page
	{
		[FindBy("ss", Platform = TargetPlatform.ANY_WEB)]
		public Edit editDestination;

		public SearchResultsPage(IWebDriver driver) : base(driver)
		{
		}
	}
}

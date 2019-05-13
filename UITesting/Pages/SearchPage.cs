using System;
using OpenQA.Selenium;
using UITesting.Framework.Core;
using UITesting.Framework.UI;
using UITesting.Framework.UI.Controls;
using UITesting.Pages.Controls;

namespace UITesting.Pages
{
	public class SearchPage : Page
	{
		[FindBy("search_searchInput", Platform = TargetPlatform.ANDROID_NATIVE)]
		[FindBy("ss", Platform = TargetPlatform.ANY_WEB)]
        public LocationLookupEdit editDestination;

		[FindBy("", Platform = TargetPlatform.ANDROID_NATIVE)]
		[FindBy("xpath=(//li[contains(@class, 'autocomplete__item')])[1]", Platform = TargetPlatform.ANY_WEB)]
		public Control autoCompleteItem;

		[FindBy("xpath=(//android.widget.TextView[contains(@resource-id, 'calendar_tv') and @enabled='true'])[1]", Platform = TargetPlatform.ANDROID_NATIVE)]
		[FindBy("xpath=//table[@class='c2-month-table']//td[contains(@class, 'c2-day-s-today')]", Platform = TargetPlatform.ANY_WEB)]
		public Control checkoutDayToday;

		[FindBy("business_purpose_leisure", Platform = TargetPlatform.ANDROID_NATIVE)]
		[FindBy("xpath=(//input[@name='sb_travel_purpose'])[2]", Platform = TargetPlatform.ANY_WEB)]
		public Control radioLeisure;

		[FindBy("business_purpose_business", Platform = TargetPlatform.ANDROID_NATIVE)]
		[FindBy("xpath=(//input[@name='sb_travel_purpose'])[1]", Platform = TargetPlatform.ANY_WEB)]
		public Control radioBusiness;

		[FindBy("adult_count", Platform = TargetPlatform.ANDROID_NATIVE)]
		[FindBy("group_adults", Platform = TargetPlatform.ANY_WEB)]
		public SelectList selectAdultsNumber;

		[FindBy("search_search", Platform = TargetPlatform.ANDROID_NATIVE)]
		[FindBy("xpath=//button[@type='submit']", Platform = TargetPlatform.ANY_WEB)]
		public Control buttonSubmit;

		public SearchPage(IWebDriver driver) : base(driver)
		{
		}

		public new Page Navigate()
		{
			String baseURL = Configuration.Get("BaseURL");
			this.Driver.Navigate().GoToUrl(baseURL);
			return this;
		}
		public void SelectTravelFor(bool isLeisure)
		{
			if (isLeisure)
			{
				radioLeisure.Click();
			}
			else
			{
				radioBusiness.Click();
			}
		}
	}
}

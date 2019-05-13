using System;
using OpenQA.Selenium;
using UITesting.Framework.Core;
using UITesting.Framework.UI;
using UITesting.Framework.UI.Controls;
namespace UITesting
{
	public class LocationSearchPage : Page
	{
		[FindBy("disam_search", Platform = TargetPlatform.ANDROID_NATIVE)]
		public Edit editSearch;
		[FindBy("xpath=(//android.widget.LinearLayout[contains(@resource-id, 'disam_list_root')])[1]",
		        Platform = TargetPlatform.ANDROID_NATIVE)]
		public Control itemTopMostResult;
		public LocationSearchPage(IWebDriver driver) : base(driver)
		{
		}
	}
}

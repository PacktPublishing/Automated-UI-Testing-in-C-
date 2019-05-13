using NUnit.Framework;
using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using UITesting.Pages;
using UITesting.Framework.Core;
using UITesting.Framework.UI;
using UITesting.Framework.UI.Controls;

namespace UITesting
{
	[TestFixture()]
	public class Test
	{
		private SearchPage searchPage;
		private SearchResultsPage searchResultsPage;
		private IWebDriver driver;
		[SetUp]
		public void SetUp()
		{
			DesiredCapabilities capabilities = new DesiredCapabilities();
			Driver.Add(Configuration.Platform, Configuration.DriverPath, capabilities);
			searchPage = PageFactory.Init<SearchPage>();
			searchPage.Navigate();
		}
		[TearDown]
		public void TearDown()
		{
			Driver.Quit();
		}
		static Object[] SearchData =
		{
			new Object[] {"London", true, 1},
			new Object[] {"Manchester", false, 2},
		};
		[Test(), TestCaseSource("SearchData")]
		public void TestValidSearch(String destination, Boolean isLeisure, int adults)
		{
			searchPage.editDestination.Text = destination;
			searchPage.checkoutDayToday.Click();
			searchPage.SelectTravelFor(isLeisure);
			//searchPage.selectAdultsNumber.Text = "" + adults;
			searchPage.buttonSubmit.Click();

			searchResultsPage = PageFactory.Init<SearchResultsPage>();
			//searchResultsPage.editDestination.Click();
			Assert.True(searchResultsPage.IsTextPresent(destination));
			//searchResultsPage.CaptureScreenShot(Path.GetFullPath("./image001.png"));
		}
		[Test()]
		public void TestVerifyUIOnSearchPage()
		{ 
			Assert.True(searchPage.editDestination.Exists());
			Assert.True(searchPage.radioBusiness.Exists());
			Assert.True(searchPage.radioLeisure.Exists());
			Assert.True(searchPage.selectAdultsNumber.Exists());
			Assert.True(searchPage.buttonSubmit.Exists());
		}
	}
}

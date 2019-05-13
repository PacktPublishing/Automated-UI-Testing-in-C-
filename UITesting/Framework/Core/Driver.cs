using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;
using UITesting.Framework.UI;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;

namespace UITesting.Framework.Core
{
	public class Driver
	{
		private static Dictionary<String, IWebDriver> driverThreadMap = new Dictionary<String, IWebDriver>();
		private static Dictionary<TargetPlatform, Type> driverMap = new Dictionary<TargetPlatform, Type>()
		{
			{TargetPlatform.CHROME, typeof(ChromeDriver)},
			{TargetPlatform.FIREFOX, typeof(FirefoxDriver)},
			{TargetPlatform.IE, typeof(InternetExplorerDriver)},
			{TargetPlatform.SAFARI, typeof(SafariDriver)},
			{TargetPlatform.OPERA, typeof(OperaDriver)},
			{TargetPlatform.ANDROID_NATIVE, typeof(AndroidDriver<AppiumWebElement>)},
			{TargetPlatform.IOS_NATIVE, typeof(IOSDriver<AppiumWebElement>)}
		};
		private static Dictionary<TargetPlatform, Type> optionsMap = new Dictionary<TargetPlatform, Type>()
		{
			{TargetPlatform.CHROME, typeof(ChromeOptions)},
			{TargetPlatform.FIREFOX, typeof(FirefoxOptions)},
			{TargetPlatform.IE, typeof(InternetExplorerOptions)},
			{TargetPlatform.SAFARI, typeof(SafariOptions)},
			{TargetPlatform.OPERA, typeof(OperaOptions)}
		};
		private Driver()
		{
		}

		public static String GetThreadName()
		{
			return Thread.CurrentThread.Name + Thread.CurrentThread.ManagedThreadId;
		}

		public static void Add(TargetPlatform browser, String path, ICapabilities capabilities)
		{
			Type driverType = driverMap[browser];
			DriverOptions options = null;
			if (optionsMap.ContainsKey(browser))
			{
				options = (DriverOptions)optionsMap[browser].GetConstructor(new Type[] { }).Invoke(new Object[] { });
			}
			IWebDriver driver;
			if (browser.IsWeb())
			{
				if (browser == TargetPlatform.FIREFOX)
				{
					driver = new FirefoxDriver((FirefoxOptions)options);
				}
				else
				{
					driver = (IWebDriver)driverType.GetConstructor(new Type[] { typeof(String), optionsMap[browser] }).Invoke(new Object[] { path, options });
				}
			}
			else
			{
				driver = (IWebDriver)driverType.GetConstructor(new Type[] { typeof(Uri), typeof(DesiredCapabilities) })
											   .Invoke(new Object[] { new Uri(path), capabilities });
			}
			String threadName = GetThreadName();
			if (driverThreadMap.ContainsKey(threadName))
			{
				driverThreadMap.Remove(threadName);
			}
			driverThreadMap.Add(threadName, driver);
		}
		public static IWebDriver Current()
		{
			String threadName = GetThreadName();
			return driverThreadMap[threadName];
		}
		public static void Quit()
		{
			String threadName = GetThreadName();
			Current().Quit();
			driverThreadMap.Remove(threadName);
		}
	}
}

using System;
using System.IO;
using UITesting.Framework.UI;

namespace UITesting.Framework.Core
{
	public class Configuration
	{
		private Configuration()
		{
		}
		public static int Timeout
		{ 
			get
			{
				return Int32.Parse(Get("Timeout"));
			}
		}
		public static TargetPlatform Platform
		{ 
			get
			{
				return TargetPlatformMethods.Value(Get("Browser"));
			}
		}
		public static String DriverPath
		{ 
			get
			{
				String path = Get("DriverPath");
				if (!path.StartsWith("http:"))
				{
					String newPath = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName;
					return Path.GetFullPath(newPath);
				}
				return path;
			}
		}
		public static String Get(String option)
		{
			return System.Configuration.ConfigurationManager.AppSettings[option];
		}
	}
}

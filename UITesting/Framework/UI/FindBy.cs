using System;
namespace UITesting.Framework.UI
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	public class FindBy : Attribute
	{
		private readonly String locator;
		private TargetPlatform platform = TargetPlatform.ANY;
		private String itemLocator = "";
		private bool excludeFromSearch = false;
		public String Locator
		{ 
			get
			{
				return locator;
			}
		}
		public TargetPlatform Platform
		{
			get 
			{
				return platform;	
			}
			set
			{
				platform = value;
			}
		}
		public String ItemLocator
		{
			get
			{
				return itemLocator;
			}
			set
			{
				itemLocator = value;
			}
		}
		public bool ExcludeFromSearch
		{
			get
			{
				return excludeFromSearch;
			}
			set
			{
				excludeFromSearch = value;
			}
		}
		public FindBy(String locatorString)
		{
			locator = locatorString;
		}
	}
}

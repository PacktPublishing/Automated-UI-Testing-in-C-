using System;
using UITesting.Framework.UI.Controls;
namespace UITesting.Framework.UI
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	public class SubItem : Attribute
	{
		private readonly String name;
		private readonly String locator;
		private TargetPlatform platform = TargetPlatform.ANY;
		private Type controlType = typeof(Control);
		public String Name
		{
			get
			{
				return name;
			}
		}

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
		public Type Type
		{ 
			get
			{
				return controlType;
			}
			set
			{
				controlType = value;
			}
		}
		public SubItem(String nameValue, String locatorValue)
		{
			name = nameValue;
			locator = locatorValue;
		}
	}
}

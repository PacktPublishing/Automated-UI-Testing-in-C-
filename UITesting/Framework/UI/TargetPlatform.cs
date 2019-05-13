using System;
using System.ComponentModel;

namespace UITesting.Framework.UI
{
	public static class TargetPlatformMethods
	{
		public static String Name(this TargetPlatform type)
		{
			return ((DescriptionAttribute) type.GetType().GetField(type.ToString())
			        .GetCustomAttributes(typeof(DescriptionAttribute), false)[0]).Description;
		}
		public static bool IsMobile(this TargetPlatform type)
		{
			return type == TargetPlatform.ANY
                     || type == TargetPlatform.ANDROID_NATIVE
                     || type == TargetPlatform.IOS_NATIVE;
		}
		public static bool IsWeb(this TargetPlatform type)
		{
			return type == TargetPlatform.ANY || !type.IsMobile();
		}
		public static TargetPlatform Value(String descriptionText)
		{
			foreach (String name in typeof(TargetPlatform).GetEnumNames())
			{ 
				String description = ((DescriptionAttribute)typeof(TargetPlatform).GetField(name)
				                      .GetCustomAttributes(typeof(DescriptionAttribute), false)[0]).Description;
				if (description.Equals(descriptionText))
				{
					return (TargetPlatform) Enum.Parse(typeof(TargetPlatform), name);
				}
			}
			return TargetPlatform.ANY;
		}
	}

	public enum TargetPlatform
	{
		[Description("chrome")]
		CHROME,
		[Description("firefox")]
		FIREFOX,
		[Description("ie")]
		IE,
		[Description("opera")]
		OPERA,
		[Description("safari")]
		SAFARI,
		[Description("android")]
		ANDROID_NATIVE,
		[Description("ios")]
		IOS_NATIVE,
		[Description("anyweb")]
		ANY_WEB,
		[Description("any")]
		ANY
	}
}

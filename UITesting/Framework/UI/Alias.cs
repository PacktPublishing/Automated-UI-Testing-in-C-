using System;
namespace UITesting.Framework.UI
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Class)]
	public class AliasAttribute : Attribute
	{
		public String Name
		{
			get; set;
		}
		public AliasAttribute(String name)
		{
			this.Name = name;
		}
	}
}

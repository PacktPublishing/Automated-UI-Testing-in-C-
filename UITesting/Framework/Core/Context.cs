using System;
using System.Collections.Generic;

namespace UITesting.Framework.Core
{
	public class Context
	{
		private Context()
		{
		}
		private static Dictionary<String, Dictionary<String, Object>> contextVariables
			= new Dictionary<String, Dictionary<String, Object>>();
		public static void Put(String name, Object value)
		{
			Dictionary<String, Object> dataMap = new Dictionary<String, Object>();
			String threadName = Driver.GetThreadName();
			if (contextVariables.ContainsKey(threadName))
			{
				dataMap = contextVariables[threadName];
				contextVariables.Remove(threadName);
			}
			if (dataMap.ContainsKey(name))
			{
				dataMap.Remove(name);
			}
			dataMap.Add(name, value);
			contextVariables.Add(threadName, dataMap);
		}
		public static Object Get(String name)
		{
			String threadName = Driver.GetThreadName();
			if (contextVariables.ContainsKey(threadName))
			{
				return contextVariables[threadName][name];
			}
			return null;
		}
		public static void ClearCurrent()
		{
			String threadName = Driver.GetThreadName();
			if (contextVariables.ContainsKey(threadName))
			{
				contextVariables.Remove(threadName);
			}
			contextVariables.Add(threadName, new Dictionary<String, Object>());
		}
		public static Dictionary<String, Object>.KeyCollection Variables
		{ 
			get
			{
				String threadName = Driver.GetThreadName();
				return contextVariables[threadName].Keys;
			}
		}
	}
}

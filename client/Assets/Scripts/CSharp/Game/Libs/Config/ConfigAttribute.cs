﻿using System;

namespace MetaGame
{
	[AttributeUsage(AttributeTargets.Class)]
	public class ConfigAttribute: BaseAttribute
	{
		public AppType Type { get; }

		public ConfigAttribute(int type)
		{
			this.Type = (AppType)type;
		}
	}
}
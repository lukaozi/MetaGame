using System;

namespace MetaGame
{
	[AttributeUsage(AttributeTargets.Class)]
	public class UIFactoryAttribute: BaseAttribute
	{
		public string Type { get; }

		public UIFactoryAttribute(string type)
		{
			this.Type = type;
		}
	}
}
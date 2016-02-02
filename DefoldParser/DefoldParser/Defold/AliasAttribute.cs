using System;

namespace Defold
{
	public class AliasAttribute : Attribute
	{
		public string Name {get;set;}
		public AliasAttribute (string name)
		{
			this.Name = name;
		}
	}
}


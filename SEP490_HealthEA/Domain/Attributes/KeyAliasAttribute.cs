using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Attributes
{
	[AttributeUsage(AttributeTargets.Property)]
	public class KeyAliasAttribute : Attribute
	{
		public string[] Aliases { get; } = Array.Empty<string>();

		public KeyAliasAttribute(string[] aliases)
		{
			Aliases = aliases;
		}
		public KeyAliasAttribute() { }
	}
}

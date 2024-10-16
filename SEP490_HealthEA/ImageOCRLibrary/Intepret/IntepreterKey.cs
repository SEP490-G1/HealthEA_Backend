using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageOCRLibrary.Interpret
{
	public enum IntepreterValueType
	{
		BOOLEAN,
		NUMERIC,
		STRING,
	}
	public class IntepreterKey
	{
		public string Name { get; set; }
		public IntepreterValueType ValueType { get; set; }
		public List<string> Aliases { get; set; }

		public IntepreterKey(string name, IntepreterValueType valueType, List<string> aliases = null)
		{
			Name = name.ToLower();
			ValueType = valueType;
			Aliases = aliases != null ? aliases : new List<string>();
		}
	}
}

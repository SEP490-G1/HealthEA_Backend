using ImageOCRLibrary.Interpret;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageOCRLibrary.Demo
{
	public class SampleTestObject
	{
		[KeyAlias]
		public double Ph {  get; set; }

		[KeyAlias] 
		public double Protein { get; set; }

		[KeyAlias(["mau"])]
		public bool Blood { get; set; }
	}
}

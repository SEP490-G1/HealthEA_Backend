using Emgu.CV.Ocl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuoVia.FuzzyStrings;

namespace ImageOCRLibrary.Interpret
{
	public class TextToDictionaryIntepreter
	{
		public IEnumerable<IntepreterKey> Keys { get; private set; }
		public TextToDictionaryIntepreter(IEnumerable<IntepreterKey> keys)
		{
			Keys = keys;
		}

		public Dictionary<string, object> Interpret(string text)
		{
			var dict = new Dictionary<string, object>();
			//Split into lines
			var lines = text.Trim().ToLower()
				.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
			foreach (var line in lines)
			{
				var words = line.Trim().Split();
				//Begin interpretation
				IntepreterKey key = null;
				object? value = null;
				foreach (var word in words)
				{
					if (word == null) continue;
					if (key == null)
					{
						foreach(var k in Keys)
						{
							if (IsSimilar(word, k.Name))
							{
								key = k;
							} else
							{
								foreach (var alias in k.Aliases)
								{
									if (IsSimilar(word, alias))
									{
										key = k;
										break;
									}
								}
							}
						}
					} else
					{
						if (key.ValueType == IntepreterValueType.BOOLEAN)
						{
							bool? v = GetBooleanValue(word);
							if (v != null)
							{
								value = v;
								dict.Add(key.Name, value);
								break;
							}
						} else if (key.ValueType == IntepreterValueType.NUMERIC)
						{
							if (double.TryParse(word, out double v))
							{
								value = v;
								dict.Add(key.Name, value);
								break;
							}
						}
					}
				}
			}
			return dict;
		}


		private bool IsSimilar(string w1, string w2)
		{
			return w1.FuzzyEquals(w2,0.5);
		}

		private bool? GetBooleanValue(string text)
		{
			if (text.FuzzyEquals("negative"))
			{
				return false;
			} else if (text.FuzzyEquals("positive"))
			{
				return true;
			}
			return null;
		}
	}
}

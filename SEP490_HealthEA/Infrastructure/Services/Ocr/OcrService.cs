using Domain.Attributes;
using Domain.Interfaces;
using ImageMagick;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace Infrastructure.Services.Ocr
{
	public class OcrService : IOcrService
	{
		private string Blacklist { get; set; } = "[]{}|'\"";
		public string ImageToText(Stream stream)
		{
			var processedStream = ImagePreprocessor.PreprocessImage(stream);
			using var engine = new TesseractEngine(@"tessdata", "vie", EngineMode.Default);
			engine.SetVariable("tessedit_char_blacklist", Blacklist);
			using var image = Pix.LoadFromMemory(processedStream.ToArray());
			var page = engine.Process(image);
			return page.GetText();
		}

		public void TextToObject<T>(string text, T obj) where T : class
		{
			if (obj == null)
			{
				throw new ArgumentNullException("The argument 'obj' cannot be null.");
			}
			if (text == null)
			{
				throw new ArgumentNullException("The argument 'text' cannot be null.");
			}
			var dict = new Dictionary<string, PropertyInfo>();
			//Split into lines
			var lines = text.Trim().ToLower()
				.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
			//Get all properties with the attribute
			var properties = obj.GetType().GetProperties()
				.Where(prop => Attribute.IsDefined(prop, typeof(KeyAliasAttribute)));
			foreach (var prop in properties)
			{
				//Only allow certain type of Property.
				if (prop.PropertyType != typeof(double)
					&& prop.PropertyType != typeof(bool))
				{
					throw new ArgumentException($"KeyAliasAttribute only supports properties of type double or bool. (Property '{prop.Name}' is '{prop.PropertyType.Name}')");
				}
				//Add to dictionary.
				if (dict.ContainsKey(prop.Name.ToLower()))
				{
					throw new ArgumentException($"Duplicated alias '{prop.Name.ToLower()}'. Note: Do not have an alias the same name as the property.");
				}
				dict.Add(prop.Name.ToLower(), prop);
				var attribute = prop.GetCustomAttribute<KeyAliasAttribute>();
				foreach (var alias in attribute!.Aliases)
				{
					if (dict.ContainsKey(alias.ToLower()))
					{
						throw new ArgumentException($"Duplicated alias '{alias.ToLower()}'. Note: Do not have an alias the same name as the property.");
					}
					dict.Add(alias.ToLower(), prop);
				}
				if (dict.Count == 0)
				{
					return;
				}
				//Intepret
				foreach (var line in lines)
				{
					var words = line.Trim().Split();
					//Begin interpretation
					PropertyInfo? property = null;
					foreach (var word in words)
					{
						if (word == null) continue;
						if (property == null)
						{
							foreach (var key in dict.Keys)
							{
								if (IsSimilar(word, key))
								{
									Console.WriteLine($"got {key}");
									property = dict[key];
									continue;
								}
							}
						}
						else
						{
							//Type of double
							if (property.PropertyType == typeof(double))
							{
								if (double.TryParse(word, out double d))
								{
									property.SetValue(obj, d);
									break;
								}
							}
							//Type of boolean
							if (property.PropertyType == typeof(bool))
							{
								if (IsFalse(word))
								{
									property.SetValue(obj, false);
									break;
								}
								else if (IsTrue(word))
								{
									property.SetValue(obj, true);
									break;
								}
							}
							break;
						}
					}
				}
			}
		}

		public void ImageToObject<T>(Stream stream, T obj) where T : class
		{
			var text = ImageToText(stream);
			TextToObject(text, obj);
		}

		private bool IsFalse(string word)
		{
			var accepts = new string[]
			{
				"negative", "am" , "amtinh"
			};
			return accepts.Any(x => IsSimilar(word, x));
		}

		private bool IsTrue(string word)
		{
			var accepts = new string[]
			{
				"positive", "duong" , "duongtinh"
			};
			return accepts.Any(x => IsSimilar(word, x));
		}

		private bool IsSimilar(string w1, string w2)
		{
			if (w1 == w2)
			{
				return true;
			}
			return w1.FuzzyEquals(w2);
		}
	}


}

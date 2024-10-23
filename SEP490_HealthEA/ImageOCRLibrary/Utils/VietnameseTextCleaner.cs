using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ImageOCRLibrary.Utils
{
	public static class VietnameseTextCleaner
	{
		public static string RemoveDiacritics(string input)
		{
			var normalizedString = input.Normalize(NormalizationForm.FormD);
			var stringBuilder = new StringBuilder();

			foreach (char c in normalizedString)
			{
				var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
				if (unicodeCategory != UnicodeCategory.NonSpacingMark)
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace ImageOCRLibrary.OCR
{
	public class ImageToText
	{
		public string TessdataPath {  get; set; }
		public string LanguageModel { get; set; }
		public string Blacklist {  get; set; }

		public ImageToText(string tessdataPath, string languageModel, string blacklist)
		{
			TessdataPath = tessdataPath;
			LanguageModel = languageModel;
			Blacklist = blacklist;
		}

		public string GetText(string imgPath)
		{
			using var engine = new TesseractEngine(TessdataPath, LanguageModel, EngineMode.Default);
			engine.SetVariable("tessedit_char_blacklist", Blacklist);
			using var img = Pix.LoadFromFile(imgPath);
			using var page = engine.Process(img);
			return page.GetText();
		}
	}
}

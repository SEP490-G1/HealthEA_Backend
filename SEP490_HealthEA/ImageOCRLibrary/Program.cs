using Emgu.CV.Reg;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;
using ImageOCRLibrary.OCR;
using ImageOCRLibrary.Interpret;
using ImageOCRLibrary.Utils;
using ImageOCRLibrary.Intepret;
using ImageOCRLibrary.Demo;

namespace ImageOCRLibrary
{
	public class Program
	{
		const string BLACKLIST = "[]{}|'\"";
		static void Main(string[] args)
		{
			OCR1();
			//OCR2();
		}

		private static void OCR2()
		{
			var ttoi = new TextToObjectIntepreter();
			var obj = new SampleTestObject();
			ttoi.Intepret("", obj);
		}

		private static void OCR1()
		{
			Console.OutputEncoding = Encoding.UTF8;
			string imagePath = @"C:\Users\ADMIN\Downloads\testimg.jpg"; // Provide the path to the image file
			string preprocessedPath = @"preprocessed.jpg";
			string tessDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tessdata");

			ImagePreprocessor preprocessor = new ImagePreprocessor();
			preprocessor.PreprocessImage(imagePath, preprocessedPath);

			var itt = new ImageToText(tessDataPath, "vie", BLACKLIST);
			var text = itt.GetText(preprocessedPath);
			var processedText = VietnameseTextCleaner.RemoveDiacritics(text.Trim().ToLower());
			Console.WriteLine(processedText);

			//Step 2
			var intepreter = new TextToObjectIntepreter();
			var testData = new SampleTestObject();
			intepreter.Intepret(processedText, testData);
			Console.WriteLine(testData.Protein);
			Console.WriteLine(testData.Ph);
			Console.WriteLine(testData.Blood);

		}
	}
}

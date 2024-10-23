using Infrastructure.Services.Ocr;
using Moq;
using Tesseract;
using Domain.Attributes;

namespace Infrastructure_Test
{
	public class OcrServiceTests
	{
		private readonly OcrService _ocrService;

		public OcrServiceTests()
		{
			_ocrService = new OcrService();
		}


		[Fact]
		public void TextToObject_ShouldPopulateObjectProperties()
		{
			var text = "somedouble 12.5";
			var testObject = new TestClass();
			var property = testObject.GetType().GetProperty("SomeDouble");
			property.SetValue(testObject, 0.0);
			_ocrService.TextToObject(text, testObject);
			Assert.Equal(12.5, testObject.SomeDouble);
		}

		[Fact]
		public void TextToObject_ShouldThrowArgumentNullException_IfObjectIsNull()
		{
			string text = "somedouble 12.5";
			Assert.Throws<ArgumentNullException>(() => _ocrService.TextToObject<TestClass>(text, null));
		}

		[Fact]
		public void TextToObject_ShouldThrowArgumentNullException_IfTextIsNull()
		{
			var obj = new TestClass();
			Assert.Throws<ArgumentNullException>(() => _ocrService.TextToObject(null, obj));
		}

		private class TestClass
		{
			[KeyAlias()]
			public double SomeDouble { get; set; }

			[KeyAlias]
			public bool SomeBool { get; set; }
		}
	}
}

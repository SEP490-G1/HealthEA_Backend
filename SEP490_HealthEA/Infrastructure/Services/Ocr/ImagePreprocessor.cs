using ImageMagick;

namespace Infrastructure.Services.Ocr
{
	public static class ImagePreprocessor
	{
		public static MemoryStream PreprocessImage(Stream imgStream)
		{
			using MagickImage image = new MagickImage(imgStream);
			// Grayscale
			image.ColorType = ColorType.Grayscale;
			// Invert
			image.Negate();
			// Compose
			image.SetArtifact("morphology:compose", "darken");
			// Morphology
			var morphologySettings = new MorphologySettings()
			{
				Method = MorphologyMethod.Thinning,
				KernelArguments = "1x30+0+0<",
				Kernel = Kernel.Rectangle,
			};
			image.Morphology(morphologySettings);
			// Invert
			image.Negate();
			// Double the image size
			image.Resize(image.Width * 2, image.Height * 2);
			// Contrast
			image.Contrast();
			image.Sharpen();
			//Theresehold
			image.Threshold(new Percentage(80));
			// Create a memory stream to hold the processed image data
			var outputStream = new MemoryStream();
			// Write the image
			image.Write(outputStream, MagickFormat.Png);
			// Reset stream position
			outputStream.Position = 0;
			return outputStream;
		}
	}
}
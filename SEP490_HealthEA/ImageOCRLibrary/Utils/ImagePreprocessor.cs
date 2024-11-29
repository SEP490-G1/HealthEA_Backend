using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using ImageMagick;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

public class ImagePreprocessor
{
	public void PreprocessImage(string inputImagePath, string outputImagePath)
	{
		using MagickImage image = new MagickImage(inputImagePath);
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
		// Save
		image.Write(outputImagePath);
	}
}

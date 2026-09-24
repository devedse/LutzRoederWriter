using System;
using System.Drawing;
using System.IO;

namespace Writer
{
	internal sealed class ImageResource
	{
		private ImageResource()
		{
		}
		public static Image Application
		{
			get
			{
				if (ImageResource.application == null)
				{
					Stream manifestResourceStream = typeof(ImageResource).Assembly.GetManifestResourceStream("htmlwriter.Application.png");
					ImageResource.application = new Bitmap(manifestResourceStream);
					manifestResourceStream.Close();
				}
				return ImageResource.application;
			}
		}
		private static Image application = null;
	}
}

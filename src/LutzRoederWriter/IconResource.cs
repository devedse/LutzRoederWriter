using System;
using System.Drawing;

namespace Writer
{
	internal sealed class IconResource
	{
		private IconResource()
		{
		}
		public static Icon Application
		{
			get
			{
				return new Icon(typeof(IconResource).Assembly.GetManifestResourceStream("htmlwriter.Application.ico"));
			}
		}
	}
}

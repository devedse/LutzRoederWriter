using System;
using System.Drawing;
using System.IO;

namespace Writer
{
	internal sealed class FormatResource
	{
		private FormatResource()
		{
		}
		static FormatResource()
		{
			Stream manifestResourceStream = typeof(CommandBarImageResource).Assembly.GetManifestResourceStream("htmlwriter.Format.png");
			Bitmap bitmap = new Bitmap(manifestResourceStream);
			bitmap.MakeTransparent(Color.FromArgb(255, 0, 255));
			int num = bitmap.Width / bitmap.Height;
			FormatResource.images = new Image[num];
			Rectangle rectangle = new Rectangle(0, 0, bitmap.Height, bitmap.Height);
			for (int i = 0; i < num; i++)
			{
				FormatResource.images[i] = bitmap.Clone(rectangle, bitmap.PixelFormat);
				rectangle.X += bitmap.Height;
			}
			manifestResourceStream.Close();
		}
		public static Image ForeColor
		{
			get
			{
				return FormatResource.images[0];
			}
		}
		public static Image BackColor
		{
			get
			{
				return FormatResource.images[1];
			}
		}
		public static Image Bold
		{
			get
			{
				return FormatResource.images[2];
			}
		}
		public static Image Italic
		{
			get
			{
				return FormatResource.images[3];
			}
		}
		public static Image Underline
		{
			get
			{
				return FormatResource.images[4];
			}
		}
		public static Image Superscript
		{
			get
			{
				return FormatResource.images[5];
			}
		}
		public static Image Subscript
		{
			get
			{
				return FormatResource.images[6];
			}
		}
		public static Image Strikethrough
		{
			get
			{
				return FormatResource.images[7];
			}
		}
		public static Image AlignLeft
		{
			get
			{
				return FormatResource.images[8];
			}
		}
		public static Image AlignCenter
		{
			get
			{
				return FormatResource.images[9];
			}
		}
		public static Image AlignRight
		{
			get
			{
				return FormatResource.images[10];
			}
		}
		public static Image OrderedList
		{
			get
			{
				return FormatResource.images[11];
			}
		}
		public static Image UnorderedList
		{
			get
			{
				return FormatResource.images[12];
			}
		}
		public static Image Unindent
		{
			get
			{
				return FormatResource.images[13];
			}
		}
		public static Image Indent
		{
			get
			{
				return FormatResource.images[14];
			}
		}
		public static Image Black
		{
			get
			{
				return FormatResource.images[15];
			}
		}
		public static Image Red
		{
			get
			{
				return FormatResource.images[16];
			}
		}
		public static Image Green
		{
			get
			{
				return FormatResource.images[17];
			}
		}
		public static Image Blue
		{
			get
			{
				return FormatResource.images[18];
			}
		}
		public static Image Yellow
		{
			get
			{
				return FormatResource.images[19];
			}
		}
		public static Image White
		{
			get
			{
				return FormatResource.images[20];
			}
		}
		public static Image Hyperlink
		{
			get
			{
				return FormatResource.images[21];
			}
		}
		public static Image Picture
		{
			get
			{
				return FormatResource.images[22];
			}
		}
		private static Image[] images = null;
	}
}

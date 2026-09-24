using System;
using System.Drawing;
using System.IO;

namespace Writer
{
	internal sealed class CommandBarImageResource
	{
		private CommandBarImageResource()
		{
		}
		static CommandBarImageResource()
		{
			Stream manifestResourceStream = typeof(CommandBarImageResource).Assembly.GetManifestResourceStream("htmlwriter.CommandBar.png");
			Bitmap bitmap = new Bitmap(manifestResourceStream);
			int num = bitmap.Width / bitmap.Height;
			CommandBarImageResource.images = new Image[num];
			Rectangle rectangle = new Rectangle(0, 0, bitmap.Height, bitmap.Height);
			for (int i = 0; i < num; i++)
			{
				CommandBarImageResource.images[i] = bitmap.Clone(rectangle, bitmap.PixelFormat);
				rectangle.X += bitmap.Height;
			}
			manifestResourceStream.Close();
		}
		public static Image New
		{
			get
			{
				return CommandBarImageResource.images[0];
			}
		}
		public static Image Open
		{
			get
			{
				return CommandBarImageResource.images[1];
			}
		}
		public static Image Save
		{
			get
			{
				return CommandBarImageResource.images[2];
			}
		}
		public static Image Cut
		{
			get
			{
				return CommandBarImageResource.images[3];
			}
		}
		public static Image Copy
		{
			get
			{
				return CommandBarImageResource.images[4];
			}
		}
		public static Image Paste
		{
			get
			{
				return CommandBarImageResource.images[5];
			}
		}
		public static Image Delete
		{
			get
			{
				return CommandBarImageResource.images[6];
			}
		}
		public static Image Properties
		{
			get
			{
				return CommandBarImageResource.images[7];
			}
		}
		public static Image Undo
		{
			get
			{
				return CommandBarImageResource.images[8];
			}
		}
		public static Image Redo
		{
			get
			{
				return CommandBarImageResource.images[9];
			}
		}
		public static Image Preview
		{
			get
			{
				return CommandBarImageResource.images[10];
			}
		}
		public static Image Print
		{
			get
			{
				return CommandBarImageResource.images[11];
			}
		}
		public static Image Search
		{
			get
			{
				return CommandBarImageResource.images[12];
			}
		}
		public static Image ReSearch
		{
			get
			{
				return CommandBarImageResource.images[13];
			}
		}
		public static Image Help
		{
			get
			{
				return CommandBarImageResource.images[14];
			}
		}
		public static Image ZoomIn
		{
			get
			{
				return CommandBarImageResource.images[15];
			}
		}
		public static Image ZoomOut
		{
			get
			{
				return CommandBarImageResource.images[16];
			}
		}
		public static Image Back
		{
			get
			{
				return CommandBarImageResource.images[17];
			}
		}
		public static Image Forward
		{
			get
			{
				return CommandBarImageResource.images[18];
			}
		}
		public static Image Favorites
		{
			get
			{
				return CommandBarImageResource.images[19];
			}
		}
		public static Image AddToFavorites
		{
			get
			{
				return CommandBarImageResource.images[20];
			}
		}
		public static Image Stop
		{
			get
			{
				return CommandBarImageResource.images[21];
			}
		}
		public static Image Refresh
		{
			get
			{
				return CommandBarImageResource.images[22];
			}
		}
		public static Image Home
		{
			get
			{
				return CommandBarImageResource.images[23];
			}
		}
		public static Image Edit
		{
			get
			{
				return CommandBarImageResource.images[24];
			}
		}
		public static Image Tools
		{
			get
			{
				return CommandBarImageResource.images[25];
			}
		}
		public static Image Tiles
		{
			get
			{
				return CommandBarImageResource.images[26];
			}
		}
		public static Image Icons
		{
			get
			{
				return CommandBarImageResource.images[27];
			}
		}
		public static Image List
		{
			get
			{
				return CommandBarImageResource.images[28];
			}
		}
		public static Image Details
		{
			get
			{
				return CommandBarImageResource.images[29];
			}
		}
		public static Image Pane
		{
			get
			{
				return CommandBarImageResource.images[30];
			}
		}
		public static Image Culture
		{
			get
			{
				return CommandBarImageResource.images[31];
			}
		}
		public static Image Languages
		{
			get
			{
				return CommandBarImageResource.images[32];
			}
		}
		public static Image History
		{
			get
			{
				return CommandBarImageResource.images[33];
			}
		}
		public static Image Mail
		{
			get
			{
				return CommandBarImageResource.images[34];
			}
		}
		public static Image Parent
		{
			get
			{
				return CommandBarImageResource.images[35];
			}
		}
		public static Image FolderProperties
		{
			get
			{
				return CommandBarImageResource.images[36];
			}
		}
		private static Image[] images = null;
	}
}

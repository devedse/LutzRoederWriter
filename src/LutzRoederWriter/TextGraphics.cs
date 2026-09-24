using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Writer
{
	internal class TextGraphics : IDisposable
	{
		public TextGraphics(Graphics graphics)
		{
			this.graphics = graphics;
			this.graphicsHandle = graphics.GetHdc();
		}
		public TextGraphics(IntPtr graphicsHandle)
		{
			this.graphics = null;
			this.graphicsHandle = graphicsHandle;
		}
		public void Dispose()
		{
			if (this.graphics != null)
			{
				this.graphics.ReleaseHdc(this.graphicsHandle);
				this.graphics = null;
			}
			this.graphicsHandle = IntPtr.Zero;
		}
		public Size MeasureText(string text, Font font)
		{
			IntPtr intPtr = font.ToHfont();
			IntPtr intPtr2 = TextGraphics.NativeMethods.SelectObject(this.graphicsHandle, intPtr);
			Size size = this.MeassureTextInternal(text);
			TextGraphics.NativeMethods.SelectObject(this.graphicsHandle, intPtr2);
			TextGraphics.NativeMethods.DeleteObject(intPtr);
			return size;
		}
		public void DrawText(string text, Point point, Font font, Color foreColor)
		{
			IntPtr intPtr = font.ToHfont();
			IntPtr intPtr2 = TextGraphics.NativeMethods.SelectObject(this.graphicsHandle, intPtr);
			int num = TextGraphics.NativeMethods.SetBkMode(this.graphicsHandle, 1);
			int num2 = TextGraphics.NativeMethods.SetTextColor(this.graphicsHandle, Color.FromArgb(0, (int)foreColor.R, (int)foreColor.G, (int)foreColor.B).ToArgb());
			Size size = this.MeassureTextInternal(text);
			TextGraphics.NativeMethods.RECT rect = default(TextGraphics.NativeMethods.RECT);
			rect.left = point.X;
			rect.top = point.Y;
			rect.right = rect.left + size.Width;
			rect.bottom = rect.top + size.Height;
			TextGraphics.NativeMethods.DrawText(this.graphicsHandle, text, text.Length, ref rect, 32);
			TextGraphics.NativeMethods.SetTextColor(this.graphicsHandle, num2);
			TextGraphics.NativeMethods.SetBkMode(this.graphicsHandle, num);
			TextGraphics.NativeMethods.SelectObject(this.graphicsHandle, intPtr2);
			TextGraphics.NativeMethods.DeleteObject(intPtr);
		}
		private Size MeassureTextInternal(string text)
		{
			TextGraphics.NativeMethods.RECT rect = default(TextGraphics.NativeMethods.RECT);
			rect.left = 0;
			rect.right = 0;
			rect.top = 0;
			rect.bottom = 0;
			TextGraphics.NativeMethods.DrawText(this.graphicsHandle, text, text.Length, ref rect, 1056);
			return new Size(rect.right, rect.bottom);
		}
		private Graphics graphics;
		private IntPtr graphicsHandle;
		private sealed class NativeMethods
		{
			private NativeMethods()
			{
			}
			[DllImport("gdi32.dll")]
			public static extern int SetBkMode(IntPtr hdc, int iBkMode);
			[DllImport("gdi32.dll")]
			public static extern int SetTextColor(IntPtr hdc, int crColor);
			[DllImport("gdi32.dll")]
			public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);
			[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
			public static extern bool DeleteObject(IntPtr hObject);
			[DllImport("user32.dll")]
			public static extern int DrawText(IntPtr hdc, string lpString, int nCount, ref TextGraphics.NativeMethods.RECT lpRect, int uFormat);
			public const int TRANSPARENT = 1;
			public const int OPAQUE = 2;
			public const int DT_SINGLELINE = 32;
			public const int DT_LEFT = 0;
			public const int DT_VCENTER = 4;
			public const int DT_CALCRECT = 1024;
			public struct RECT
			{
				public int left;
				public int top;
				public int right;
				public int bottom;
			}
		}
	}
}

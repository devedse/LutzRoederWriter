using System;
using System.Windows.Forms;

namespace Writer
{
	internal class DocumentView : Panel
	{
		public DocumentView(Document document)
		{
			this.document = document;
			this.Dock = DockStyle.Fill;
		}
		private Document document;
	}
}

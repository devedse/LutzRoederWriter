using System;

namespace Writer
{
	internal sealed class Resource
	{
		private Resource()
		{
		}
		public static string GetString(string name)
		{
			if (name != null)
			{
				string text;
				if (!(name == "ApplicationName"))
				{
					if (!(name == "Homepage"))
					{
						if (!(name == "Ok"))
						{
							if (!(name == "Cancel"))
							{
								if (!(name == "HtmlFilter"))
								{
									if (!(name == "PictureFilter"))
									{
										goto IL_0089;
									}
									text = "Picture files (*.jpg,*.png,*.bmp,*.gif)|*.jpg;*.png;*.bmp;*.gif|All files (*.*)|*.*";
								}
								else
								{
									text = "HTML files (*.htm,*.html)|*.htm;*.html|All files (*.*)|*.*";
								}
							}
							else
							{
								text = "Cancel";
							}
						}
						else
						{
							text = "OK";
						}
					}
					else
					{
						text = "http://www.lutzroeder.com";
					}
				}
				else
				{
					text = "Writer";
				}
				return text;
			}
			IL_0089:
			throw new NotImplementedException();
		}
	}
}

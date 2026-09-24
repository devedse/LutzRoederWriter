using System;
using System.Collections;
using System.Globalization;

namespace Writer
{
	internal sealed class CommandLine
	{
		public CommandLine()
		{
			string[] commandLineArgs = Environment.GetCommandLineArgs();
			for (int i = 1; i < commandLineArgs.Length; i++)
			{
				string text = commandLineArgs[i];
				string text2 = string.Empty;
				string text3 = string.Empty;
				if (text[0] != '/' && text[0] != '-')
				{
					text3 = text;
				}
				else
				{
					int num = text.IndexOf(':');
					if (num == -1)
					{
						text2 = text.Substring(1).ToLower(CultureInfo.InvariantCulture);
						if (text2 == "?")
						{
							text2 = "help";
						}
					}
					else
					{
						text2 = text.Substring(1, num - 1).ToLower(CultureInfo.InvariantCulture);
						text3 = text.Substring(num + 1);
					}
				}
				ArrayList arrayList = (ArrayList)this.dictionary[text2];
				if (arrayList == null)
				{
					arrayList = new ArrayList();
					this.dictionary.Add(text2, arrayList);
				}
				arrayList.Add(text3);
			}
		}
		public string GetArgument(string name)
		{
			ArrayList arrayList = (ArrayList)this.dictionary[name];
			string text;
			if (arrayList != null)
			{
				if (arrayList.Count != 1)
				{
					throw new InvalidOperationException();
				}
				text = (string)arrayList[0];
			}
			else
			{
				text = null;
			}
			return text;
		}
		public string[] GetArguments(string name)
		{
			ArrayList arrayList = (ArrayList)this.dictionary[name];
			string[] array2;
			if (arrayList != null)
			{
				string[] array = new string[arrayList.Count];
				arrayList.CopyTo(array, 0);
				array2 = array;
			}
			else
			{
				array2 = null;
			}
			return array2;
		}
		private IDictionary dictionary = new Hashtable();
	}
}

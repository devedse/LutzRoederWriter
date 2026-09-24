using System;
using System.Collections;
using System.IO;

namespace Writer
{
	internal class ConfigurationManager : IConfigurationManager
	{
		public IConfiguration this[string name]
		{
			get
			{
				if (!this.table.Contains(name))
				{
					this.table.Add(name, new Configuration());
				}
				return (IConfiguration)this.table[name];
			}
		}
		public void Load(string fileName)
		{
			fileName = Path.ChangeExtension(fileName, ".cfg");
			if (File.Exists(fileName))
			{
				using (Stream stream = File.OpenRead(fileName))
				{
					this.Load(stream);
				}
			}
		}
		public void Save(string fileName)
		{
			fileName = Path.ChangeExtension(fileName, ".cfg");
			Stream stream = null;
			try
			{
				stream = File.Create(fileName);
				this.Save(stream);
			}
			catch (IOException)
			{
			}
			catch (UnauthorizedAccessException)
			{
			}
			finally
			{
				if (stream != null)
				{
					stream.Close();
				}
			}
		}
		public void Load(Stream stream)
		{
			using (StreamReader streamReader = new StreamReader(stream))
			{
				while (streamReader.Peek() != -1)
				{
					string text = streamReader.ReadLine();
					text = text.Trim();
					if (text.Length > 0)
					{
						if (text.StartsWith("[") && text.EndsWith("]"))
						{
							string text2 = text.Substring(1, text.Length - 2);
							Configuration configuration = (Configuration)this[text2];
							configuration.Load(streamReader);
						}
					}
				}
			}
		}
		public void Save(Stream stream)
		{
			using (StreamWriter streamWriter = new StreamWriter(stream))
			{
				IDictionaryEnumerator enumerator = this.table.GetEnumerator();
				while (enumerator.MoveNext())
				{
					Configuration configuration = (Configuration)enumerator.Value;
					if (!configuration.IsEmpty)
					{
						streamWriter.WriteLine();
						streamWriter.Write("[");
						streamWriter.Write((string)enumerator.Key);
						streamWriter.Write("]");
						streamWriter.WriteLine();
						configuration.Save(streamWriter);
					}
				}
			}
		}
		private SortedList table = new SortedList();
	}
}

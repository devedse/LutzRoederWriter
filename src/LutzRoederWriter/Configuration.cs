using System;
using System.ComponentModel;
using System.IO;

namespace Writer
{
	internal class Configuration : IConfiguration
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public void Clear()
		{
			for (int i = this.properties.Length - 1; i >= 0; i--)
			{
				string name = this.properties[i].Name;
				this[name] = null;
			}
		}
		public string this[string name]
		{
			get
			{
				for (int i = 0; i < this.properties.Length; i++)
				{
					if (this.properties[i].Name != null && this.properties[i].Name == name)
					{
						return this.properties[i].Value;
					}
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					for (int i = 0; i < this.properties.Length; i++)
					{
						if (name == this.properties[i].Name)
						{
							this.properties[i].Value = value;
							this.OnPropertyChanged(new PropertyChangedEventArgs(name));
							return;
						}
					}
					Configuration.Property[] array = new Configuration.Property[this.properties.Length + 1];
					Array.Copy(this.properties, 0, array, 0, this.properties.Length);
					array[this.properties.Length] = new Configuration.Property(name, value);
					this.properties = array;
					this.OnPropertyChanged(new PropertyChangedEventArgs(name));
				}
				else
				{
					for (int i = 0; i < this.properties.Length; i++)
					{
						if (name == this.properties[i].Name)
						{
							Configuration.Property[] array = new Configuration.Property[this.properties.Length - 1];
							Array.Copy(this.properties, 0, array, 0, i);
							Array.Copy(this.properties, i + 1, array, i, this.properties.Length - i - 1);
							this.properties = array;
							this.OnPropertyChanged(new PropertyChangedEventArgs(name));
						}
					}
				}
			}
		}
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, e);
			}
		}
		internal bool IsEmpty
		{
			get
			{
				return this.properties.Length == 0;
			}
		}
		internal void Load(StreamReader reader)
		{
			this.Clear();
			int num = 0;
			while (reader.Peek() != -1)
			{
				long position = reader.BaseStream.Position;
				string text = reader.ReadLine();
				text = text.Trim();
				if (text.Length <= 0 || text.StartsWith("["))
				{
					reader.BaseStream.Position = position;
					break;
				}
				string text2 = num.ToString();
				string text3 = text;
				int num2 = text.IndexOf("=");
				int num3 = text.IndexOf("\"");
				if (num2 != -1 && num2 < num3)
				{
					text2 = text.Substring(0, num2);
					text3 = text.Substring(num2 + 1);
				}
				if (text3.StartsWith("\""))
				{
					text3 = text3.Substring(1);
				}
				if (text3.EndsWith("\""))
				{
					text3 = text3.Substring(0, text3.Length - 1);
				}
				this[text2] = text3;
				num++;
			}
		}
		internal void Save(StreamWriter writer)
		{
			bool flag = true;
			for (int i = 0; i < this.properties.Length; i++)
			{
				if (i.ToString() != this.properties[i].Name)
				{
					flag = false;
				}
			}
			for (int i = 0; i < this.properties.Length; i++)
			{
				if (!flag)
				{
					writer.Write(this.properties[i].Name);
					writer.Write("=");
				}
				writer.Write("\"");
				writer.Write(this.properties[i].Value);
				writer.Write("\"");
				writer.WriteLine();
			}
		}
		private Configuration.Property[] properties = new Configuration.Property[0];
		private class Property
		{
			public Property(string name, string value)
			{
				this.Name = name;
				this.Value = value;
			}
			public string Name;
			public string Value;
		}
	}
}

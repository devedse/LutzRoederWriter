using System;
using System.Collections;

namespace Writer
{
	internal class FileHistory
	{
		public IConfiguration Configuration
		{
			get
			{
				return this.configuration;
			}
			set
			{
				this.configuration = value;
				this.files.Clear();
				int num = 0;
				while (this.configuration[num.ToString()] != null)
				{
					this.files.Add(this.configuration[num.ToString()]);
					num++;
				}
			}
		}
		public void AddFile(string url)
		{
			if (url != null && url.Length != 0 && !this.files.Contains(url))
			{
				this.files.Insert(0, url);
				this.UpdateConfiguration();
			}
		}
		public int Count
		{
			get
			{
				return this.files.Count;
			}
		}
		public string this[int index]
		{
			get
			{
				return (index >= this.Count) ? string.Empty : ((string)this.files[index]);
			}
		}
		public void UpdateConfiguration()
		{
			this.configuration.Clear();
			for (int i = 0; i < this.files.Count; i++)
			{
				this.configuration[i.ToString()] = (string)this.files[i];
			}
		}
		private IConfiguration configuration;
		private ArrayList files = new ArrayList();
	}
}

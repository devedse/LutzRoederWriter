using System;
using System.Collections;

namespace Writer.Forms
{
	/// <summary>
	/// Manages the command bars owned by a <see cref="CommandBarManager"/>.
	/// </summary>
	public class CommandBarCollection : ICollection, IEnumerable
	{
		public CommandBarCollection(CommandBarManager commandBarManager)
		{
			this.commandBarManager = commandBarManager;
		}
		public IEnumerator GetEnumerator()
		{
			return this.bands.GetEnumerator();
		}
		public int Count
		{
			get
			{
				return this.bands.Count;
			}
		}
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}
		public object SyncRoot
		{
			get
			{
				throw new NotImplementedException();
			}
		}
		public void CopyTo(Array array, int index)
		{
			this.bands.CopyTo(array, index);
		}
		public void CopyTo(CommandBar[] array, int index)
		{
			this.bands.CopyTo(array, index);
		}
		public int Add(CommandBar commandBar)
		{
			int num2;
			if (!this.Contains(commandBar))
			{
				int num = this.bands.Add(commandBar);
				this.commandBarManager.UpdateBands();
				num2 = num;
			}
			else
			{
				num2 = -1;
			}
			return num2;
		}
		public void Clear()
		{
			while (this.Count > 0)
			{
				this.RemoveAt(0);
			}
		}
		public bool Contains(CommandBar commandBar)
		{
			return this.bands.Contains(commandBar);
		}
		public int IndexOf(CommandBar commandBar)
		{
			return this.bands.IndexOf(commandBar);
		}
		public void Remove(CommandBar commandBar)
		{
			this.bands.Remove(commandBar);
			this.commandBarManager.UpdateBands();
		}
		public void RemoveAt(int index)
		{
			this.bands.RemoveAt(index);
			this.commandBarManager.UpdateBands();
		}
		public CommandBar this[int index]
		{
			get
			{
				return (CommandBar)this.bands[index];
			}
		}
		private CommandBarManager commandBarManager;
		private ArrayList bands = new ArrayList();
	}
}

using System;
using System.Collections.ObjectModel;

namespace Data.Collections
{
	public class LimitedObservableCollection<T> : ObservableCollection<T> where T : IComparable
	{
		private readonly int sizeLimit;

		public LimitedObservableCollection(int sizeLimit)
		{
			this.sizeLimit = sizeLimit;
		}

		protected override void InsertItem(int index, T item)
		{
			if (Count >= sizeLimit){
				RemoveAt(0);
			}

			int i = 0;

			while (i < Count && this[i].CompareTo(item) < 0){
				i++;
			}

			base.InsertItem(i, item);
		}
	}
}
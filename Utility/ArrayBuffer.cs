using System;
using System.Collections;
using System.Collections.Generic;

namespace Utility
{
	public class ArrayBuffer<TValue> : IEnumerable<TValue>
	{
		const int defaultCapacity = 1;

		TValue[] items;
		int size = 0;

		public TValue this[int index]
		{
			get { return items[index]; }
			set { items[index] = value; }
		}

		public TValue[] Items { get { return items; } }
		public int Count { get { return size; } }
		public int Capacity
		{
			get { return items.Length; }
			set
			{
				if (value != items.Length)
				{
					if (value < size) throw new InvalidOperationException("Capacity cannot be less than Count.");

					TValue[] newItems = new TValue[value];
					Array.Copy(items, newItems, size);
					items = newItems;
				}
			}
		}

		public ArrayBuffer() : this(defaultCapacity) { }
		public ArrayBuffer(int capacity)
		{
			items = new TValue[capacity];
		}

		public void Clear()
		{
			size = 0;
		}
		public void Trim()
		{
			if (size == 0) Capacity = defaultCapacity;
			else
			{
				int newSize = items.Length;
				while (newSize > size * 2) newSize /= 2;
				Capacity = newSize;
			}
		}
		public void Add(TValue item)
		{
			if (size == items.Length) Capacity *= 2;

			items[size++] = item;
		}

		public IEnumerator<TValue> GetEnumerator()
		{
			return ((IEnumerable<TValue>)items).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}